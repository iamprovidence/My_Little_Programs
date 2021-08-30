using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DaCache.Implementation.Telemetry
{
	internal abstract class TrackingDecoratorBase
	{
		private readonly TelemetryClient _telemetryClient;
		private readonly TrackingOptions _trackingOptions;

		public TrackingDecoratorBase(TelemetryClient telemetryClient, TrackingOptions trackingOptions)
		{
			_telemetryClient = telemetryClient;
			_trackingOptions = trackingOptions;
		}

		protected void TrackEvent(string method, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			_telemetryClient.TrackEvent(GetEventName(method), properties, metrics);
		}

		protected async Task<T> ExecuteWithTracking<T>(string method, Func<Task<T>> func, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
			var isSuccessful = true;
			var start = DateTimeOffset.UtcNow;

			var watch = Stopwatch.StartNew();

			try
			{
				TrackEvent(GetEventName(method), properties, metrics);

				return await func();
			}
			catch (Exception ex)
			{
				isSuccessful = false;
				_telemetryClient.TrackException(ex);

				throw;
			}
			finally
			{
				watch.Stop();
				var duration = watch.Elapsed;

				_telemetryClient.TrackDependency(
					dependencyTypeName: _trackingOptions.TrackingDependencyName,
					target: _trackingOptions.TrackTarget,
					dependencyName: method,
					data: string.Empty,
					startTime: start,
					duration: duration,
					resultCode: string.Empty,
					success: isSuccessful);
			}
		}

		private string GetEventName(string method)
		{
			return $"{_trackingOptions.TrackingDependencyName}.{method}";
		}
	}
}
