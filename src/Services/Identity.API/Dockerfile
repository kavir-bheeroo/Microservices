FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY src/Services/Identity.API/Identity.API.csproj /Services/Identity.API/
COPY src/BuildingBlocks/HealthChecks/HealthChecks.csproj /BuildingBlocks/HealthChecks/

# Copy everything else and build
COPY . .
WORKDIR src/Services/Identity.API
RUN dotnet build -c Release -o /app

FROM build AS publish 
RUN dotnet publish -c Release -o /app

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.API.dll"]