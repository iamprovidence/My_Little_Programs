FROM mcr.microsoft.com/mssql/server:2017-latest

COPY DockerBasedInfrastructure/Northwind.SqlServer/DbBackups/NorthwindDb.bak .
COPY DockerBasedInfrastructure/Northwind.SqlServer/DbScripts/create-db.sql .
COPY DockerBasedInfrastructure/Northwind.SqlServer/DbScripts/create-missing-table.sql .
COPY DockerBasedInfrastructure/Northwind.SqlServer/DbScripts/add-missing-data.sql .
COPY DockerBasedInfrastructure/Northwind.SqlServer/EntryPoint.sh .
COPY DockerBasedInfrastructure/Northwind.SqlServer/SqlScriptLauncher.sh .

RUN chmod +x ./SqlScriptLauncher.sh

ENTRYPOINT /bin/bash ./EntryPoint.sh