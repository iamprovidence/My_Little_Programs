# Get BotApi url
Write-Host "Get BotApi url"
$launchSettings = Get-Content ../Properties/launchSettings.json | ConvertFrom-Json
$apiPort = $launchSettings.profiles.BandersnatchTelegramBot.applicationUrl.split(':') | Select-Object -Last 1

# Initiate ngrok
Write-Host "Initiate ngrok at port $($apiPort)"
$param = "http $($apiPort)"
$ngrokProcess = Start-Process ../bin/ngrok.exe $param -PassThru
Start-Sleep 10

# Get ngrok tunnels
Write-Host "Load ngrok tunnels"
$ngrokOutput = ConvertFrom-Json (Invoke-WebRequest -Uri http://localhost:4040/api/tunnels -ContentType application/json).Content
$httpsUrl = $ngrokOutput.tunnels.public_url | where{$_ -like "https*"} | Select-Object -First 1

# Get bot token
$botToken = (dotnet user-secrets list --project "../BandersnatchTelegramBot.csproj").split(' ') | Select-Object -Last 1

# Set bot webhook
Write-Host "Set bot webhook : $($httpsUrl)"
$postParams = @{ url=$httpsUrl }
Invoke-WebRequest -Uri "https://api.telegram.org/bot$($botToken)/setWebhook" -Method POST -Body $postParams

# Dispose
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
Stop-Process $ngrokProcess
