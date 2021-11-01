namespace BandersnatchTelegramBot.DomainModels.States.Novels.Portraits;

class AwakeState : IBotState
{
	private readonly string Option1 = "Почати заново";

	public string GetDescription()
	{
		return "Вранці вас розбудило яскраве сонячне світло. На стінах не було жодних картин, лише вікна";
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
		if (update == Option1) return new InitiaState();

		return this;
	}
}
