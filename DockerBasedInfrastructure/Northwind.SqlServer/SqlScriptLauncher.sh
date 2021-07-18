#!/bin/bash
set -e

echo "Moving backup file..."
while [ ! -d "/var/opt/mssql" ] 
do
  echo "Directory /var/opt/mssql does not yet exist"
  sleep 1
done
mv /NorthwindDb.bak /var/opt/mssql/

echo "Checking for SQL Server connection..."
while [ ! `/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q "SELECT @@VERSION"` ]
do
  echo "SQL Server has not yet started"
  sleep 2
done

echo "Running create-db.sql..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i create-db.sql 

echo "Running create-missing-table.sql..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i create-missing-table.sql

echo "Running add-missing-data.sql..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i add-missing-data.sql