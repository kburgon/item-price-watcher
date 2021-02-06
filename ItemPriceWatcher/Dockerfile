FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN apt-get update && apt-get install -y firefox-esr

COPY bin/release/net5.0/publish App/
WORKDIR /App
ENTRYPOINT [ "dotnet", "ItemPriceWatcher.dll" ]
