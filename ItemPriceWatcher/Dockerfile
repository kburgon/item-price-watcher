FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

RUN apt-get update && apt-get install -y firefox-esr

COPY bin/Release/netcoreapp3.0/publish App/
WORKDIR /App
ENTRYPOINT [ "dotnet", "ItemPriceWatcher.dll" ]
