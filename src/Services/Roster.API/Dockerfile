FROM microsoft/aspnetcore:latest AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:latest AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Microservices.sln ./
COPY src/Services/Roster.API/Roster.API.csproj /Services/Roster.API/
COPY src/BuildingBlocks/EventBus/EventBus.csproj /BuildingBlocks/EventBusRabbitMQ/
COPY src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj /BuildingBlocks/EventBusRabbitMQ/
COPY src/BuildingBlocks/HealthChecks/HealthChecks.csproj /BuildingBlocks/HealthChecks/

RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR src/Services/Roster.API
RUN dotnet build -c Release -o /app

FROM build AS publish 
RUN dotnet publish -c Release -o /app

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Roster.API.dll"]