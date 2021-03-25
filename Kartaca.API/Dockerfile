#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Kartaca.API/Kartaca.API.csproj", "Kartaca.API/"]
RUN dotnet restore "Kartaca.API/Kartaca.API.csproj"
COPY . .
WORKDIR "/src/Kartaca.API"
RUN dotnet build "Kartaca.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kartaca.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kartaca.API.dll"]