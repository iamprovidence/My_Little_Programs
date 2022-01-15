# Run in Administrator mode
Set-ExecutionPolicy Bypass -Scope Process -Force; 
iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

$Packages = @(
    '7zip',
    'winrar',
    
    'ccleaner',
    'chocolateygui',
    'lightshot',

    'googlechrome',
    'adblockpluschrome',

    'notepadplusplus',
    'vscode',
    'visualstudio-installer',
    'visualstudio2022community',

    'skype',
    'microsoft-teams',
    'telegram',

    'git',
    'git-lfs',

    'sql-server-management-studio',
    'nodejs',
    'jcpicker',
    'curl',
    'postman',
    'docker-desktop',
    'lockhunter')

ForEach ($PackageName in $Packages)
{
    Write-Host 'Installing {$PackageName}';
    choco install $PackageName -y
}

Write-Host -NoNewLine 'Press any key to close...';
$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
