namespace BandersnatchTelegramBot.DomainModels.States;

class InitialState : IBotState
{
	private readonly string Option1 = "Дзвінок";
	private readonly string Option2 = "Портрети";

	public string GetDescription()
	{
		return "Бот дозволяє взяти участь в одній з інтерактивних новел";
	}

	public IReadOnlyCollection<string> GetOptions()
	{
		return new[]
		{
			Option1,
			Option2,
		};
	}

	public IBotState Handle(string update, IOutputPort outputPort)
	{
		if (update == Option1) return new InsomniaState();
		if (update == Option2) return new RunningInTheWoodState();

		return this;
	}
}
