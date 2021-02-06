#env /bin/bash

# Set current directory variable
cwd=$(pwd)

# Build ItemPriceWatcher release
cd ../ItemPriceWatcher/
dotnet publish -c release

# Build and run container
cd ${cwd}
docker-compose --env-file vars.env up --build -d