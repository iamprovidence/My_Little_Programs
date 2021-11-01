namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class RunningInTheWoodState : IBotState
{
	private readonly string Option1 = "Зайти в середину";
	private readonly string Option2 = "Пройти повз";

	public string GetDescription()
	{
		return "Ви заблудились в лісі. Після довгих блукань у сутінках ви знаходите хатину.";
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
		if (update == Option1) return new InTheHouseState();
		if (update == Option2) return new GetLostState();

		return this;
	}
}
