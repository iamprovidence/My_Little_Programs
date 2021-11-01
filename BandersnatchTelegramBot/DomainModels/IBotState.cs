namespace BandersnatchTelegramBot.DomainModels;

interface IBotState
{
	string GetDescription();
	IReadOnlyCollection<string> GetOptions();
	IBotState Handle(string update, IOutputPort outputPort);
}
