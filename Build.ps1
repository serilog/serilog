param(
    [String] $majorMinor = "0.0",  # 1.4
    [String] $patch = "0",         # $env:APPVEYOR_BUILD_VERSION
    [String] $customLogger = "",   # C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll
    [Switch] $notouch
)

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
        Where-Object { -not ($_.Name -eq "Serilog.Sinks.Seq.csproj") } |
        Where-Object { -not ($_.Name -eq "Serilog.Sinks.Splunk.csproj") } |
        ForEach-Object { Invoke-NuGetPackProj $_ }

    pushd .\src\Serilog
    Invoke-NuGetPackSpec "Serilog.nuspec" $version
    popd

    pushd .\src\Serilog.Sinks.Seq
    Invoke-NuGetPackSpec "Serilog.Sinks.Seq.nuspec" $version
    popd

    pushd .\src\Serilog.Sinks.Splunk
    Invoke-NuGetPackSpec "Serilog.Sinks.Splunk.nuspec" $version
    popd
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

    Install-NuGetPackages
    
    Invoke-MSBuild "Serilog-net40.sln" $customLogger
    Invoke-MSBuild "Serilog.sln" $customLogger

    Invoke-NuGetPack $package
}

$ErrorActionPreference = "Stop"
Invoke-Build $majorMinor $patch $customLogger $notouch
