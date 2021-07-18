rem Stop and Remove containers

call ./clearcache.cmd

rem Remove images

docker rmi northwind_dbprocessor:latest
docker rmi northwind_sqlserver:latest

docker system prune -f
