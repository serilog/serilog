param(
    [String]   $majorMinor = "0.0",  # 1.4
    [String]   $patch = "0",         # $env:APPVEYOR_BUILD_VERSION
    [String]   $customLogger = "",   # C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll
    [Switch]   $notouch,             # When not set, Major & Minor assembly versions are updated
    [Alias("r")]
    [String]   $runtime,             # KRE Runtime (default: $null > MSBuild)
    [Switch]   $x86 = $false,        # KRE Arch (default: x86)
    [Switch]   $amd64 = $false,      # KRE Arch (default: x86)
    [String[]] $args=@()             # KRE Version (default: 1.0.0-beta1)
)

$selectedKreVersion = $null
$defaultKreVersion ="1.0.0-beta1"
$selectedKreArch=$null
$defaultKreArch="x86"

function Set-AssemblyVersions($informational, $assembly)
{
    (Get-Content assets/CommonAssemblyInfo.cs) |
        ForEach-Object { $_ -replace """1.0.0.0""", """$assembly""" } |
        ForEach-Object { $_ -replace """1.0.0""", """$informational""" } |
        ForEach-Object { $_ -replace """1.1.1.1""", """$($informational).0""" } |
        Set-Content assets/CommonAssemblyInfo.cs
}

function Install-NuGetPackages()
{
    nuget restore Serilog.sln
}

function Invoke-MSBuild($solution, $customLogger)
{
    if ($customLogger)
    {
        msbuild "$solution" /verbosity:minimal /p:Configuration=Release /logger:"$customLogger"
    }
    else
    {
        msbuild "$solution" /verbosity:minimal /p:Configuration=Release
    }
}

function Invoke-KpmBuildProj($projectDir, $solutionDir)
{
    pushd (Split-Path $projectDir)
    & kpm build --configuration Release
    Copy-Item .\bin\Release\*.nupkg $solutionDir
    popd
}

function Invoke-Kpm()
{
    $solutionDir = (Get-Item -path .\).FullName
    ls src/**/project.json |
        ForEach-Object { Invoke-KpmBuildProj $_  $solutionDir}
}

function Invoke-NuGetPackProj($csproj)
{
    nuget pack -Prop Configuration=Release -Symbols $csproj
}

function Invoke-NuGetPackSpec($nuspec, $version)
{
    nuget pack $nuspec -Version $version -OutputDirectory ..\..\
}

function Invoke-NuGetPack($version)
{
    ls src/**/*.csproj |
        Where-Object { -not ($_.Name -like "*net40*") } |
        Where-Object { -not ($_.Name -like "*FullNetFx*") } |
        Where-Object { -not ($_.Name -eq "Serilog.csproj") } |
        ForEach-Object { Invoke-NuGetPackProj $_ }

    pushd .\src\Serilog
    Invoke-NuGetPackSpec "Serilog.nuspec" $version
    popd
}

function Install-Kvm()
{
    $installSource = "https://raw.githubusercontent.com/aspnet/Home/master/kvminstall.ps1"
    $installTarget = Join-Path (Get-Item -path .\).FullName (Split-Path $installSource -leaf)
    $webClient = New-Object System.Net.WebClient
    $webClient.DownloadFile($installSource, $installTarget)
    & $installTarget
    Remove-Item $installTarget
}

function Install-Kre()
{
	$krePath = Join-Path $env:UserProfile "\.kre"
	$env:KRE_HOME = $krePath
    $kvmPath = Join-Path $krePath "\bin\kvm.cmd"
    Write-Output "Using KRE-$runtime-$selectedKreArch.$selectedKreVersion"
    & $kvmPath "install $selectedKreVersion -$selectedKreArch -r $runtime"
    & $kvmPath "use $selectedKreVersion -$selectedKreArch -r $runtime"
}

function Invoke-Build($majorMinor, $patch, $customLogger, $notouch)
{
    $package="$majorMinor.$patch"

    Write-Output "Building Serilog $package"

    if (-not $notouch)
    {
        $assembly = "$majorMinor.0.0"

        Write-Output "Assembly version will be set to $assembly"
        Set-AssemblyVersions $package $assembly
    }

    if ($runtime)
    {
        Install-Kvm
        Install-Kre

        Invoke-Kpm
    }
    else
    {
		Install-NuGetPackages
    
		Invoke-MSBuild "Serilog-net40.sln" $customLogger
		Invoke-MSBuild "Serilog.sln" $customLogger

		Invoke-NuGetPack $package
    }
}

function Sanitise-Parameters()
{	
    if ($runtime)
    {
        $validRuntimes = "CoreCLR", "CLR"
        $match = $validRuntimes | ? { $_ -like $Runtime } | Select -First 1
        if (!$match) { throw "'$runtime' is not a valid runtime" }
        if ($x86 -and $amd64) { throw "Cannot use both amd64 and x86." }
        if ($amd64) { Set-Variable -name "selectedKreArch" -value "amd64" -scope script }
        else { Set-Variable -name "selectedKreArch" -value $defaultKreArch -scope script }
        if ($args.Count -ne 1) { Set-Variable -name "selectedKreVersion" -value $defaultKreVersion -scope script }
        else { Set-Variable -name "selectedKreVersion" -value $args[0] -scope script }
    }
    else
    {
    # Should be no vNext parameters
        if ($x86 -or $amd64) { Write-Output "Cannot apply architecture for MSBuild." }
        if ($args.Count -ne 0) { $args | foreach { Write-Output "Cannot parse $_." } }
    }
}

$ErrorActionPreference = "Stop"
Sanitise-Parameters
Invoke-Build $majorMinor $patch $customLogger $notouch