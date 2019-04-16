# always run restore, build and run when building the image
# FROM microsoft/aspnetcore:2.0
# WORKDIR /app
# COPY . .
# RUN dotnet restore
# RUN dotnet build
# ENTRYPOINT [ "dotnet", "Resources.API.dll" ]

# run dotnet build on the local machine and copy the published folder
# FROM microsoft/aspnetcore:2.0
# WORKDIR /app
# COPY bin/Debug/netcoreapp2.0/publish .
# ENTRYPOINT ["dotnet", "Resources.API.dll"]

# multi-stage build
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY src/Services/Resources.API/Resources.API.csproj /Services/Resources.API/
COPY src/BuildingBlocks/EventBus/EventBus.csproj /BuildingBlocks/EventBus/
COPY src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj /BuildingBlocks/EventBusRabbitMQ/
COPY src/BuildingBlocks/HealthChecks/HealthChecks.csproj /BuildingBlocks/HealthChecks/

# Copy everything else and build
COPY . .
WORKDIR src/Services/Resources.API
RUN dotnet build -c Release -o /app

FROM build AS publish 
RUN dotnet publish -c Release -o /app

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Resources.API.dll"]