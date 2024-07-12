# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY FileStorageApp.sln .
COPY FileStorageApp.WebApi/FileStorageApp.WebApi.csproj ./FileStorageApp.WebApi/
COPY FileStorageApp.Application/FileStorageApp.Application.csproj ./FileStorageApp.Application/
COPY FileStorageApp.Domain/FileStorageApp.Domain.csproj ./FileStorageApp.Domain/
COPY FileStorageApp.Persistence/FileStorageApp.Persistence.csproj ./FileStorageApp.Persistence/
RUN dotnet restore ./FileStorageApp.WebApi/FileStorageApp.WebApi.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/FileStorageApp.WebApi
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileStorageApp.WebApi.dll"]
