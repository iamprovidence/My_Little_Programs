using HotChocolate;
using HotChocolate.Types;
using Server.Domain;

namespace Server.Endpoints.GraphQL
{
	public class Subscription
	{
		[Subscribe]
		[Topic]
		public PlatformAddedEvent OnPlatformAdded([EventMessage] PlatformAddedEvent platform)
		{
			return platform;
		}
	}
}
