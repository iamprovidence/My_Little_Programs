FROM mcr.microsoft.com/dotnet/aspnet:5.0
FROM mcr.microsoft.com/dotnet/sdk:5.0

COPY Domain Domain
COPY FunWithTests FunWithTests

RUN dotnet publish "FunWithTests/FunWithTests.csproj" -c Release -o out

WORKDIR ./out
ENTRYPOINT ["dotnet", "FunWithTests.dll"]