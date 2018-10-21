dotnet build
coverlet ./bin/Debug/netcoreapp2.1/VideotapesGalore.Tests.dll --target "dotnet" --targetargs "test --no-build" --format opencover --include "[*]VideotapesGalore.Services.*"

# Remove lines 5 - 18 from coverage
sed -i -e '5,18d;' ./coverage.opencover.xml

reportgenerator -reports:coverage.opencover.xml -targetdir:./coverage 
