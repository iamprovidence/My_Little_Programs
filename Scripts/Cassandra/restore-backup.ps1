# Run in Administrator mode
if (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))  
{  
    $arguments = "& '" +$myinvocation.mycommand.definition + "'"
    Start-Process powershell -Verb runAs -ArgumentList $arguments
    Break
}

# Get Cassandra path
Clear
Write-Host "Getting Cassandra path"
$cassandraDirectory = $env:CASSANDRA_HOME
if ($cassandraDirectory -eq $null)
{
    Write-Host "CASSANDRA_HOME environment variable missing. Searching for directory"
    $cassandraDirectory = Get-ChildItem -Path "C:\" -Recurse "apache-cassandra*" -Directory -ErrorAction SilentlyContinue | Select-Object -ExpandProperty FullName
}
Write-Host "Cassandra path : $($cassandraDirectory)"
Write-Host

# Get backup name
Write-Host "Available backups :"
$backupList = cmd.exe /c "$($cassandraDirectory)\bin\nodetool.bat" listsnapshots `
| Select -Skip 2 `
| Select -SkipLast 3 `
| ForEach-Object { $_.Split("")[0] } `
| Get-Unique `
| Sort-Object
$backupList
Write-Host

do
{
    $backupName = Read-Host -Prompt 'Input backup name'
} while(-not ($backupList -contains $backupName))

# Restore backup
Write-Host "Start restoring data"

Stop-Service -Name "cassandra"

$backupDirectories = Get-ChildItem -Path "$($cassandraDirectory)\data\data" -Recurse $backupName -Directory -ErrorAction SilentlyContinue

$i = 1
ForEach ($backupDirectory in $backupDirectories)
{
    Write-Host "Restoring $($i) of $($backupDirectories.Count)"

    Remove-Item "$($backupDirectory.Parent.Parent.FullName)\*.*"

    Copy-Item `
        -Path "$($backupDirectory.FullName)\*" `
        -Destination $backupDirectory.Parent.Parent.FullName       

    $i = $i + 1
}
Start-Service -Name "cassandra"

# Delete backup
$confirmation = Read-Host "Do you want to delete backup y/N?"
if ($confirmation -eq 'y') 
{
    cmd.exe /c "$($cassandraDirectory)\bin\nodetool.bat" clearsnapshot -t $backupName $keyspaceName
}


Write-Host
Read-Host -Prompt 'Press Enter to exit'