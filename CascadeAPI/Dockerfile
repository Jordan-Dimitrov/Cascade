FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER root
RUN apt-get update && apt-get install -y ffmpeg
USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CascadeAPI/CascadeAPI.csproj", "CascadeAPI/"]
COPY ["MusicModule/Core/Music.Application/Music.Application.csproj", "MusicModule/Core/Music.Application/"]
COPY ["Shared/Application.Shared/Application.Shared.csproj", "Shared/Application.Shared/"]
COPY ["Shared/Domain.Shared/Domain.Shared.csproj", "Shared/Domain.Shared/"]
COPY ["UserModule/External/IntegrationEvents/Users.IntegrationEvents.csproj", "UserModule/External/IntegrationEvents/"]
COPY ["MusicModule/External/Music.IntegrationEvents/Music.IntegrationEvents.csproj", "MusicModule/External/Music.IntegrationEvents/"]
COPY ["MusicModule/Core/Music.Domain/Music.Domain.csproj", "MusicModule/Core/Music.Domain/"]
COPY ["MusicModule/External/Music.Infrastructure/Music.Infrastructure.csproj", "MusicModule/External/Music.Infrastructure/"]
COPY ["Shared/Infrastructure.Shared/Infrastructure.Shared.csproj", "Shared/Infrastructure.Shared/"]
COPY ["MusicModule/External/Music.Persistence/Music.Persistence.csproj", "MusicModule/External/Music.Persistence/"]
COPY ["Shared/Persistence.Shared/Persistence.Shared.csproj", "Shared/Persistence.Shared/"]
COPY ["MusicModule/External/Music.Presentation/Music.Presentation.csproj", "MusicModule/External/Music.Presentation/"]
COPY ["Shared/Presentation.Shared/Presentation.Shared.csproj", "Shared/Presentation.Shared/"]
COPY ["StreamingService/Streaming.Application/Streaming.Application.csproj", "StreamingService/Streaming.Application/"]
COPY ["StreamingService/Streaming.Infrastructure/Streaming.Infrastructure.csproj", "StreamingService/Streaming.Infrastructure/"]
COPY ["StreamingService/Streaming.Presentation/Streaming.Presentation.csproj", "StreamingService/Streaming.Presentation/"]
COPY ["UserModule/Core/Application/Users.Application.csproj", "UserModule/Core/Application/"]
COPY ["UserModule/Core/Domain/Users.Domain.csproj", "UserModule/Core/Domain/"]
COPY ["UserModule/External/Infrastructure/Users.Infrastructure.csproj", "UserModule/External/Infrastructure/"]
COPY ["UserModule/External/Persistence/Users.Persistence.csproj", "UserModule/External/Persistence/"]
COPY ["UserModule/External/Presentation/Users.Presentation.csproj", "UserModule/External/Presentation/"]
RUN dotnet restore "./CascadeAPI/./CascadeAPI.csproj"
COPY . .
WORKDIR "/src/CascadeAPI"
RUN dotnet build "./CascadeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CascadeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CascadeAPI.dll"]
