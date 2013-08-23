$dataDir = Join-Path $env:AppData "Glimpse"
$dataFile = Join-Path $dataDir "extensions.cli"

function Get-VersionValue($package, $packages){
    $oldPkg = $packages | where { $_.Action -eq "Uninstall" -and $_.Id -eq $package.Id } | sort { $_.Timestamp } -des | select -first 1
    
    $result = ""
    
    if ($oldPkg -ne $nul -and $oldPkg.Version -ne $package.Version){ $result += "{0}.." -f $oldPkg.Version }
    
    return $result += $package.Version
}

# INSTALL
function Register-GlimpseExtension($package, $dte) {
    $pkgs = @()
        
    if (Test-Path $dataFile)
    {
    	$pkgs = @(Import-Clixml $dataFile | where { $_.Timestamp -gt (Get-Date).AddMinutes(-8) })
    }
      
    $pkgs += @{
                 "Id" = $package.Id;
                 "Version" = $package.Version;
                 "Timestamp" = Get-Date;
                 "Action" = "Install"
             }
    
	if (!(Test-Path $dataDir)){ md $dataDir }

    $pkgs | Export-Clixml $dataFile
    
    $queryArgs = @()
    foreach ($pkg in @($pkgs | where { $_.Action -eq "Install" } | sort { $_.Timestamp } -des))
    {
        $version = Get-VersionValue $pkg $pkgs
        $queryArgs += "{0}={1}" -f $pkg.Id, $version
    }
 
    $queryString = $queryArgs -join "&"
    $dte.ItemOperations.Navigate("http://getGlimpse.com/Version/Install/?" + $queryString)
}

Register-TabExpansion 'Register-GlimpseExtension' @{
    'package' = { 
        "$package"
    }; 
    'dte' = { 
        "$DTE"
    }; 
}

Export-ModuleMember Register-GlimpseExtension

# UNINSTALL
function Unregister-GlimpseExtension($package) {
    $pkgs = @()
        
    if (Test-Path $dataFile)
    {
    	$pkgs = @(Import-Clixml $dataFile)
    }
      
    $pkgs += @{
                 "Id" = $package.Id;
                 "Version" = $package.Version;
                 "Timestamp" = Get-Date;
                 "Action" = "Uninstall"
             }
    
	if (!(Test-Path $dataDir)){ md $dataDir }

    $pkgs | Export-Clixml $dataFile
}

Register-TabExpansion 'Unregister-GlimpseExtension' @{
    'package' = { 
        "$package"
    }; 
}

Export-ModuleMember Unregister-GlimpseExtension

