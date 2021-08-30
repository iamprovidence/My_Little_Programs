using MsgPack;
using MsgPack.Serialization;
using Newtonsoft.Json;
using System;

namespace DaCache.Implementation.Services
{
	/// <summary>
	/// A custom serializer for <see cref="DateTimeOffset"/>.
	/// </summary>
	/// <remarks>
	/// The default serializer is not working well. It does not keep time zone information.
	/// </remarks>
	internal class DateTimeOffsetSerializer : MessagePackSerializer<DateTimeOffset>
	{
		public DateTimeOffsetSerializer(SerializationContext ownerContext) : base(ownerContext) { }

		protected override void PackToCore(Packer packer, DateTimeOffset objectTree)
		{
			var textValue = JsonConvert.SerializeObject(objectTree);
			packer.Pack(textValue);
		}

		protected override DateTimeOffset UnpackFromCore(Unpacker unpacker)
		{
			var textValue = unpacker.LastReadData.AsString();

			return JsonConvert.DeserializeObject<DateTimeOffset>(textValue);
		}
	}
}
