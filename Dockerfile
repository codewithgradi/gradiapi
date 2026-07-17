# STAGE 1: Build & Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first
COPY *.sln ./
# ⬇️ Changed to lowercase to match your repository's casing
COPY gradiapi/*.csproj ./gradiapi/

RUN dotnet restore

# Copy the remaining source files and build the app
COPY . .
# ⬇️ Changed to lowercase here as well
WORKDIR /src/gradiapi
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# STAGE 2: Final Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# ⬇️ Make sure the output DLL matches your project assembly name
ENTRYPOINT ["dotnet", "gradiapi.dll"]