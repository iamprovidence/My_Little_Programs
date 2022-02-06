namespace EFCore.MongoDB
{
	public interface IDesignTimeDbContextFactory<out T>
		where T : MongoDbContext
	{
		T CreateDbContext(string[] args);
	}
}
