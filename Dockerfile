# ����������� ����������� SDK .NET � �������� ����� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 90

# ���������� ����� csproj � ������������ ����������� ��� ������� �������
COPY FileStorageApp.WebApi/FileStorageApp.WebApi.csproj FileStorageApp.WebApi/
COPY FileStorageApp.Application/FileStorageApp.Application.csproj FileStorageApp.Application/
COPY FileStorageApp.Domain/FileStorageApp.Domain.csproj FileStorageApp.Domain/
COPY FileStorageApp.Persistence/FileStorageApp.Persistence.csproj FileStorageApp.Persistence/
RUN dotnet restore FileStorageApp.WebApi/FileStorageApp.WebApi.csproj

# ���������� ���������� �������� ��� � �������� ����������
COPY . .
WORKDIR /app/FileStorageApp.WebApi
RUN dotnet build FileStorageApp.WebApi.csproj -c Release -o /app/build

# ����������� ����������
RUN dotnet publish FileStorageApp.WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# �������� ����� runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FileStorageApp.WebApi.dll"]
