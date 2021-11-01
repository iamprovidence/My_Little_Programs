# Overview

A small project of telegram bot using State pattern

## Configure

1. Create Telegram bot using @BotFather
2. Copy access token
2. Run ```Scripts\set-secrets.ps1``` and paste copied token

## Debug 

Use ngrok managment page to track and reply requests - http://localhost:4040/

## Build

1. Build project in VisualStudio or by using ```dotnet build``` command
2. Run ```Scripts\install-ngrok.ps1``` or install ngrok manually and add it to ```bin``` folder

## Start

1. Start project in VisualStudio or by using ```dotnet run``` command
2. Run ```Scripts\start-bot.ps1``` to set telegram webhook