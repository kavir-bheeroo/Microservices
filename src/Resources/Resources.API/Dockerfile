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
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Resources.API.dll"]