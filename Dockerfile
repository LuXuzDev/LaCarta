# Usa .NET 8.0 (no 9.0, que a�n no es estable)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia y restaura la soluci�n (incluye todos los proyectos)
COPY *.sln .
COPY LaCartaAPI/. ./LaCartaAPI/
COPY Data/*.csproj ./Data/
COPY Business/*.csproj ./Business/
COPY Entities/*.csproj ./Entities/

# Restaura dependencias
RUN dotnet restore

# Copia todo el c�digo
COPY LaCartaAPI/. ./LaCartaAPI/
COPY Data/. ./Data/
COPY Business/. ./Business/
COPY Entities/*.csproj ./Entities/

# Publica la API principal
RUN dotnet publish "LaCartaAPI/LaCartaAPI.csproj" -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Puerto
ENV ASPNETCORE_URLS=http://*:8080

# Volumen para almacenamiento persistente
VOLUME /app/data

# Punto de entrada
ENTRYPOINT ["dotnet", "LaCartaAPI.dll"]