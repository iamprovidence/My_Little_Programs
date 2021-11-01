namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class OpenTheDoorState : IBotState
{
	private readonly string Option1 = "Покинути будинок";
	private readonly string Option2 = "Лягти спати";

	public string GetDescription()
	{
		return "Відкривши двері ви виявили, що за ними нікого немає. Та у вас відчуття, що за вами стежать картини на стінах.";
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
		if (update == Option1) return new GetLostState();
		if (update == Option2) return new FallAsleepState();

		return this;
	}
}
