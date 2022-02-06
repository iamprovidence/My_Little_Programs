class UndefinedCommand : ICommand
{
	private readonly string _command;

	public UndefinedCommand(string command)
	{
		_command = command;
	}

	public void Execute()
	{
		var text = $"Command '{_command}' not defined";

		Console.WriteLine(text);
	}
}
