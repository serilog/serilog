# Releasing packages from `serilog/serilog`

> This is a machine-generated, human-readable overview of the project setup and publishing processes. It's here to ease
> the migration of our satellite projects back into this repository. Once that work is complete, we can reevaluate what
> we need to document here, and consider replacing it with a handwritten maintainer guide.

Each package's `Version.props` declares the newest version that should exist on NuGet, and merging a change to that file into `main` publishes the package. Every other push to `main` publishes `latest + 0.0.1` prereleases of only the packages whose source changed.

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

Each package lives in a directory named exactly after its package ID. That name is also the project file name, the release tag prefix and the key `Build.ps1` uses throughout. Only files under `src/<PackageId>/` count as changes to a package, which is why each package's README lives there and not at the repository root.

```text
serilog/
  Build.ps1
  Directory.Build.props
  .github/
    CODEOWNERS
    workflows/
      ci.yml              builds and tests pull requests
      publish.yml         builds, tests and publishes pushes
  src/
    Serilog/
      Serilog.csproj
      Version.props
      README.md           packed into the package
    Serilog.Sinks.Console/
      Serilog.Sinks.Console.csproj
      Version.props
      README.md
  test/
    Serilog.Sinks.Console.Tests/     run by every build; the directory name must end in `.Tests`
  benchmark/
    Serilog.PerformanceTests/        built by every build, run by `RunPerfTests.ps1` only
  example/
    Serilog.Expressions.Sample/
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

The root `Directory.Build.props` imports the declared version, then the resolved version for the current build if `Build.ps1` has written one. Builds that bypass `Build.ps1`, such as `dotnet build` or an IDE, get the declared version when the generated file is absent.

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

When a package is first imported into the monorepo, tag the commit that the version already on nuget.org was built from:

```shell
git tag Serilog.Sinks.Console/v6.0.0 <commit>
git push origin Serilog.Sinks.Console/v6.0.0
```

Which commit that is depends on how the package was imported:

- **With its history**, the commit is the one the original repository released from. Changes made since that release then show up as a Prerelease, as they should. The package must keep its `src/<PackageId>/` path for the comparison to work.
- **As a snapshot**, the commit is the import commit, and the snapshot must be the released source.

## Releasing

### One package

1. Open a pull request that changes `src/<PackageId>/Version.props` to the version to ship. Keep it separate from code changes so the release is obvious in review.
2. Read the plan in the pull request's build summary. It should list that package as Release with the expected version, and no other package as Release.
3. Get code owner approval and merge to `main`.
4. CI publishes the package, tags the merge commit and creates the GitHub release. The version appears on nuget.org a few minutes later.

### A related set

Bump the `Version.props` of every package in the set in one pull request. All of them are published from the same commit in the same CI run.

Packages in this repository reference each other by `ProjectReference`, which adds one rule: a package cannot be released while a sibling it references, directly or through other siblings, has unreleased changes. The build fails and names the sibling, and the fix is to bump the sibling in the same pull request.

### A package without its siblings

When a package has to ship but a sibling it references has unreleased changes that are not ready, the package can be released against the sibling's last release instead:

1. In one pull request, replace the `ProjectReference` to the sibling with a `PackageReference` to a version of it that is already on nuget.org, and bump the package's `Version.props`.
2. After it is published, restore the `ProjectReference` in a follow-up pull request.

The sibling check only follows `ProjectReference`, so it no longer applies. Restore fails if the pinned version is not on nuget.org, but nothing else checks the pin, so the reviewer confirms it is the minimum version the package should require. The package's tests run against the pinned version, not the sibling as it stands on `main`.

Once the `ProjectReference` is restored, the package's prereleases depend on the sibling's prereleases again, and its next release needs either a release of the sibling or this procedure again.

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

$isCI   = $env:GITHUB_ACTIONS -eq 'true'
$isPush = $env:GITHUB_EVENT_NAME -eq 'push'
$isMain = $env:GITHUB_REF -eq 'refs/heads/main'
$suffix = if ($isPush -and $env:CI_BUILD_NUMBER) { 'dev-{0:d5}' -f [int]$env:CI_BUILD_NUMBER } else { 'local' }

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

Five details in the helpers are easy to get wrong:

- The package ID in the nuget.org URL must be lower case, or the request returns 404 for a package that exists.
- Only a 404 means "never published". A timeout or server error that was treated the same way would turn every package into a Release.
- The build number is zero-padded because prerelease labels sort as text. Unpadded, `dev-999` sorts above `dev-1000`.
- The build number is the workflow run number plus a fixed offset, set in `publish.yml`. The offset keeps new prereleases sorting above those published before the monorepo, such as `4.4.1-dev-02453`. Pull request and local builds are labelled `local`.
- The tag and diff checks need full history and tags, which the workflow's checkout step must fetch.

### The plan

```powershell
$plan = foreach ($dir in Get-ChildItem src -Directory | Sort-Object Name) {
    $id = $dir.Name
    if (-not (Test-Path "src/$id/Version.props")) { continue }

    $declared = Get-DeclaredVersion $id
    $tag      = "$id/v$declared"
    $tagged   = Test-Tag $tag
    $onNuGet  = -not $tagged -and @(Get-PublishedVersions $id) -contains $declared

    $action, $version =
        if ($tagged) {
            if (Test-ChangedSince $tag "src/$id") { 'Prerelease', "$(Step-Patch $declared)-$suffix" }
            else { 'None', $declared }
        }
        elseif (-not $onNuGet) { 'Release', $declared }
        else { throw "$id $declared is on nuget.org but tag $tag is missing" }

    [pscustomobject]@{ Id = $id; Action = $action; Version = $version; Tag = $tag }
}
```

A tagged version is released whatever nuget.org says, so nuget.org is only asked about untagged versions. A build of tagged packages therefore needs no network access to plan.

The plan treats a version as released when it is on nuget.org or tagged, which gives four cases:

| On nuget.org | Tagged | Meaning                                             | Result                                                |
| ------------ | ------ | --------------------------------------------------- | ----------------------------------------------------- |
| No           | No     | Not released yet                                    | Release                                               |
| Yes          | Yes    | Released                                            | Prerelease or None, by diff against the tag           |
| No           | Yes    | Released moments ago, nuget.org still indexing      | Prerelease or None, by diff against the tag           |
| Yes          | No     | Tagging failed, or the import tag was never created | Build fails; see [Troubleshooting](#troubleshooting)  |

The third case matters because nuget.org takes a few minutes to list a new version. Without the tag check, a second push inside that window would plan the same release again.

Local builds soften the fourth case, and a failure to reach nuget.org, into a warning and a `local` prerelease version. Clones of forks often lack the release tags, and a contributor's build should not fail for that. CI builds always fail.

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

dotnet build -c Release /p:ContinuousIntegrationBuild=true
if ($LASTEXITCODE -ne 0) { throw 'Build failed' }

foreach ($test in Get-ChildItem test/*.Tests) {
    dotnet test $test -c Release --no-build --no-restore
    if ($LASTEXITCODE -ne 0) { throw 'Testing failed' }
}
```

The build covers every project in `Serilog.sln`, but only projects in `test/` whose names end in `.Tests` are run. Benchmarks and test support projects are built so they cannot rot, and are not run.

The `Replace('\', '/')` in the reference check is needed on Linux runners, where a Windows-style `Include` path is otherwise read as one long file name.

## `Build.ps1`: publishing

After the build and tests pass, `Build.ps1` packs every package the plan marks as changed, then pushes and tags only on a push build with an API key present. Only `publish.yml` supplies a key.

```powershell
# Pack everything that is not 'None', on every build, so pull requests catch packing errors.
foreach ($p in $plan | Where-Object Action -ne 'None') {
    dotnet pack "src/$($p.Id)" -c Release --no-build -o artifacts/packages
    if ($LASTEXITCODE -ne 0) { throw "Packaging failed for $($p.Id)" }

    if (-not (Test-Path "artifacts/packages/$($p.Id).$($p.Version).nupkg")) {
        throw "Packaging $($p.Id) did not produce version $($p.Version)"
    }
}

if (-not ($isPush -and $env:NUGET_API_KEY)) { return }

# Releases from main and release branches; prereleases from main only.
$toPublish = $plan | Where-Object {
    $_.Action -eq 'Release' -or ($_.Action -eq 'Prerelease' -and $isMain)
}

foreach ($p in $toPublish) {
    $nupkg = "artifacts/packages/$($p.Id).$($p.Version).nupkg"

    dotnet nuget push $nupkg `
        --source https://api.nuget.org/v3/index.json `
        --api-key $env:NUGET_API_KEY `
        --skip-duplicate
    if ($LASTEXITCODE -ne 0) { throw "Push failed for $($p.Id) $($p.Version)" }

    if ($p.Action -ne 'Release') { continue }

    if (-not (Test-Tag $p.Tag)) {
        git tag $p.Tag
        if ($LASTEXITCODE -ne 0) { throw "Could not create tag $($p.Tag)" }
        git push origin $p.Tag
        if ($LASTEXITCODE -ne 0) { throw "Could not push tag $($p.Tag)" }
    }

    gh release view $p.Tag *> $null
    if ($LASTEXITCODE -ne 0) {
        gh release create $p.Tag --verify-tag `
            --title "$($p.Id) $($p.Version)" `
            --notes "https://www.nuget.org/packages/$($p.Id)/$($p.Version)" `
            $nupkg ($nupkg -replace '\.nupkg$', '.snupkg')
        if ($LASTEXITCODE -ne 0) { throw "Could not create GitHub release $($p.Tag)" }
    }
}
```

The order and the flags here carry the reliability of the whole process:

- Each package is tagged immediately after its own push succeeds, and never before. The plan relies on a tag meaning the version was pushed.
- `--skip-duplicate` makes a re-run safe after a partial failure. Packages already pushed are skipped, and the rest are published and tagged.
- The tag and GitHub release steps check before creating, for the same reason.
- The check after packing catches a project that overrides its version and would otherwise publish under the wrong one.
- The package and its symbols are attached to the GitHub release, as they were before the monorepo.
- `gh release create --generate-notes` is not used. In a monorepo it lists every pull request since the previous release of any package.

## Workflows and trusted publishing

Two workflow files run `Build.ps1`. `.github/workflows/ci.yml` builds pull requests, with read-only permissions and no key. `.github/workflows/publish.yml` builds and publishes every push to `main` and `release/**`. Keeping all publishing in that one file is what lets a single nuget.org policy cover every package.

```yaml
name: Publish

on:
  push:
    branches: [ "main", "release/**" ]

concurrency:
  group: publish-${{ github.ref }}
  cancel-in-progress: false

env:
  CI_BUILD_NUMBER_BASE: ${{ github.run_number }}

jobs:
  publish:
    runs-on: windows-latest
    permissions:
      id-token: write   # NuGet trusted publishing
      contents: write   # push release tags, create GitHub releases
    steps:
      - uses: actions/checkout@v7
        with:
          fetch-depth: 0
          fetch-tags: true

      - name: Setup
        uses: actions/setup-dotnet@v6
        with:
          global-json-file: global.json
          dotnet-version: |
            8.0.x
            9.0.x
            10.0.x

      - name: Compute build number
        shell: bash
        run: |
          echo "CI_BUILD_NUMBER=$(($CI_BUILD_NUMBER_BASE+2500))" >> $GITHUB_ENV

      - name: Log in to NuGet
        uses: NuGet/login@v1
        id: login
        with:
          user: ${{ secrets.NUGET_USER }}

      - name: Build and Publish
        env:
          NUGET_API_KEY: ${{ steps.login.outputs.NUGET_API_KEY }}
          GH_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        shell: pwsh
        run: |
          ./Build.ps1
```

The parts of the workflows that must not drift:

- Both workflows run on `windows-latest`. The .NET Framework targets are only built and tested on Windows, and a package packed elsewhere ships without them.
- `fetch-depth: 0` and `fetch-tags: true`, in both workflows, give `Build.ps1` the history and tags it diffs against. A shallow checkout makes every tagged package look untagged.
- `setup-dotnet` installs the SDK named in `global.json`, plus a runtime for each target framework the tests run on. The runner image is not relied on for these, because it drops runtimes as they go out of support.
- The concurrency group stops two publishes from the same branch overlapping, and a publish that has started is never cancelled. A run still waiting its turn is replaced by a newer one, which is safe because the newer run plans from the same tags and publishes whatever is outstanding. Pull request builds in `ci.yml` are cancelled by newer commits.
- Pull requests, including those from forks, never receive a key, because `ci.yml` has no login step and no `id-token` permission.
- The `2500` offset must never decrease. Run numbers for `publish.yml` started again from 1, and the offset clears the highest build number published before the monorepo. Renaming the workflow file resets the run number again, so the offset must be raised at the same time.
- The key from `NuGet/login` is valid for one hour. If the build and tests ever approach its lifetime, move the push into a separate step that follows a later login.

### The nuget.org policy

The [trusted publishing](https://learn.microsoft.com/nuget/nuget-org/trusted-publishing) policy is created once on nuget.org with these values:

| Field            | Value                                |
| ---------------- | ------------------------------------ |
| Policy owner     | The `serilog` organisation           |
| Repository owner | `serilog`                            |
| Repository       | `serilog`                            |
| Workflow file    | `publish.yml` (file name only, no path) |
| Environment      | Left blank                           |

Renaming `publish.yml`, or moving the publish step into another workflow, breaks publishing until the policy is updated. `NUGET_USER` is a nuget.org profile name, not an email address.

## Guardrails

Merging a `Version.props` change is the release, so the pull request review is the release approval. Three things enforce that.

### Code owners

`.github/CODEOWNERS` routes every file that can cause or alter a publish to the `@serilog/publishers` team.

```text
# Merging a change to any of these can publish a package.
**/Version.props          @serilog/publishers
/Build.ps1                @serilog/publishers
/Directory.Build.props    @serilog/publishers
/.github/                 @serilog/publishers
```

### Branch protection

`main` and `release/**` have the same protection rules:

- Changes arrive by pull request only, with no direct pushes.
- Code owner review is required.
- The `CI` check, from `ci.yml`, must pass before merging.

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

Packages in this repository reference each other by `ProjectReference`, never by `PackageReference`, except briefly while [releasing a package without its siblings](#a-package-without-its-siblings). One rule covers every package, and every pull request builds and tests all packages against their siblings as they stand on `main`.

Each release therefore depends on the latest released version of every sibling it references.

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
| Push is rejected as unauthorised | The workflow file, repository or owner no longer matches the nuget.org policy | Compare the [policy fields](#the-nugetorg-policy) with `publish.yml` and the repository name, and update the policy |

### Creating a missing release tag

```shell
git tag Serilog.Sinks.Console/v6.1.0 <commit-the-version-was-built-from>
git push origin Serilog.Sinks.Console/v6.1.0
```

> [!IMPORTANT]
> Release tags are never moved or deleted once the version is on nuget.org. A published version cannot be replaced, so the tag must keep pointing at the source it was built from.
