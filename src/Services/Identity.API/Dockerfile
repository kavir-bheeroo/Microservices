FROM microsoft/aspnetcore:latest AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:latest AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Microservices.sln ./
COPY src/Services/Identity.API/Identity.API.csproj /Services/Identity.API/
RUN dotnet restore

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