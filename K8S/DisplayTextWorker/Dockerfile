FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src
# speed up local container build by taking advantage of Docker's build cache for nuget packages
COPY ./*.sln ./
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore "K8S.sln"
COPY . .
WORKDIR "/src/DisplayTextWorker"
RUN dotnet publish "DisplayTextWorker.csproj" --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DisplayTextWorker.dll"]