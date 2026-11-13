# Releasing packages from serilog/serilog

Each package's `Version.props` declares the newest version that should exist on NuGet, and merging a change to that file into `main` publishes the package. Every other push to `main` publishes prereleases of only the packages whose source changed.

## How publishing is decided

On every push to `main` or `release/**`, `Build.ps1` puts each package into exactly one of three states. Pull request builds compute and print the same plan but never publish, and release branches publish releases only.

| State      | Condition                                                                  | What is published                                                                                    |
| ---------- | -------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------- |
| Release    | The declared version is not on nuget.org and has no release tag            | The declared version, such as `6.1.0`. The commit is then tagged `Serilog.Sinks.Console/v6.1.0`      |
| Prerelease | The declared version is released, and `src/<PackageId>/` differs from its release tag | The declared version with patch + 1 and a CI suffix, such as `6.1.1-dev-00482`             |
| None       | The declared version is released, and `src/<PackageId>/` matches its release tag      | Nothing                                                                                    |

Three consequences follow from these rules:

- Ordinary pull requests never touch `Version.props`. Unreleased changes can sit on `main` for as long as needed.
- The version number is chosen at release time, when it is clear whether the accumulated changes are a patch, a minor or a major.
- Prereleases use patch + 1 because the declared version is already published. `6.1.0-dev-00482` would sort below `6.1.0`, while `6.1.1-dev-00482` sorts above it and below any later release.

## Repository layout

Each package lives in a directory named exactly after its package ID. That name is also the project file name, the release tag prefix and the key `Build.ps1` uses throughout.

```text
serilog/
  Build.ps1
  Directory.Build.props
  Directory.Packages.props
  .github/
    CODEOWNERS
    workflows/ci.yml
  src/
    Serilog/
      Serilog.csproj
      Version.props
    Serilog.Sinks.Console/
      Serilog.Sinks.Console.csproj
      Version.props
  test/
    Serilog.Sinks.Console.Tests/
  artifacts/              git-ignored, wiped at the start of every build
    versions/             resolved versions for this build, one file per package
    packages/             packed .nupkg and .snupkg files
```

### `Version.props`

`Version.props` holds one property and nothing else. The value is always three numeric parts with no prerelease label, because it is compared as a string against nuget.org's normalised version list.

```xml
<Project>
  <PropertyGroup>
    <VersionPrefix>6.1.0</VersionPrefix>
  </PropertyGroup>
</Project>
```

### `Directory.Build.props`

The root `Directory.Build.props` imports the declared version, then the resolved version for the current build if `Build.ps1` has written one. Local builds without the generated file get the declared version.

```xml
<Project>
  <Import Project="$(MSBuildProjectDirectory)/Version.props"
          Condition="Exists('$(MSBuildProjectDirectory)/Version.props')" />
  <Import Project="$(MSBuildThisFileDirectory)artifacts/versions/$(MSBuildProjectName).props"
          Condition="Exists('$(MSBuildThisFileDirectory)artifacts/versions/$(MSBuildProjectName).props')" />
</Project>
```

Versions reach MSBuild through these files and never through `-p:Version=` or `--version-suffix`. Command-line properties are global and flow into referenced projects, so packing one package that way stamps its version onto its dependencies on sibling packages.

### Release tags

A release tag has the form `<PackageId>/v<version>` and marks the commit a stable version was built from. The tag for each package's current declared version must exist, because prerelease detection diffs against it.

When a package is first imported into the monorepo, tag the import commit with the version already on nuget.org:

```shell
git tag Serilog.Sinks.Console/v6.0.0 <import-commit>
git push origin Serilog.Sinks.Console/v6.0.0
```

## Releasing

### One package

1. Open a pull request that changes `src/<PackageId>/Version.props` to the version to ship. Keep it separate from code changes so the release is obvious in review.
2. Read the plan in the pull request's build summary. It should list that package as Release with the expected version, and no other package as Release.
3. Get code owner approval and merge to `main`.
4. CI publishes the package, tags the merge commit and creates the GitHub release. The version appears on nuget.org a few minutes later.

### A related set

Bump the `Version.props` of every package in the set in one pull request. All of them are published from the same commit in the same CI run.

Packages in this repository reference each other by `ProjectReference`, which adds one rule: a package cannot be released while a sibling it references, directly or through other siblings, has unreleased changes. The build fails and names the sibling, and the fix is to bump the sibling in the same pull request.

### A new package

1. Add `src/<PackageId>/` with its project file, but leave out `Version.props` until the package is ready. `Build.ps1` ignores directories without one.
2. When it is ready, add `Version.props` with the first version in a pull request of its own. No tag is needed, because a version that is neither on nuget.org nor tagged is a Release.
3. After the first publish, confirm on nuget.org that the new package ID is owned by the `serilog` organisation.

### An older major

1. Create `release/<major>.x` from the last release tag of that major, for example `git branch release/6.x Serilog.Sinks.Console/v6.1.0`.
2. Merge the fix into that branch by pull request, then bump `Version.props` there, for example to `6.1.1`.
3. CI publishes and tags from the release branch exactly as it does from `main`.

Release branches never publish prereleases, so other packages that happen to differ from their tags on that branch stay untouched.

## `Build.ps1`: planning

`Build.ps1` first works out a plan, one entry per package, and only then builds anything. The plan is the single source of truth for versions, packing, pushing and tagging.

### Helpers

```powershell
$ErrorActionPreference = 'Stop'

$isPush = $env:GITHUB_EVENT_NAME -eq 'push'
$isMain = $env:GITHUB_REF -eq 'refs/heads/main'
$suffix = if ($isPush) { 'dev-{0:d5}' -f [int]$env:GITHUB_RUN_NUMBER } else { 'local' }

function Get-DeclaredVersion([string] $id) {
    $xml = [xml](Get-Content "src/$id/Version.props" -Raw)
    $version = "$($xml.Project.PropertyGroup.VersionPrefix)".Trim()
    if ($version -notmatch '^\d+\.\d+\.\d+$') {
        throw "src/$id/Version.props: '$version' is not in major.minor.patch form"
    }
    $version
}

function Get-PublishedVersions([string] $id) {
    $url = "https://api.nuget.org/v3-flatcontainer/$($id.ToLowerInvariant())/index.json"
    try {
        (Invoke-RestMethod $url -MaximumRetryCount 3 -RetryIntervalSec 5).versions
    }
    catch {
        if ([int]$_.Exception.Response.StatusCode -eq 404) { return }   # never published
        throw   # anything else stops the build
    }
}

function Test-Tag([string] $tag) {
    git rev-parse --quiet --verify "refs/tags/$tag" *> $null
    $LASTEXITCODE -eq 0
}

function Test-ChangedSince([string] $tag, [string] $path) {
    git diff --quiet "refs/tags/$tag" HEAD -- $path
    $LASTEXITCODE -ne 0
}

function Step-Patch([string] $version) {
    $major, $minor, $patch = $version.Split('.')
    "$major.$minor.$([int]$patch + 1)"
}
```

Four details in the helpers are easy to get wrong:

- The package ID in the nuget.org URL must be lower case, or the request returns 404 for a package that exists.
- Only a 404 means "never published". A timeout or server error that was treated the same way would turn every package into a Release.
- The run number is zero-padded because prerelease labels sort as text. Unpadded, `dev-999` sorts above `dev-1000`.
- The tag and diff checks need full history and tags, which the workflow's checkout step must fetch.

### The plan

```powershell
$plan = foreach ($dir in Get-ChildItem src -Directory | Sort-Object Name) {
    $id = $dir.Name
    if (-not (Test-Path "src/$id/Version.props")) { continue }

    $declared = Get-DeclaredVersion $id
    $tag      = "$id/v$declared"
    $onNuGet  = @(Get-PublishedVersions $id) -contains $declared
    $tagged   = Test-Tag $tag

    $action, $version =
        if (-not $onNuGet -and -not $tagged) { 'Release', $declared }
        elseif (-not $tagged) { throw "$id $declared is on nuget.org but tag $tag is missing" }
        elseif (Test-ChangedSince $tag "src/$id") { 'Prerelease', "$(Step-Patch $declared)-$suffix" }
        else { 'None', $declared }

    [pscustomobject]@{ Id = $id; Action = $action; Version = $version; Tag = $tag }
}
```

The plan treats a version as released when it is on nuget.org or tagged, which gives four cases:

| On nuget.org | Tagged | Meaning                                             | Result                                                |
| ------------ | ------ | --------------------------------------------------- | ----------------------------------------------------- |
| No           | No     | Not released yet                                    | Release                                               |
| Yes          | Yes    | Released                                            | Prerelease or None, by diff against the tag           |
| No           | Yes    | Released moments ago, nuget.org still indexing      | Prerelease or None, by diff against the tag           |
| Yes          | No     | Tagging failed, or the import tag was never created | Build fails; see [Troubleshooting](#troubleshooting)  |

The third case matters because nuget.org takes a few minutes to list a new version. Without the tag check, a second push inside that window would plan the same release again.

### Checking and applying the plan

```powershell
# Every sibling a package references, directly or through other siblings.
function Get-SiblingReferences([string] $id, [System.Collections.Generic.HashSet[string]] $seen) {
    Select-Xml -Path "src/$id/$id.csproj" -XPath '//ProjectReference/@Include' |
        ForEach-Object { [IO.Path]::GetFileNameWithoutExtension($_.Node.Value.Replace('\', '/')) } |
        Where-Object { $seen.Add($_) } |
        ForEach-Object { $_; Get-SiblingReferences $_ $seen }
}

# A stable package must not depend on unreleased changes in any of them.
foreach ($p in $plan | Where-Object Action -eq 'Release') {
    $refs = @(Get-SiblingReferences $p.Id ([System.Collections.Generic.HashSet[string]]::new()))
    $unreleased = $plan | Where-Object { $_.Id -in $refs -and $_.Action -eq 'Prerelease' }
    if ($unreleased) {
        throw "$($p.Id) $($p.Version) references unreleased changes in $($unreleased.Id -join ', ')"
    }
}

# Hand each project its resolved version (imported by Directory.Build.props).
Remove-Item artifacts -Recurse -Force -ErrorAction Ignore
New-Item artifacts/versions -ItemType Directory | Out-Null
foreach ($p in $plan) {
    "<Project><PropertyGroup><Version>$($p.Version)</Version></PropertyGroup></Project>" |
        Set-Content "artifacts/versions/$($p.Id).props"
}

# Show the plan on the run and on pull requests.
if ($env:GITHUB_STEP_SUMMARY) {
    $plan | Where-Object Action -ne 'None' |
        ForEach-Object { "- **$($_.Action)**: $($_.Id) $($_.Version)" } |
        Add-Content $env:GITHUB_STEP_SUMMARY
}

dotnet build -c Release
if ($LASTEXITCODE -ne 0) { throw 'Build failed' }
dotnet test -c Release --no-build
if ($LASTEXITCODE -ne 0) { throw 'Tests failed' }
```

The `Replace('\', '/')` in the reference check is needed on Linux runners, where a Windows-style `Include` path is otherwise read as one long file name.

## `Build.ps1`: publishing

After the build and tests pass, `Build.ps1` packs every package the plan marks as changed, then pushes and tags only on a push build with an API key present.

```powershell
# Pack everything that is not 'None', on every build, so pull requests catch packing errors.
foreach ($p in $plan | Where-Object Action -ne 'None') {
    dotnet pack "src/$($p.Id)" -c Release --no-build -o artifacts/packages
    if ($LASTEXITCODE -ne 0) { throw "Pack failed for $($p.Id)" }
}

if (-not ($isPush -and $env:NUGET_API_KEY)) { return }

# Releases from main and release branches; prereleases from main only.
$toPublish = $plan | Where-Object {
    $_.Action -eq 'Release' -or ($_.Action -eq 'Prerelease' -and $isMain)
}

foreach ($p in $toPublish) {
    dotnet nuget push "artifacts/packages/$($p.Id).$($p.Version).nupkg" `
        --source https://api.nuget.org/v3/index.json `
        --api-key $env:NUGET_API_KEY `
        --skip-duplicate
    if ($LASTEXITCODE -ne 0) { throw "Push failed for $($p.Id) $($p.Version)" }

    if ($p.Action -ne 'Release') { continue }

    if (-not (Test-Tag $p.Tag)) {
        git tag $p.Tag
        git push origin $p.Tag
        if ($LASTEXITCODE -ne 0) { throw "Could not push tag $($p.Tag)" }
    }

    gh release view $p.Tag *> $null
    if ($LASTEXITCODE -ne 0) {
        gh release create $p.Tag --verify-tag `
            --title "$($p.Id) $($p.Version)" `
            --notes "https://www.nuget.org/packages/$($p.Id)/$($p.Version)"
    }
}
```

The order and the flags here carry the reliability of the whole process:

- Each package is tagged immediately after its own push succeeds, and never before. The plan relies on a tag meaning the version was pushed.
- `--skip-duplicate` makes a re-run safe after a partial failure. Packages already pushed are skipped, and the rest are published and tagged.
- The tag and GitHub release steps check before creating, for the same reason.
- `gh release create --generate-notes` is not used. In a monorepo it lists every pull request since the previous release of any package.

## Workflow and trusted publishing

One workflow file, `.github/workflows/ci.yml`, builds every pull request and publishes on every push. Keeping all publishing in this one file is what lets a single nuget.org policy cover every package.

```yaml
name: CI

on:
  push:
    branches: [main, 'release/**']
  pull_request:
    branches: [main, 'release/**']

concurrency:
  group: ci-${{ github.ref }}
  cancel-in-progress: ${{ github.event_name == 'pull_request' }}

jobs:
  build:
    runs-on: ubuntu-latest
    permissions:
      contents: write   # push release tags, create GitHub releases
      id-token: write   # NuGet trusted publishing
    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0
          fetch-tags: true

      - uses: actions/setup-dotnet@v4

      - name: NuGet login
        id: login
        if: github.event_name == 'push'
        uses: NuGet/login@v1
        with:
          user: ${{ secrets.NUGET_USER }}

      - name: Build and publish
        shell: pwsh
        env:
          NUGET_API_KEY: ${{ steps.login.outputs.NUGET_API_KEY }}
          GH_TOKEN: ${{ github.token }}
        run: ./Build.ps1
```

The parts of the workflow that must not drift:

- `fetch-depth: 0` and `fetch-tags: true` give `Build.ps1` the history and tags it diffs against. A shallow checkout makes every tagged package look untagged.
- The concurrency group queues pushes to the same branch and never cancels one, so a publish is never interrupted halfway. Only pull request builds are cancelled by newer commits.
- The login step runs only on pushes, so pull requests, including those from forks, never receive a key.
- The key from `NuGet/login` is valid for one hour. If the build and tests ever approach its lifetime, move the login to just before the push.

### The nuget.org policy

The [trusted publishing](https://learn.microsoft.com/nuget/nuget-org/trusted-publishing) policy is created once on nuget.org with these values:

| Field            | Value                                |
| ---------------- | ------------------------------------ |
| Policy owner     | The `serilog` organisation           |
| Repository owner | `serilog`                            |
| Repository       | `serilog`                            |
| Workflow file    | `ci.yml` (file name only, no path)   |
| Environment      | Left blank                           |

Renaming `ci.yml`, or moving the publish step into another workflow, breaks publishing until the policy is updated. `NUGET_USER` is a nuget.org profile name, not an email address.

## Guardrails

Merging a `Version.props` change is the release, so the pull request review is the release approval. Three things enforce that.

### Code owners

`.github/CODEOWNERS` routes every file that can cause or alter a publish to the release managers. The team name below is a placeholder.

```text
# Merging a change to any of these can publish a package.
**/Version.props          @serilog/release-managers
/Build.ps1                @serilog/release-managers
/Directory.Build.props    @serilog/release-managers
/.github/                 @serilog/release-managers
```

### Branch protection

`main` and `release/**` have the same protection rules:

- Changes arrive by pull request only, with no direct pushes.
- Code owner review is required.
- The CI check must pass before merging.

### Version check on pull requests

Pull request builds fail if a declared version goes backwards relative to the base branch. Going backwards would point the plan at an older tag and publish a misleading prerelease.

```powershell
if ($env:GITHUB_BASE_REF) {
    foreach ($p in $plan) {
        $file = "src/$($p.Id)/Version.props"
        $old = git show "origin/$($env:GITHUB_BASE_REF):$file" 2> $null
        if ($LASTEXITCODE -ne 0) { continue }   # new package, nothing to compare

        $oldVersion = "$(([xml]($old -join "`n")).Project.PropertyGroup.VersionPrefix)".Trim()
        $newVersion = Get-DeclaredVersion $p.Id
        if ([version]$newVersion -lt [version]$oldVersion) {
            throw "${file}: version goes backwards ($oldVersion -> $newVersion)"
        }
    }
}
```

The comparison casts to `[version]` because string comparison puts `6.10.0` below `6.9.0`. Comparing against the pull request's base branch, not against nuget.org, is what lets `release/6.x` ship `6.1.1` while `main` declares `7.0.0`.

## References between packages

Packages in this repository always reference each other by `ProjectReference`, never by `PackageReference`. One rule covers every package, and every pull request builds and tests all packages against their siblings as they stand on `main`.

Each release therefore depends on the latest released version of every sibling it references. `Directory.Packages.props` lists dependencies from outside the organisation only.

A `ProjectReference` packs differently depending on the sibling's state in the plan:

- **Sibling is None:** the dependency is the sibling's declared version, which is already on nuget.org.
- **Sibling is Release:** the dependency is the sibling's new version, published in the same run.
- **Sibling is Prerelease:** a prerelease dependent depends on the sibling's prerelease from the same run. A Release dependent fails the build, as described under [A related set](#a-related-set).

The check follows references through other siblings, so a package is blocked by unreleased changes anywhere beneath it.

### Keeping `main` releasable

Almost every package references `Serilog`, so unreleased changes under `src/Serilog/` block the release of every other package until `Serilog` is released in the same pull request. Three practices keep that from becoming a bottleneck:

- Small core changes are released along with the package that needs to ship, by bumping `Serilog` in the same pull request.
- Long-running core work, such as the next major version, is developed on a branch and merged to `main` only when it is ready to release.
- After a core major ships, the next release of every package requires it. Fixes for users on the previous major ship from `release/<major>.x`.

## Troubleshooting

Most failures are fixed by re-running the workflow, because the plan is recomputed from nuget.org and the tags each time.

| Symptom | Cause | Fix |
| ------- | ----- | --- |
| CI failed partway through a release, with some packages pushed and others not | A transient failure during push or tagging | Re-run the failed run. Pushed packages are skipped, and the rest are published and tagged |
| Build fails with `is on nuget.org but tag ... is missing` | The push succeeded but tagging did not, or the import tag was never created | Tag the commit that version was built from by hand ([commands below](#creating-a-missing-release-tag)), then re-run |
| A package publishes a prerelease on every push although nobody changed it | Files under `src/<PackageId>/` differ from the release tag | Run `git diff <tag> HEAD -- src/<PackageId>` to see what differs. Release the package if the change is real |
| A released version is broken | A bad build was published | Unlist it on nuget.org, fix the problem, and release the next patch version |
| A version was bumped and merged by mistake | The version is already published | Unlist it on nuget.org and leave `Version.props` as it is. Reverting the bump fails the [version check](#version-check-on-pull-requests) |
| Push is rejected as unauthorised | The workflow file, repository or owner no longer matches the nuget.org policy | Compare the [policy fields](#the-nugetorg-policy) with `ci.yml` and the repository name, and update the policy |

### Creating a missing release tag

```shell
git tag Serilog.Sinks.Console/v6.1.0 <commit-the-version-was-built-from>
git push origin Serilog.Sinks.Console/v6.1.0
```

> [!IMPORTANT]
> Release tags are never moved or deleted once the version is on nuget.org. A published version cannot be replaced, so the tag must keep pointing at the source it was built from.
