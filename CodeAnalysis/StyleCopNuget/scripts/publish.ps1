$ErrorActionPreference = "Stop"

$outputPath = 'nupkgs'

if (-not (Test-Path $outputPath))
{
	Set-Location ..
}

Write-Host -ForegroundColor Green "$('-'*6) Start pushing nuget $('-'*6)"

dotnet nuget push ".\$outputPath\*.nupkg" --source "LocalNugetFeed" --skip-duplicate --interactive

if (!$?) {
    Write-Error "Failed to push nuget package" -ErrorAction $ErrorActionPreference
    return
}

Write-Host -ForegroundColor Green "$('-'*6) Finish pushing nuget $('-'*6)"