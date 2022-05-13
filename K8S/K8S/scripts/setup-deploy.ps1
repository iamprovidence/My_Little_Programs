# create secrets
$secretName = "ms-sql-secrets"
kubectl delete secret $secretName
kubectl create secret generic $secretName --from-literal=SA_PASSWORD="Pass@word"