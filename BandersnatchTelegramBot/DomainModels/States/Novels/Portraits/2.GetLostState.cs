namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class GetLostState : IBotState
{
	private readonly string Option1 = "Почати заново";

	public string GetDescription()
	{
		return "Ви загубились.";
	}

	public IReadOnlyCollection<string> GetOptions()
	{
		return new[]
		{
			Option1,
		};
	}

	public IBotState Handle(string update, IOutputPort outputPort)
	{
		if (update == Option1) return new InitialState();

		return this;
	}
}
