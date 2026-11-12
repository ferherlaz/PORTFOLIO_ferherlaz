# --- Compilación ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Primero solo los proyectos y la configuración común: la restauración se cachea mientras no cambien.
COPY Directory.Build.props Directory.Packages.props ./
COPY src/ProyectoPractica.Domain/ProyectoPractica.Domain.csproj src/ProyectoPractica.Domain/
COPY src/ProyectoPractica.Application/ProyectoPractica.Application.csproj src/ProyectoPractica.Application/
COPY src/ProyectoPractica.Infrastructure/ProyectoPractica.Infrastructure.csproj src/ProyectoPractica.Infrastructure/
COPY src/ProyectoPractica.Api/ProyectoPractica.Api.csproj src/ProyectoPractica.Api/
RUN dotnet restore src/ProyectoPractica.Api/ProyectoPractica.Api.csproj

COPY src/ src/
RUN dotnet publish src/ProyectoPractica.Api/ProyectoPractica.Api.csproj \
    -c Release -o /app/publish --no-restore /p:UseAppHost=false

# --- Ejecución ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# SQLite en /tmp: el contenedor corre sin root y /app es de solo lectura para él.
# En Render (plan gratuito) el disco es efímero: la base se recrea con la semilla en cada arranque.
ENV ASPNETCORE_HTTP_PORTS=8080 \
    ConnectionStrings__Sqlite="Data Source=/tmp/proyecto-practica.db"
EXPOSE 8080
USER $APP_UID

ENTRYPOINT ["dotnet", "ProyectoPractica.Api.dll"]
