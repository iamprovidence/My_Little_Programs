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

# Get user input
Write-Host "Available keyspaces :"
$keyspaceList = Get-ChildItem -Directory "$($cassandraDirectory)\data\data" `
| Select-Object -ExpandProperty Name `
| Where-Object { -not $_.StartsWith("system")} `
| Sort-Object
$keyspaceList
Write-Host

do
{
    $keyspaceName = Read-Host -Prompt 'Input keyspace name'
} while(-not ($keyspaceList -contains $keyspaceName))

$backupName = Read-Host -Prompt 'Input backup name'

# Create backup
cmd.exe /c "$($cassandraDirectory)\bin\nodetool.bat" cleanup $keyspaceName
cmd.exe /c "$($cassandraDirectory)\bin\nodetool.bat" snapshot --tag $backupName $keyspaceName

Write-Host
Read-Host -Prompt 'Press Enter to exit'
