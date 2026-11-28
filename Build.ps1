# Builds, tests and packs every package in this repository. When run by the publish workflow, also
# pushes packages to nuget.org, tags releases, and creates GitHub releases. PUBLISHING.md describes
# the rules implemented here; keep the two in step.

$ErrorActionPreference = 'Stop'

Write-Output "build: Tool versions follow"

dotnet --version
dotnet --list-sdks

$isCI   = $env:GITHUB_ACTIONS -eq 'true'
$isPush = $env:GITHUB_EVENT_NAME -eq 'push'
$isMain = $env:GITHUB_REF -eq 'refs/heads/main'

# Prerelease labels sort as text, so the build number is zero-padded. The publish workflow supplies
# it; pull request and local builds are never published, and are labelled `local`.
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
    # The package ID must be lower case, or nuget.org returns 404 for a package that exists.
    $url = "https://api.nuget.org/v3-flatcontainer/$($id.ToLowerInvariant())/index.json"
    try {
        (Invoke-RestMethod $url -MaximumRetryCount 3 -RetryIntervalSec 5).versions
    }
    catch {
        if ([int]$_.Exception.Response.StatusCode -eq 404) { return }   # never published
        throw   # anything else must not be mistaken for "never published"
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

# Every sibling a package references, directly or through other siblings.
function Get-SiblingReferences([string] $id, [System.Collections.Generic.HashSet[string]] $seen) {
    Select-Xml -Path "src/$id/$id.csproj" -XPath '//ProjectReference/@Include' |
        ForEach-Object { [IO.Path]::GetFileNameWithoutExtension($_.Node.Value.Replace('\', '/')) } |
        Where-Object { $seen.Add($_) } |
        ForEach-Object { $_; Get-SiblingReferences $_ $seen }
}

Push-Location $PSScriptRoot
try {
    Write-Output "build: Planning"

    $plan = foreach ($dir in Get-ChildItem src -Directory | Sort-Object Name) {
        $id = $dir.Name
        if (-not (Test-Path "src/$id/Version.props")) { continue }

        $declared = Get-DeclaredVersion $id
        $tag      = "$id/v$declared"
        $tagged   = Test-Tag $tag

        # A tagged version is released whatever nuget.org says, so nuget.org is only asked about
        # untagged ones. `$null` means a local build could not reach it.
        $onNuGet = $false
        if (-not $tagged) {
            try {
                $onNuGet = @(Get-PublishedVersions $id) -contains $declared
            }
            catch {
                if ($isCI) { throw }
                Write-Warning "Could not reach nuget.org to check $id $declared; assuming it is published"
                $onNuGet = $null
            }
        }

        $action, $version =
            if ($tagged) {
                if (Test-ChangedSince $tag "src/$id") { 'Prerelease', "$(Step-Patch $declared)-$suffix" }
                else { 'None', $declared }
            }
            elseif ($onNuGet -eq $false) { 'Release', $declared }
            elseif ($isCI) { throw "$id $declared is on nuget.org but tag $tag is missing" }
            else {
                # Clones of forks often lack release tags. Local builds carry on with a best guess.
                Write-Warning "Tag $tag not found, so changes to $id cannot be detected. Fetch tags from serilog/serilog for an accurate plan."
                'Prerelease', "$(Step-Patch $declared)-$suffix"
            }

        [pscustomobject]@{ Id = $id; Action = $action; Version = $version; Tag = $tag }
    }

    $plan | Format-Table Id, Action, Version | Out-String | Write-Output

    # Show the plan on the run and on pull requests.
    if ($env:GITHUB_STEP_SUMMARY) {
        $lines = @('### Publishing plan', '')
        $lines += $plan | Where-Object Action -ne 'None' |
            ForEach-Object { "- **$($_.Action)**: $($_.Id) $($_.Version)" }
        if ($lines.Count -eq 2) { $lines += 'No packages have changed since their last release.' }
        $lines | Add-Content $env:GITHUB_STEP_SUMMARY
    }

    # On pull requests, a declared version must not go backwards relative to the base branch.
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

    # A stable package must not depend on unreleased changes in any sibling.
    foreach ($p in $plan | Where-Object Action -eq 'Release') {
        $refs = @(Get-SiblingReferences $p.Id ([System.Collections.Generic.HashSet[string]]::new()))
        $unreleased = $plan | Where-Object { $_.Id -in $refs -and $_.Action -eq 'Prerelease' }
        if ($unreleased) {
            throw "$($p.Id) $($p.Version) references unreleased changes in $($unreleased.Id -join ', ')"
        }
    }

    # Hand each project its resolved version (imported by Directory.Build.props).
    if (Test-Path ./artifacts) {
        Write-Output "build: Cleaning ./artifacts"
        Remove-Item ./artifacts -Force -Recurse
    }
    New-Item artifacts/versions -ItemType Directory | Out-Null
    foreach ($p in $plan) {
        "<Project><PropertyGroup><Version>$($p.Version)</Version></PropertyGroup></Project>" |
            Set-Content "artifacts/versions/$($p.Id).props"
    }

    Write-Output "build: Build started"

    dotnet build -c Release /p:ContinuousIntegrationBuild=true
    if ($LASTEXITCODE -ne 0) { throw 'Build failed' }

    foreach ($test in Get-ChildItem test/*.Tests) {
        Write-Output "build: Testing project in $test"

        dotnet test $test -c Release --no-build --no-restore
        if ($LASTEXITCODE -ne 0) { throw 'Testing failed' }
    }

    # Pack everything that is not 'None', on every build, so pull requests catch packing errors.
    foreach ($p in $plan | Where-Object Action -ne 'None') {
        Write-Output "build: Packaging $($p.Id) $($p.Version)"

        dotnet pack "src/$($p.Id)" -c Release --no-build -o artifacts/packages
        if ($LASTEXITCODE -ne 0) { throw "Packaging failed for $($p.Id)" }

        if (-not (Test-Path "artifacts/packages/$($p.Id).$($p.Version).nupkg")) {
            throw "Packaging $($p.Id) did not produce version $($p.Version)"
        }
    }

    # The publish workflow supplies the key on pushes only; pull requests never receive one.
    if (-not ($isPush -and $env:NUGET_API_KEY)) { return }

    # Releases from main and release branches; prereleases from main only.
    $toPublish = $plan | Where-Object {
        $_.Action -eq 'Release' -or ($_.Action -eq 'Prerelease' -and $isMain)
    }

    foreach ($p in $toPublish) {
        Write-Output "build: Publishing $($p.Id) $($p.Version)"

        $nupkg = "artifacts/packages/$($p.Id).$($p.Version).nupkg"

        dotnet nuget push $nupkg `
            --source https://api.nuget.org/v3/index.json `
            --api-key $env:NUGET_API_KEY `
            --skip-duplicate
        if ($LASTEXITCODE -ne 0) { throw "Push failed for $($p.Id) $($p.Version)" }

        if ($p.Action -ne 'Release') { continue }

        # Tag only after the push succeeds: the plan relies on a tag meaning the version was pushed.
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
} finally {
    Pop-Location
}
