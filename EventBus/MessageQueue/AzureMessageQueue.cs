using MessageQueue.Abstracts;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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

		private readonly object _lockObject = new object();
		private readonly IDictionary<string, CloudQueueClient> _queueClients = new Dictionary<string, CloudQueueClient>();

		private readonly IConfiguration _configuration;

		public AzureMessageQueue(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task Send<T>(string connectionStringKey, string queueName, T message)
		{
			var queueClient = GetOrCreateQueueClient(connectionStringKey);

			var queue = queueClient.GetQueueReference(queueName);
			await queue.CreateIfNotExistsAsync();

			var messageJson = JsonConvert.SerializeObject(message);
			var queueMessage = new CloudQueueMessage(messageJson);
			await queue.AddMessageAsync(queueMessage);
		}

		public async Task<IAsyncEnumerable<T>> Read<T>(string connectionStringKey, string queueName, TimeSpan readInterval)
		{
			var queueClient = GetOrCreateQueueClient(connectionStringKey);

			var queue = queueClient.GetQueueReference(queueName);
			await queue.CreateIfNotExistsAsync();

			return new AzureQueueAsyncEnumerator<T>(queue, readInterval);
		}

		private CloudQueueClient GetOrCreateQueueClient(string connectionStringKey)
		{
			if (!_queueClients.ContainsKey(connectionStringKey))
			{
				var connectionStringValue = _configuration.GetConnectionString(connectionStringKey);

				if (string.IsNullOrWhiteSpace(connectionStringValue))
				{
					throw new InvalidOperationException("Connection string is not configured");
				}

				lock (_lockObject)
				{
					var storageAccount = CloudStorageAccount.Parse(connectionStringValue);
					var queueClient = storageAccount.CreateCloudQueueClient();
					_queueClients.Add(connectionStringKey, queueClient);
				}
			}

			return _queueClients[connectionStringKey];
		}
	}
}
