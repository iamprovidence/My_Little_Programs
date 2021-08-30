using DaCache.Implementation.Interfaces;
using MsgPack.Serialization;
using System.Threading.Tasks;

namespace DaCache.Implementation.Services
{
	internal class BinarySerializer : ISerializer
	{
		private readonly SerializationContext _context;

		public BinarySerializer()
		{
			var context = new SerializationContext();

			// Register custom serializers
			context.Serializers.RegisterOverride(new DateTimeOffsetSerializer(context));

			_context = context;
		}

		public Task<byte[]> Serialize<T>(T item)
		{
			return MessagePackSerializer.Get<T>(_context).PackSingleObjectAsync(item);
		}
		public Task<T> Deserialize<T>(byte[] serializedObject)
		{
			return MessagePackSerializer.Get<T>(_context).UnpackSingleObjectAsync(serializedObject);
		}
	}
}
