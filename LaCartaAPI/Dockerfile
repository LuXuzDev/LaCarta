# Usa la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .

# Puerto que usa Fly.io
ENV ASPNETCORE_URLS=http://*:8080

# Directorio donde guardarás archivos (ej: subidas)
VOLUME /app/data

# Comando para ejecutar la app
ENTRYPOINT ["dotnet", "LaCarta.dll"]

