FROM mcr.microsoft.com/dotnet/sdk:6.0

COPY . /app
WORKDIR /app
RUN dotnet build

CMD dotnet run --no-build 