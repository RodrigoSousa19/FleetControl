# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FleetControl.API/FleetControl.API.csproj", "FleetControl.API/"]
COPY ["FleetControl.Application/FleetControl.Application.csproj", "FleetControl.Application/"]
COPY ["FleetControl.Core/FleetControl.Core.csproj", "FleetControl.Core/"]
COPY ["FleetControl.Infrastructure/FleetControl.Infrastructure.csproj", "FleetControl.Infrastructure/"]
RUN dotnet restore "./FleetControl.API/FleetControl.API.csproj"
COPY . .
WORKDIR "/src/FleetControl.API"
RUN dotnet build "./FleetControl.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FleetControl.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetControl.API.dll"]