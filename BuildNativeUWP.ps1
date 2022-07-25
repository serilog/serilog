param(
    [String] $customLogger = "C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll",
    [String] $proj = "test/Serilog.UwpTests"
)

echo "build: running .NET Native toolchain for UWP test project"

$msbuild = $(cmd /c where /R 'C:\Program Files (x86)\Microsoft Visual Studio\2022' msbuild.exe | select -First 1)

if ($customLogger) {
    & $msbuild $proj /logger:"$customLogger" /verbosity:minimal /t:Restore
    & $msbuild $proj /logger:"$customLogger" /verbosity:minimal /t:Build /p:Configuration=Release
}
else {
    & $msbuild $proj /verbosity:minimal /t:Restore
    & $msbuild $proj /verbosity:minimal /t:Build /p:Configuration=Release
}

if ($LASTEXITCODE -ne 0) { exit 4 }
