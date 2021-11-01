namespace BandersnatchTelegramBot.Adapters;

class ConsoleOutputPort : IOutputPort
{
	public Task ShowTextMessage(string text)
	{
		Console.WriteLine(text);
	
		return Task.CompletedTask;
	}

	public Task ShowOptions(IEnumerable<string> options)
	{
		Console.WriteLine("Options:");

		foreach (var option in options)
		{
			Console.WriteLine($"- {option}");
		}

		return Task.CompletedTask;
	}
}
