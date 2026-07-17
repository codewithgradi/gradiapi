# STAGE 1: Build & Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first to leverage Docker layer caching
COPY *.sln ./
COPY GradiApi/*.csproj ./GradiApi/
# Add copies for your Core/Infrastructure project layers here if you use Clean Architecture layout:
# COPY GradiApi.Core/*.csproj ./GradiApi.Core/
# COPY GradiApi.Infrastructure/*.csproj ./GradiApi.Infrastructure/

RUN dotnet restore

# Copy the remaining source files and build the app
COPY . .
WORKDIR /src/GradiApi
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# STAGE 2: Final Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render exposes traffic on port 8080 by default for Docker services
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "GradiApi.dll"]