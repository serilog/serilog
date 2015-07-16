properties {
    $projects = $null
    $configuration = "Release"
    $source_folder = $null
	$build_folder = $null
	$deploy_folder = $null
    $solutions = $null
	$build_meta = $null
	$version_override = $null
}

Task Default -Depends Info, Build

Task Info {
	Write-Host "Configuration: $configuration" -ForegroundColor Yellow
	Write-Host "Source Folder: $source_folder" -ForegroundColor Yellow
	Write-Host "Build Folder: $build_folder" -ForegroundColor Yellow
	Write-Host "Deploy Folder: $deploy_folder" -ForegroundColor Yellow
	Write-Host "Solutions: $solutions" -ForegroundColor Yellow
	Write-Host "Version Override: $version_override" -ForegroundColor Yellow
	Write-Host "Build Meta: $build_meta" -ForegroundColor Yellow
	Write-Host "Projects: $projects" -ForegroundColor Yellow
}

Task RestorePackages {
    $solutions | % {
        Get-ChildItem $_ | % {
        	Exec { nuget restore $_ -PackagesDirectory .\packages }
        }
    }
}

Task Publish -Depends Package {
    $version = getVersionBase
    
    $projects | % {
        Get-ChildItem | Where-Object -FilterScript {
            ($_.Name.Contains("$project.$version")) -and !($_.Name.Contains(".symbols")) -and ($_.Extension -eq '.nupkg')    
        } | % {
            exec { nuget push $_.Fullname }
        }
    }
}

# -Depends Test
Task Package  {
    $version = getVersionBase;
	if (-not ($build_meta -eq $null)) {
		$version = $version + "-" + $build_meta
	}

	New-Item -ItemType Directory -Force -Path $build_folder -ErrorAction SilentlyContinue

    $projects | % {
        Get-ChildItem -Path "$_\*.nuspec" | % {
			Write-Host "Creating Nuget Package for version ($version)"
            exec { nuget pack -sym $_.Fullname -OutputDirectory $build_folder -version $version -Prop Configuration=$configuration }
        }        
    }

    Get-ChildItem $source_folder -Recurse -Include *.nuspec | % {
		Write-Host "Creating Nuget Package for version ($version)"
        Write-Host "nuget pack -sym $_ -OutputDirectory $build_folder -version $version -Prop Configuration=$configuration"
        exec { nuget pack -sym $_ -OutputDirectory $build_folder -version $version -Prop Configuration=$configuration }
    }
}

Task Test -Depends Build {
    Get-ChildItem $source_folder -Recurse -Include *Tests.csproj | % {
        try {
            Exec { & "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe" /testcontainer:"$($_.DirectoryName)\bin\$configuration\$($_.BaseName).dll" }
        }
        catch {
        }
    }
}

Task Build -Depends Clean,Set-Versions,RestorePackages {
    $solutions | % {
        Get-ChildItem $_ | % {
            Exec { msbuild "$_" /t:Build /p:Configuration=$configuration } 
        }
    }
}

Task Clean {
    $solutions | % {
        Get-ChildItem $_ | % {
            Exec { msbuild "$_" /t:Clean /p:Configuration=$configuration } 
        }
    }
}

Task Set-Versions {
    $version = getVersionBase

	if ($build_meta) {
        "##teamcity[buildNumber '$version+$build_meta']" | Write-Host
    } else {
		"##teamcity[buildNumber '$version']" | Write-Host
	}

    Get-ChildItem -Recurse -Force | Where-Object { $_.Name -like "CommonAssemblyInfo.cs" } | ForEach-Object {
        (Get-Content $_.FullName) | ForEach-Object {
            ($_ -replace 'AssemblyVersion\(.*\)', ('AssemblyVersion("' + $version + '")')) -replace 'AssemblyFileVersion\(.*\)', ('AssemblyFileVersion("' + $version + '")')
        } | Set-Content $_.FullName -Encoding UTF8
    }    
}

function getVersionBase {
	if ($version_override -eq $null) {
		$versionInfo = (Get-Content "version.json") -join "`n" | ConvertFrom-Json
			"$($versionInfo.major).$($versionInfo.minor).$($versionInfo.patch)";    
	} else {
		$version_override
	}
}