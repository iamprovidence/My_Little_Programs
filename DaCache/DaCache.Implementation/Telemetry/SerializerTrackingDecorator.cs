using DaCache.Implementation.Interfaces;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaCache.Implementation.Telemetry
{
	internal class SerializerTrackingDecorator : TrackingDecoratorBase, ISerializer
	{
		private readonly ISerializer _serializer;

		public SerializerTrackingDecorator(
			ISerializer serializer,
			TelemetryClient telemetryClient,
			TrackingOptions trackingOptions)
			: base(telemetryClient, trackingOptions)
		{
			_serializer = serializer;
		}

		public Task<byte[]> Serialize<T>(T item)
		{
			return ExecuteWithTracking(nameof(Serialize), async () =>
			{
				var serializedObject = await _serializer.Serialize(item);
				var serializedObjectSize = serializedObject.Length;

				TrackEvent(nameof(Serialize), CreateProperties<T>(), CreateMetrics(serializedObjectSize));

				return serializedObject;
			});
		}

		public Task<T> Deserialize<T>(byte[] serializedObject)
		{
			return ExecuteWithTracking(nameof(Deserialize), () =>
			{
				return _serializer.Deserialize<T>(serializedObject);
			}, CreateProperties<T>(), CreateMetrics(serializedObject.Length));
		}

		private IDictionary<string, string> CreateProperties<T>()
		{
			return new Dictionary<string, string>()
			{
				["SerializedObjectType"] = typeof(T).Name,
			};
		}

		private IDictionary<string, double> CreateMetrics(int serializedObjectSize)
		{
			return new Dictionary<string, double>()
			{
				["SerializedObjectSize"] = serializedObjectSize,
			};
		}
	}
}
