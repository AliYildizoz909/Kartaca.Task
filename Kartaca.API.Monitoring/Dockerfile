#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Kartaca.API.Monitoring/Kartaca.API.Monitoring.csproj", "Kartaca.API.Monitoring/"]
RUN dotnet restore "Kartaca.API.Monitoring/Kartaca.API.Monitoring.csproj"
COPY . .
WORKDIR "/src/Kartaca.API.Monitoring"
RUN dotnet build "Kartaca.API.Monitoring.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kartaca.API.Monitoring.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kartaca.API.Monitoring.dll"]