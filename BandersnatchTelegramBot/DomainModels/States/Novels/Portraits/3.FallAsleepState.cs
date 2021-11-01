namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class FallAsleepState : IBotState
{
	private readonly string Option1 = "Заплющити очі";
	private readonly string Option2 = "Продовжити роздивлятись кімнату";

	public string GetDescription()
	{
		return "Ви довго не можете заснути, бо на стінах висять картини людей, що посміхаються вам.";
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
		if (update == Option1) return new AwakeState();
		if (update == Option2) return new WaitHouseKeeperState();

		return this;
	}
}
