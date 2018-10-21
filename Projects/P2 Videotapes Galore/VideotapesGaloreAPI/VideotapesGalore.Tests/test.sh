dotnet build
coverlet ./bin/Debug/netcoreapp2.1/VideotapesGalore.Tests.dll --target "dotnet" --targetargs "test --no-build" --format opencover
reportgenerator -reports:coverage.opencover.xml -targetdir:./coverage