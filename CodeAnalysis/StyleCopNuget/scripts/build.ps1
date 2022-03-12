$ErrorActionPreference = 'Stop'

$outputPath = 'nupkgs'

if (-not (Test-Path $outputPath))
{
	Set-Location ..
}

Write-Host -ForegroundColor Green "$('-'*6) Start building nuget $('-'*6)"

if ($args.count -lt 1) 
{
	$version = '99.0.0-dev' + (Get-Date).toString('yyyyMMddHHmmss') # available when 'Include prerelease' enabled
} 
else 
{
	$version = $args[0]
}

if (Test-Path $outputPath) 
{
	Write-Host 'Clearing output path'
	Get-ChildItem -Path $outputPath -Exclude *.gitkeep | Foreach-Object {Remove-Item $_.FullName}
}

Write-Host 'Setting package version'
(Get-Content Custom.StyleCop.props) -replace '<StylePackageVersion>.*<\/StylePackageVersion>', "<StylePackageVersion>$version</StylePackageVersion>" | Set-Content Custom.StyleCop.props

Write-Host "Building package version $version"
dotnet pack StyleCopNuget.csproj -p:NuspecFile=Custom.StyleCop.nuspec -p:NuspecProperties=version=$version --output $outputPath

if (!$?) 
{
	Write-Error "Failed to build nuget package" -ErrorAction $ErrorActionPreference
	return
}

Write-Host -ForegroundColor Green "$('-'*6) Finish building nuget $('-'*6)"