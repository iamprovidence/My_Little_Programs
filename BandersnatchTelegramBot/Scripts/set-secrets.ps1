$token = Read-Host -Prompt 'Input your bot token'

dotnet user-secrets set "TelegramBot:Token" $token --project ../BandersnatchTelegramBot.csproj

$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');