$ErrorActionPreference = "Stop"

powershell -ExecutionPolicy Bypass -File setup-deploy.ps1

cd ..
cd ..

docker build . -f TicketApi/Dockerfile -t local/ticket-api:kube
docker build . -f DisplayTextWorker/Dockerfile -t local/display-text-worker:kube

$deploymentFiles = Get-ChildItem -Recurse -Path K8S/deployment -Filter *.yaml

foreach ($deploymentFile in $deploymentFiles)
{
	kubectl apply -f $deploymentFile.FullName
}

do
{
	Start-Sleep -MilliSecond 500

	Write-Host "Deployment status:"

	$deployment = kubectl get deployment --no-headers -o custom-columns=":metadata.name,:status.conditions[?(@.type=='Available')].status"
	
	Write-Host ($deployment | Out-String)

	$deploymentRunningStatuses = $deployment | 
        ForEach-Object { $PSItem.Split(' ',[System.StringSplitOptions]::RemoveEmptyEntries)[1] } | 
        ForEach-Object { [System.Convert]::ToBoolean($PSItem) }
	
} while ($deploymentRunningStatuses -contains $false)

Start-Process http://localhost:30080/api/server

