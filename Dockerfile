# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80
WORKDIR /app

# Copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . .
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hwAPI.dll"]