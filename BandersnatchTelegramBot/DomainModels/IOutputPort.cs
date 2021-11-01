namespace BandersnatchTelegramBot.DomainModels;

interface IOutputPort
{
	Task ShowTextMessage(string text);
	Task ShowOptions(IEnumerable<string> options);
}
