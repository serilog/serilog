Push-Location $PSScriptRoot

./Build.ps1

foreach ($test in ls benchmark/*.PerformanceTests) {
    Push-Location $test

	echo "perf: Running performance test project in $test"

    & dotnet test -c Release
    if($LASTEXITCODE -ne 0) { exit 2 }

    Pop-Location
}

Pop-Location
