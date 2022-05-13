function Get-DeploymentToDelete([string[]] $availableDeployment) 
{
	do 
	{
		$deploymentName = Read-Host "Enter deployment name "
	}
	until ($availableDeployment -contains $deploymentName -or [string]::IsNullOrWhiteSpace($deploymentName))

	if ([string]::IsNullOrWhiteSpace($deploymentName))
	{
		return $availableDeployment
	}

	return @($deploymentName)
}

while ($true)
{
	cls

	Write-Host "Available deployment:"
	Write-Host "`n"
	kubectl get deployment
	Write-Host "`n"

	$availableDeployment = kubectl get deployment --no-headers -o custom-columns=":metadata.name" | ForEach-Object { $_.Split("")[0] }

	$deploymentToDelete = Get-DeploymentToDelete $availableDeployment

	foreach ($deploy in $deploymentToDelete)
	{
		kubectl delete deployment $deploy
	}
}