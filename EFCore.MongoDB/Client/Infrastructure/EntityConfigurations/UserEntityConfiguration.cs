using Client.Domain;
using EFCore.MongoDB;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Client.Infrastructure.EntityConfigurations
{
	internal class UserEntityConfiguration : EntityConfigurationBase<User>
	{
		protected override void Configure(BsonClassMap<User> builder)
		{
			builder
				.MapIdField(x => x.Id);

			builder
				.MapProperty(x => x.Name);

			builder
				.MapProperty(x => x.Code)
				.SetSerializer(new GuidSerializer());
		}
	}
}
