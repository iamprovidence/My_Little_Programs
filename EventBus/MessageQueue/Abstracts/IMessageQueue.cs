using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageQueue.Abstracts
{
	internal interface IMessageQueue
	{
		Task Send<T>(string connectionStringKey, string queueName, T message);

		// await foreach (var message in _service.Read<T>(connectionStringKey, queueName).WithCancellation(cancellationToken))
		IAsyncEnumerable<T> Read<T>(string connectionStringKey, string queueName, TimeSpan readInterval);
	}
}
