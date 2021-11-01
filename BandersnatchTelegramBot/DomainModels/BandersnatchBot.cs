namespace BandersnatchTelegramBot.DomainModels;

class BandersnatchBot
{
	private readonly IBotState _state;

	public BandersnatchBot()
	{
		_state = new InitialState();
	}

	public async Task Handle(string update, IOutputPort outputPort)
	{
		var newBotState = _state.Handle(update, outputPort);

		var description = newBotState.GetDescription();
		await outputPort.ShowTextMessage(description);

		var newOptions = newBotState.GetOptions();
		await outputPort.ShowOptions(newOptions);
	}
}
