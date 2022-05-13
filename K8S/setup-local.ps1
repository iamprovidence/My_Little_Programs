# https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CommandTaskRunner64

$devNetworkName = "k8s-dev-network"
$rabbitMqContainerName = "rabbit-mq-container"

Write-Host "Configure network"
docker network rm $devNetworkName
docker network create --subnet=172.18.0.0/16 $devNetworkName

Write-Host "Start RabbitMq"

Register-EngineEvent PowerShell.Exiting –Action { 
	docker container rm $rabbitMqContainerName -f
	docker network rm $devNetworkName
}

docker run `
	--rm `
	-p 5672:5672 `
	-p 15672:15672 `
	--hostname=localhost `
	--name $rabbitMqContainerName `
	--net $devNetworkName `
	--ip 172.18.0.10 `
	rabbitmq:3-management
