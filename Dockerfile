# STAGE 1: Build & Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj file from the current root directory
COPY *.csproj ./
RUN dotnet restore

# Copy all your local source code folders (Controllers, Data, Models, etc.)
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# STAGE 2: Final Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Force the container to expose traffic on port 8080 for Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "gradiapi.dll"]