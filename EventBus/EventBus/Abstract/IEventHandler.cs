namespace EventBus.Abstract
{
	public interface IEventHandler<in TEvent> where TEvent : IEvent
	{
		System.Threading.Tasks.Task Handle(TEvent @event);
	}
}
