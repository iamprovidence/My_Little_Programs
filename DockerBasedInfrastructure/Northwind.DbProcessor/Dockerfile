FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.300 AS build
WORKDIR /src
COPY ["DockerBasedInfrastructure/Northwind.DbProcessor", "Container/Northwind.DbProcessor"]
COPY ["DockerBasedInfrastructure/Northwind", "Container/Northwind"]
RUN dotnet restore "Container/Northwind.DbProcessor/Northwind.DbProcessor.csproj"
WORKDIR "/src/Container/Northwind.DbProcessor"
RUN dotnet build "Northwind.DbProcessor.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "Northwind.DbProcessor.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Below 2 lines add support for Globalization Invariant Mode
# https://github.com/dotnet/SqlClient/issues/220#issue-498595465
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "Northwind.DbProcessor.dll"]
