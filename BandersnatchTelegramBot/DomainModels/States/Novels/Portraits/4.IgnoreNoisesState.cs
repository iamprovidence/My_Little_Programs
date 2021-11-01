namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class IgnoreNoisesState : IBotState
{
	private readonly string Option1 = "Відкрити двері";

	public string GetDescription()
	{
		return "Гуркотіння тільки голоснішає. У вас немає вибору.";
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
		if (update == Option1) return new OpenTheDoorState();

		return this;
	}
}
