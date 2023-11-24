function Write-Console {
    [CmdletBinding()]
    param (
        [Parameter(ValueFromPipeline)]
        $InputObject
    )

    process {
        Write-Host $InputObject

        Return ($InputObject)
    }
}

# Deleting all BIN and OBJ folders...
Clear

Write-Host "Deleting all BIN and OBJ folders:"
Write-Host ""

Get-ChildItem -Path $PSScriptRoot -Recurse -Directory -Include bin, obj | 
Select-Object -ExpandProperty FullName |
Where-Object { $PsItem -notmatch "\\node_modules\\" } |
Write-Console |
Remove-Item -Recurse -Force

Write-Host ""
Write-Host "BIN and OBJ folders have been successfully deleted"
Pause
