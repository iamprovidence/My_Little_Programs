const int SessionTimeoutInMinutes = 15;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<IAssemblyMarker>();
builder.Services.AddMemoryCache();
var app = builder.Build();

app.MapGet("/", async (http) =>
{
	var token = GetBotToken(http);

	var result = string.IsNullOrWhiteSpace(token) ?
		"Bot is not configured. Specify Token." :
		"Bot started";

	await http.Response.WriteAsync(result);
});

app.MapPost("/", async (http) =>
{
	var botUpdate = await GetBotUpdate(http);
	var currentBotState = GetCurrentBotState(http, botUpdate);
	await HandleUpdate(http, currentBotState, botUpdate);
	UpdateSession(http, currentBotState);
	
	async Task<Update> GetBotUpdate(HttpContext http)
	{
		using var reader = new StreamReader(http.Request.Body);
		var body = await reader.ReadToEndAsync();

		return JsonConvert.DeserializeObject<Update>(body);
	}

	BandersnatchBot GetCurrentBotState(HttpContext http, Update botUpdate)
	{
		var sessionService = http.RequestServices.GetRequiredService<IMemoryCache>();
		var sessionId = botUpdate.Message?.Chat?.Id ?? botUpdate.MyChatMember.Chat.Id;

		return sessionService.GetOrCreate(sessionId, cacheEntry =>
		{
			cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(SessionTimeoutInMinutes);

			return new BandersnatchBot();
		});
	}

	async Task HandleUpdate(HttpContext http, BandersnatchBot currentBotState, Update botUpdate)
	{
		var telegramOutputPort = new TelegramOutputPort(new TelegramBotClient(GetBotToken(http)), botUpdate);
		var consoleOutputPort = new ConsoleOutputPort();
		var combinedOutputPort = new CombinedOutputPort(telegramOutputPort, consoleOutputPort);

		var userInput = botUpdate.Message?.Text ?? string.Empty;
		Console.WriteLine("Користувач ввів: ", userInput);

		await currentBotState.Handle(userInput, combinedOutputPort);
	}

	void UpdateSession(HttpContext http, BandersnatchBot currentBotState)
	{
		var sessionService = http.RequestServices.GetRequiredService<IMemoryCache>();
		var sessionId = botUpdate.Message?.Chat?.Id ?? botUpdate.MyChatMember.Chat.Id;

		sessionService.Set(sessionId, currentBotState);
	}
});

app.Run();

static string GetBotToken(HttpContext http)
{
	return http.RequestServices.GetRequiredService<IConfiguration>().GetValue<string>("TelegramBot:Token");
}