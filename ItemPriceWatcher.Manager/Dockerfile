FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ItemPriceWatcher.Manager/ItemPriceWatcher.Manager.csproj", "ItemPriceWatcher.Manager/"]
RUN dotnet restore "ItemPriceWatcher.Manager/ItemPriceWatcher.Manager.csproj"
COPY . .
WORKDIR "/src/ItemPriceWatcher.Manager"
RUN dotnet build "ItemPriceWatcher.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItemPriceWatcher.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItemPriceWatcher.Manager.dll"]
