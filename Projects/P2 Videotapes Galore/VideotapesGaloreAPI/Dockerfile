FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY VideotapesGalore.WebApi/*.csproj ./VideotapesGalore.WebApi/
COPY VideotapesGalore.Services/*.csproj ./VideotapesGalore.Services/
COPY VideotapesGalore.Repositories/*.csproj ./VideotapesGalore.Repositories/
COPY VideotapesGalore.Models/*.csproj ./VideotapesGalore.Models/
RUN cd ./VideotapesGalore.WebApi/ && dotnet restore

# Copy everything else and build
COPY . .
RUN cd ./VideotapesGalore.WebApi/ && dotnet build -c Release -o /app
RUN cd ./VideotapesGalore.WebApi/ && dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VideotapesGalore.WebApi.dll"]