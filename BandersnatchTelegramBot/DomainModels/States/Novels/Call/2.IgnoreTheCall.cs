namespace BandersnatchTelegramBot.DomainModels.States.Novels.Call;

class IgnoreTheCallState : IBotState
{
	private readonly int IgnoreLimit = 3;

	private readonly string Option1 = "Підняти трубку";
	private readonly string Option2 = "Проігнорувати";
	private readonly string Option3 = "Заснути";

	private int _callIgoreCount = 0;

	public string GetDescription()
	{
		++_callIgoreCount;

		return "Дзвінок пролунав знову.";
	}

	public IReadOnlyCollection<string> GetOptions()
	{
		var options = new List<string>()
		{
			Option1,
			Option2,
		};

		if (_callIgoreCount >= IgnoreLimit)
		{
			options.Add(Option3);
		}

		return options;
	}

	public IBotState Handle(string update, IOutputPort outputPort)
	{
		if (update == Option1) return new PickUpThePhoneState();
		if (update == Option2) return this;
		if (update == Option3) return new WakeUpInTheMorningState();

		return this;
	}
}
