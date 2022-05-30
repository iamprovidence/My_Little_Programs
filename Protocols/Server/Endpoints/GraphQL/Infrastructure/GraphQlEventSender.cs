using HotChocolate.Subscriptions;
using Server.Application.Common;
using Server.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Endpoints.GraphQL.Infrastructure
{
	public class GraphQlEventSender : IEventSender
	{
		private readonly ITopicEventSender _topicEventSender;

		public GraphQlEventSender(ITopicEventSender topicEventSender)
		{
			_topicEventSender = topicEventSender;
		}

		public async Task Publish(EventArgs eventArgs, CancellationToken cancellationToken)
		{
			if (eventArgs is PlatformAddedEvent)
			{
				await _topicEventSender.SendAsync(nameof(Subscription.OnPlatformAdded), eventArgs as PlatformAddedEvent, cancellationToken);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}
	}
}
