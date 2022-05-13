$delimiter = '='*10

Write-Host "$delimiter Deployments $delimiter`n"
kubectl get deployments

Write-Host "`n$delimiter Pods $delimiter`n"
kubectl get pods

Write-Host "`n$delimiter Services $delimiter`n"
kubectl get services

Write-Host "`n$delimiter Persistent Volume Claims $delimiter`n"
kubectl get pvc