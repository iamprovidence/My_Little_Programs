namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class InTheHouseState : IBotState
{
	private readonly string Option1 = "Лягти спати";
	private readonly string Option2 = "Почекати господаря будинку";
	private readonly string Option3 = "Вийти з будинку та продовжити блукати лісом";

	public string GetDescription()
	{
		return "Ви зайшли в середину та виявили що нікого немає. Трохи відпочивши, вас склонило на сон.";
	}

	public IReadOnlyCollection<string> GetOptions()
	{
		return new[]
		{
			Option1,
			Option2,
			Option3,
		};
	}

	public IBotState Handle(string update, IOutputPort outputPort)
	{
		if (update == Option1) return new FallAsleepState();
		if (update == Option2) return new WaitHouseKeeperState();
		if (update == Option3) return new GetLostState();

		return this;
	}
}
