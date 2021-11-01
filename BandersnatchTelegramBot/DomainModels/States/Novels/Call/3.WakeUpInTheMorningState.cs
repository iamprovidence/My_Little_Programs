namespace BandersnatchTelegramBot.DomainModels.States.Novels.Call;

class WakeUpInTheMorningState : IBotState
{
	private readonly string Option1 = "Почати заново";

	public string GetDescription()
	{
		return "На ранок ви дізнались, що ваша мати померла.";
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
