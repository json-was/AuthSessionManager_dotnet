FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AuthSessionManager.API/AuthSessionManager.API.csproj", "src/AuthSessionManager.API/"]
COPY ["src/AuthSessionManager.Application/AuthSessionManager.Application.csproj", "src/AuthSessionManager.Application/"]
COPY ["src/AuthSessionManager.Domain/AuthSessionManager.Domain.csproj", "src/AuthSessionManager.Domain/"]
COPY ["src/AuthSessionManager.Infrastructure/AuthSessionManager.Infrastructure.csproj", "src/AuthSessionManager.Infrastructure/"]
RUN dotnet restore "src/AuthSessionManager.API/AuthSessionManager.API.csproj"
COPY . .
WORKDIR "/src/src/AuthSessionManager.API"
RUN dotnet build "./AuthSessionManager.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthSessionManager.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthSessionManager.API.dll"]
