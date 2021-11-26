using MessageQueue.Abstracts;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MessageQueue
{
	internal class AzureMessageQueue : IMessageQueue
	{
		private class AzureQueueAsyncEnumerator<T> : IAsyncEnumerable<T>
		{
			private readonly CloudQueue _cloudQueue;
			private readonly TimeSpan _readInterval;

			public AzureQueueAsyncEnumerator(CloudQueue cloudQueue, TimeSpan readInterval)
			{
				_cloudQueue = cloudQueue;
				_readInterval = readInterval;
			}

			public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
			{
				while (!cancellationToken.IsCancellationRequested)
				{
					var message = await _cloudQueue.GetMessageAsync(cancellationToken);

					if (message != null)
					{
						yield return JsonConvert.DeserializeObject<T>(message.AsString);

						await _cloudQueue.DeleteMessageAsync(message, cancellationToken);
					}

					await Task.Delay(_readInterval, cancellationToken);
				}
			}
		}

		private readonly IConfiguration _configuration;
		private readonly ConcurrentDictionary<string, CloudQueue> _queues;

		public AzureMessageQueue(IConfiguration configuration)
		{
			_configuration = configuration;
			_queues = new ConcurrentDictionary<string, CloudQueue>();
		}

		public async Task Send<T>(string connectionStringKey, string queueName, T message)
		{
			var queue = GetOrCreateQueue(connectionStringKey, queueName);

			var messageJson = JsonConvert.SerializeObject(message);
			var queueMessage = new CloudQueueMessage(messageJson);
			await queue.AddMessageAsync(queueMessage);
		}

		public IAsyncEnumerable<T> Read<T>(string connectionStringKey, string queueName, TimeSpan readInterval)
		{
			var queue = GetOrCreateQueue(connectionStringKey, queueName);

			return new AzureQueueAsyncEnumerator<T>(queue, readInterval);
		}

		private CloudQueue GetOrCreateQueue(string connectionStringKey, string queueName)
		{
			return _queues.GetOrAdd($"{connectionStringKey}~{queueName}", (key) =>
			{
				var connectionStringValue = _configuration.GetConnectionString(connectionStringKey);

				if (string.IsNullOrWhiteSpace(connectionStringValue))
				{
					throw new InvalidOperationException("Connection string is not configured");
				}

				var storageAccount = CloudStorageAccount.Parse(connectionStringValue);
				var queueClient = storageAccount.CreateCloudQueueClient();

				var queue = queueClient.GetQueueReference(queueName);
				queue.CreateIfNotExists();

				return queue;
			});
		}
	}
}
