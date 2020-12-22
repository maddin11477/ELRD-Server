# FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.12-amd64 AS build-env
# FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine3.12-amd64
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.12-amd64 AS builder
ARG Configuration=Release
WORKDIR /src
COPY ["ELRDServerAPI/ELRDServerAPI.csproj", "ELRDServerAPI/"]
COPY ["ELRDDataAccessLibrary/ELRDDataAccessLibrary.csproj", "ELRDDataAccessLibrary/"]
RUN dotnet restore "ELRDServerAPI/ELRDServerAPI.csproj"
COPY . .
WORKDIR /src/ELRDServerAPI
RUN dotnet build -c $Configuration -o /app

FROM builder AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ELRDServerAPI.dll"]