Clear-Host

# get all scripts files
$files = Get-ChildItem -Path ./tests/. -Filter *.js -File 
$selectedFileIndex = 0

$isMenuAction = $true
while ($isMenuAction)
{
	# draw menu
    Clear-Host
    Write-Host 'Select file to run:'
	for ($i=0; $i -lt $files.Count; $i++) {
        if ($selectedFileIndex -eq $i) {
            Write-Host " * " -NoNewline
        } else {
            Write-Host "   " -NoNewline
        }

       Write-Host "$($files[$i].Name)"
    }
    
	# get user input
	$key = $Host.UI.RawUI.ReadKey()
	switch ($key.VirtualKeyCode) {
		38 { #down key
			if ($selectedFileIndex -gt 0) {
				$selectedFileIndex = $selectedFileIndex - 1
			}
		}

		40 { #up key
			if ($selectedFileIndex -lt $files.Length - 1) {
				$selectedFileIndex = $selectedFileIndex + 1
			}
		}
		13 { #enter key
			$isMenuAction = $false
		}
	}    
}

# run test with K6
k6 run $files[$selectedFileIndex].FullName 

Read-Host -Prompt 'Press any key to exit'