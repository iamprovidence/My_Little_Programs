namespace BandersnatchTelegramBot.Adapters;

class TelegramOutputPort : IOutputPort
{
	private readonly ITelegramBotClient _telegramBotClient;
	private readonly ChatId _chatId;

	public TelegramOutputPort(ITelegramBotClient telegramBotClient, Update update)
	{
		_telegramBotClient = telegramBotClient;
		_chatId = update.Message?.Chat?.Id ?? update.MyChatMember.Chat.Id;
	}

	public async Task ShowTextMessage(string text)
	{
		await _telegramBotClient.SendTextMessageAsync(_chatId, text);
	}

	public async Task ShowOptions(IEnumerable<string> options)
	{
		var replyKeyboardMarkup = new ReplyKeyboardMarkup
		{
			Keyboard = options
				.Select(option => new KeyboardButton[]
				{
					new KeyboardButton(option),
				}),
		};

		await _telegramBotClient.SendTextMessageAsync(_chatId, text: "Зробіть вибір:", replyMarkup: replyKeyboardMarkup);
	}
}
