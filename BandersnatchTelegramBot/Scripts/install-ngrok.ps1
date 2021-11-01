# Check if file exist
if (Test-Path -Path ../bin/ngrok.exe -PathType Leaf) 
{
	return
}

# Create bin
if (Test-Path -Path ../bin/)
{
	New-Item -ItemType Directory -Force -Path ../bin/
}

# Download and extract
Invoke-WebRequest -Uri "https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-windows-amd64.zip" -OutFile ../bin/ngrok-stable-windows-amd64.zip -UseBasicParsing
Expand-Archive ../bin/ngrok-stable-windows-amd64.zip ../bin/ngrok -Force

# Move to bin
Get-ChildItem ../bin/ngrok/ngrok.exe | Move-Item -Destination ../bin/

# Clean the kitchen
del -Force ../bin/ngrok-stable-windows-amd64.zip
del -Force -Recurse ../bin/ngrok/

$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');