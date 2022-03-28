dotnet tool restore

dotnet tool run coverlet .\bin\Debug\net5.0\FunWithTests.UnitTests.dll `
	--target "dotnet" `
	--targetargs "test --no-build" `
	# --exclude "[*]Domain*"

dotnet test --collect:"XPlat Code Coverage"

$coverageFile = (Get-ChildItem -Recurse -Filter 'coverage.cobertura.xml' | Sort-Object -Property CreationTime | Select-Object -First 1).FullName

dotnet tool run reportgenerator	`
	-reports:$coverageFile `
	-targetdir:"coverageresults" `
	-reporttypes:Html

dotnet stryker