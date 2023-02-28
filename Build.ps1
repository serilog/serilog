Write-Output "build: Build started"

Push-Location $PSScriptRoot

if(Test-Path .\artifacts) {
    Write-Output "build: Cleaning .\artifacts"
    Remove-Item .\artifacts -Force -Recurse
}

$branch = @{ $true = $env:APPVEYOR_REPO_BRANCH; $false = $(git symbolic-ref --short -q HEAD) }[$NULL -ne $env:APPVEYOR_REPO_BRANCH];
$revision = @{ $true = "{0:00000}" -f [convert]::ToInt32("0" + $env:APPVEYOR_BUILD_NUMBER, 10); $false = "local" }[$NULL -ne $env:APPVEYOR_BUILD_NUMBER];
$suffix = @{ $true = ""; $false = "$($branch.Substring(0, [math]::Min(10,$branch.Length)))-$revision"}[$branch -eq "main" -and $revision -ne "local"]
$commitHash = $(git rev-parse --short HEAD)
$buildSuffix = @{ $true = "$($suffix)-$($commitHash)"; $false = "$($branch)-$($commitHash)" }[$suffix -ne ""]

Write-Output "build: Package version suffix is $suffix"
Write-Output "build: Build version suffix is $buildSuffix"

& dotnet build --configuration Release --version-suffix=$buildSuffix /p:ContinuousIntegrationBuild=true

if($LASTEXITCODE -ne 0) { exit 1 }

if($suffix) {
    & dotnet pack --configuration Release --no-build --no-restore -o ..\..\artifacts --version-suffix=$suffix
} else {
    & dotnet pack --configuration Release --no-build --no-restore -o ..\..\artifacts
}

if($LASTEXITCODE -ne 0) { exit 2 }

Write-Output "build: Testing"

& dotnet test --configuration Release --no-build --no-restore

if($LASTEXITCODE -ne 0) { exit 3 }