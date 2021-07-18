docker run ^
	northwind_dbprocessor ^
	-env RunModes=ListChanges ^
	-env ConnectionStrings:NorthwindDbConnection="Server=127.0.0.1,5533;Database=Northwind.Db;User Id=sa;Password=Pass@word"
