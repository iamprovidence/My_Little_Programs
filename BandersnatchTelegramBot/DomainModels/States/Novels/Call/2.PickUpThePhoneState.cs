namespace BandersnatchTelegramBot.DomainModels.States.Novels.Call;

class PickUpThePhoneState : IBotState
{
	private readonly int RecallLimit = 3;

	private readonly string Option1 = "Спробувати перетелефонувати";
	private readonly string Option2 = "Заснути";

	private int _recallCount = 0;

	public string GetDescription()
	{
		return "Тишина.";
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
		if (update == Option1 && _recallCount >= RecallLimit)
		{
			outputPort.ShowTextMessage("Так тривало близько години. Ви провалилася в забуття.");

			return new WakeUpInTheMorningState();
		}
		if (update == Option1)
		{
			++_recallCount;

			return this;
		}
		if (update == Option2) return new WakeUpInTheMorningState();

		return this;
	}
}
