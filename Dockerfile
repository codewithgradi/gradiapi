# STAGE 1: Build & Publish (Using .NET 10 SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the csproj file from the current root directory
COPY *.csproj ./
RUN dotnet restore

# Copy all your local source code folders
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# STAGE 2: Final Runtime (Using .NET 10 ASP.NET Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Force the container to expose traffic on port 8080 for Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "gradiapi.dll"]