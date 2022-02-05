# load external docker images into the local repository
docker pull mcr.microsoft.com/mssql/server:2017-latest
docker pull mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
docker pull mcr.microsoft.com/dotnet/core/sdk:3.1.300

# build base images
# docker build -f ../Northwind.SqlServer/Dockerfile.base -t ms-sql-server-base .

# build our own docker images and services
docker-compose -f docker-compose-infrastructure.yml build
