Push-Location $PSScriptRoot

Remove-Item $PSScriptRoot\results\* -Recurse -Force

./Build.ps1

$destinationPath = ($PSScriptRoot + "/results/");

foreach ($test in ls .\test\*.PerformanceTests) {
    Push-Location $test

	echo "perf: Clean old results and logs in $test";
    if (Test-Path "./BenchmarkDotNet.Artifacts/") { Remove-Item "./BenchmarkDotNet.Artifacts/" -Recurse -Force }

	echo "perf: Running performance test project in $test";
    & dotnet run -c Release --framework netcoreapp3.1 -- --filter *

    if($LASTEXITCODE -ne 0) {
        Pop-Location
        exit 2
    }

	echo "perf: Copying performance test results";

	foreach ($resultFile in Get-ChildItem "./BenchmarkDotNet.Artifacts/results/" -Recurse -Filter *.md) {
		$newFileName = $resultFile.Name.Replace($test.Name + ".", "").Replace("-report-github.md", ".md");
		$fullDestination = ($destinationPath + "/" + $newFileName);
		Copy-Item $resultFile.FullName -Destination $fullDestination;
	}

    Pop-Location
}

Pop-Location
