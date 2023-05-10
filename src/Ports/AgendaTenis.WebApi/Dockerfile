#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ports/AgendaTenis.WebApi/AgendaTenis.WebApi.csproj", "src/Ports/AgendaTenis.WebApi/"]
COPY ["src/Core/AgendaTenis.Core.Identity/AgendaTenis.Core.Identity.csproj", "src/Core/AgendaTenis.Core.Identity/"]
COPY ["src/Core/AgendaTenis.Core.Jogadores/AgendaTenis.Core.Jogadores.csproj", "src/Core/AgendaTenis.Core.Jogadores/"]
COPY ["src/Core/AgendaTenis.Core.Partidas/AgendaTenis.Core.Partidas.csproj", "src/Core/AgendaTenis.Core.Partidas/"]
RUN dotnet restore "src/Ports/AgendaTenis.WebApi/AgendaTenis.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Ports/AgendaTenis.WebApi"
RUN dotnet build "AgendaTenis.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgendaTenis.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendaTenis.WebApi.dll"]