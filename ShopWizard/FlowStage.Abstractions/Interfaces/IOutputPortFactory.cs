namespace FlowStage.Abstractions.Interfaces
{
	// Not a required part 
	// Just added to bypass ASP limitation
	public interface IOutputPortFactory
	{
		void Register<TOutputPort>(TOutputPort outputPort)
			where TOutputPort : IOutputPort;
		TOutputPort MakeInstance<TOutputPort>()
			where TOutputPort : IOutputPort;
	}
}
