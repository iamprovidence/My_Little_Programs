namespace BandersnatchTelegramBot.DomainModels.States.Novels.Call;

class InsomniaState : IBotState
{
	private readonly string Option1 = "Підняти трубку";
	private readonly string Option2 = "Проігнорувати";

	public string GetDescription()
	{
		return "Пізня ніч. Ви не можете заснути. Раптом із вашого телефону пролунав дзвінок. Телефонує ваша мати.";
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
		if (update == Option1) return new PickUpThePhoneState();
		if (update == Option2) return new IgnoreTheCallState();

		return this;
	}
}
