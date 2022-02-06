using MongoDB.Bson.Serialization;

namespace EFCore.MongoDB
{
	public abstract class EntityConfigurationBase<TEntity>
	{
		protected EntityConfigurationBase()
		{
			BsonClassMap.RegisterClassMap<TEntity>(Configure);
		}

		protected abstract void Configure(BsonClassMap<TEntity> builder);
	}
}
