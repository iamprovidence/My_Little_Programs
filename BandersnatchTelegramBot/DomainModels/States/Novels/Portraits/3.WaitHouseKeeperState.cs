namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class WaitHouseKeeperState : IBotState
{
	private readonly string Option1 = "Відкрити двері";
	private readonly string Option2 = "Чекати";

	public string GetDescription()
	{
		return "У двері роздався сильний гуркіт.";
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
		if (update == Option1) return new OpenTheDoorState();
		if (update == Option2) return new IgnoreNoisesState();

		return this;
	}
}
