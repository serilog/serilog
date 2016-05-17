$root = $(Get-Item $($MyInvocation.MyCommand.Path)).DirectoryName

function Install-DotNetCli
{
    & where.exe dotnet 2>&1 | Out-Null
    if(($LASTEXITCODE -ne 0) -Or ((Test-Path Env:\APPVEYOR) -eq $true))
    {
        Write-Host ".NET CLI not found, installing"
        &{$Branch='dev';iex ((New-Object net.webclient).DownloadString('https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0/scripts/obtain/install.ps1'))}
    }
}

function Get-DotNetSdkVersion
{
    $globalJson = Join-Path $PSScriptRoot "global.json"
    $jsonData = Get-Content -Path $globalJson -Raw | ConvertFrom-JSON
    return $jsonData.sdk.version
}

function Remove-PathVariable
{
    param([string] $VariableToRemove)
    $path = [Environment]::GetEnvironmentVariable("PATH", "User")
    $newItems = $path.Split(';') | Where-Object { $_.ToString() -inotlike $VariableToRemove }
    [Environment]::SetEnvironmentVariable("PATH", [System.String]::Join(';', $newItems), "User")
    $path = [Environment]::GetEnvironmentVariable("PATH", "Process")
    $newItems = $path.Split(';') | Where-Object { $_.ToString() -inotlike $VariableToRemove }
    [Environment]::SetEnvironmentVariable("PATH", [System.String]::Join(';', $newItems), "Process")
}

Push-Location $PSScriptRoot

$dnxVersion = Get-DotNetSdkVersion

# Clean
if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

# Remove the installed dotnet from the path and force use of
# per-user dotnet (which we can upgrade as needed without admin permissions)
# Remove-PathVariable "*Program Files\dotnet*"

Install-DotNetCli

& dotnet restore

$revision = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];

Push-Location src/Serilog

& dotnet pack -c Release -o ..\..\.\artifacts --version-suffix=$revision
if($LASTEXITCODE -ne 0) { exit 1 }    

Pop-Location

Push-Location test/Serilog.Tests

& dotnet test
if($LASTEXITCODE -ne 0) { exit 2 }

Pop-Location

Pop-Location
