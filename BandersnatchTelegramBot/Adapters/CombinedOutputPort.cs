namespace BandersnatchTelegramBot.Adapters;

class CombinedOutputPort : IOutputPort
{
	private readonly IOutputPort[] _outputPorts;

	public CombinedOutputPort(params IOutputPort[] outputPorts)
	{
		_outputPorts = outputPorts;
	}

	public async Task ShowTextMessage(string text)
	{
		foreach (var outputPort in _outputPorts)
		{
			await outputPort.ShowTextMessage(text);
		}
	}

	public async Task ShowOptions(IEnumerable<string> options)
	{
		foreach (var outputPort in _outputPorts)
		{
			await outputPort.ShowOptions(options);
		}
	}
}
