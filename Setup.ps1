$ErrorActionPreference = "Stop"

$RequiredDotnetVersion =  $(cat ./global.json | convertfrom-json).sdk.version

mkdir "./build"
Invoke-WebRequest "https://dot.net/v1/dotnet-install.ps1" -OutFile "./build/installcli.ps1"
& ./build/installcli.ps1 -InstallDir "$pwd/.dotnetcli" -NoPath -Version $RequiredDotnetVersion
if ($LASTEXITCODE) { exit 1 }

$env:Path = "$pwd/.dotnetcli;$env:Path"
