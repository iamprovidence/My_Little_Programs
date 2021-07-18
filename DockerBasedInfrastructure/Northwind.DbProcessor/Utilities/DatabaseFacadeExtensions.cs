using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Northwind.DbProcessor.Utilities
{
	internal static class DatabaseFacadeExtensions
	{
		public static bool Exists(this DatabaseFacade source)
		{
			try
			{
				return source.GetService<IRelationalDatabaseCreator>().Exists();
			}
			catch (SqlException)
			{
				return false;
			}
		}
	}
}
