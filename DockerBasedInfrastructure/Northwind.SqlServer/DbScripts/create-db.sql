--CREATE DATABASE [Northwind.NewDb];

RESTORE DATABASE [Northwind.Db] 
	FROM DISK = '/var/opt/mssql/NorthwindDb.bak' WITH FILE = 1,  
	MOVE 'Northwind' TO '/var/opt/mssql/data/Northwind.Db.MDF',  
	MOVE 'Northwind_log' TO '/var/opt/mssql/data/Northwind.Db_log.ldf';
