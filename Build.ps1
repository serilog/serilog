Param(
    [Parameter(Position=1,Mandatory=0)]
    [string[]]$task_list = @(),

	[Parameter()]
	[string]$Configuration = "Release",

	[Parameter()]
    [string]$BuildMetaData,

	[Parameter()]
    [string]$version,

	[Parameter()]
    [string]$patch
)

$build_file = 'default.ps1'

# Properties for the psake build script
$properties = @{

    # Build configuration to use
    "configuration" = $Configuration;

    # Version number to use if running the Publish build task.
    # This will be read from the command line args
    "version_override"       = $version;

	# Build number metadata that will be appended to main version number (patch level, pre-build, etc)
	"build_meta" = $patch;
	
    # Path to the solution file
    "solutions"  = @('Serilog.sln', 'Serilog-net40.sln');

    # Folder containing source code
    "source_folder" = '';

    # Folder to output deployable packages to. This folder should be ignored
    # from any source control, as we don't commit build artefacts to source
    # control
    "deploy_folder" = 'deploy';
	
	"build_folder" = 'build';

    "projects" = @(    
	)

}

import-module .\packages\psake.4.4.2\tools\psake.psm1

invoke-psake $build_file $task_list -Properties $properties