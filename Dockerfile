# Usa .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Directorio para datos persistentes (aquí se montará el volumen)
# Este será el punto de montaje: /app/data
RUN mkdir -p /app/data && chmod -R 777 /app/data

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY API/*.csproj ./API/
COPY Data/*.csproj ./Data/
COPY Business/*.csproj ./Business/
COPY Domain/*.csproj ./Domain/

RUN dotnet restore

COPY API/. ./API/
COPY Data/. ./Data/
COPY Business/. ./Business/
COPY Domain/. ./Domain/

RUN dotnet publish "API/LaCartaAPI.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Puerto
ENV ASPNETCORE_URLS=http://*:8080

# Directorio donde se montará el volumen (Koyeb lo hará)
# Tú solo defines que existe y es accesible
VOLUME /app/data

# Punto de entrada
ENTRYPOINT ["dotnet", "LaCartaAPI.dll"]