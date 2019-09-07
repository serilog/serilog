Push-Location $PSScriptRoot

Remove-Item $PSScriptRoot/results/* -Recurse -Force

./Build.ps1

foreach ($test in ls ./test/*.PerformanceTests) {
    Push-Location $test

	echo "perf: Running performance test project in $test";

    & dotnet test -c Release
    if($LASTEXITCODE -ne 0) { exit 2 }

	echo "perf: Copying performance test results";
	
	foreach ($sdkResult in Get-ChildItem ./bin/Release/) {
		$destinationPath = ($PSScriptRoot + "/results/" + $sdkResult.Name);
		mkdir $destinationPath > $null;
		foreach ($resultFile in Get-ChildItem ($sdkResult.FullName + "/") -Recurse -Filter *.md) {
			$newFileName = $resultFile.Name.Replace($test.Name + ".", "").Replace("-report-github.md", ".md");
			$fullDestination = ($destinationPath + "/" + $newFileName);
			Copy-Item $resultFile.FullName -Destination $fullDestination;
		}
	}

    Pop-Location
}

Pop-Location
