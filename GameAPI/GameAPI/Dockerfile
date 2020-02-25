#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GameAPI/GameAPI.csproj", "GameAPI/"]
COPY ["GameAPI.Data/GameAPI.Data.csproj", "GameAPI.Data/"]
COPY ["GameAPI.Domain/GameAPI.Domain.csproj", "GameAPI.Domain/"]
RUN dotnet restore "GameAPI/GameAPI.csproj"
COPY . .
WORKDIR "/src/GameAPI"
RUN dotnet build "GameAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameAPI.dll"]