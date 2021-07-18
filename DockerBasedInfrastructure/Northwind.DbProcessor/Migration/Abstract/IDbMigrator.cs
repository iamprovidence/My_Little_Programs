using System.Threading.Tasks;

namespace Northwind.DbProcessor.Migration.Abstract
{
	internal interface IDbMigrator
	{
		Task Migrate();
	}
}
