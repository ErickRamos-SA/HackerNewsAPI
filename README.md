# hnAPI - Hacker News Minimal API

Una API minimalista construida con .NET 9 que obtiene las mejores historias de tecnología desde Hacker News.

## ?? Características

- ? API Minimalista con .NET 9
- ??? Arquitectura limpia (Clean Architecture)
- ?? Obtiene las mejores historias de Hacker News
- ? Optimizada con Native AOT
- ?? Procesamiento paralelo para mejor rendimiento
- ?? Source Generators para JSON serialization

## ??? Arquitectura

El proyecto sigue los principios de Clean Architecture:

```
hnAPI/
??? Domain/          # Entidades y contratos
??? Application/     # Casos de uso y DTOs
??? Infrastructure/  # Repositorios e implementaciones externas
??? Presentation/    # Endpoints y extensiones de configuración
```

## ?? Requisitos

- .NET 9.0 SDK
- Visual Studio 2022 (17.8 o superior) o VS Code

## ??? Instalación y Configuración

1. **Clonar el repositorio**
   ```bash
   git clone <repository-url>
   cd MinimalAPI
   ```

2. **Configurar la aplicación**
   
   Asegúrate de tener la configuración necesaria en `appsettings.json`:
   ```json
   {
     "StoryApi": {
       "BaseUrl": "https://hacker-news.firebaseio.com/v0/"
     }
   }
   ```

3. **Ejecutar la aplicación**
   ```bash
   cd hnAPI
   dotnet run
   ```

## ?? Endpoints

### Obtener Top Stories

```http
GET /tecnologynews?n={cantidad}
```

**Parámetros:**
- `n` (int): Número de historias a obtener

**Respuesta:**
```json
[
  {
    "title": "Título de la historia",
    "uri": "https://example.com",
    "postedBy": "usuario",
    "time": "2024-01-01T00:00:00Z",
    "score": 150,
    "commentCount": 25
  }
]
```

**Ejemplo:**
```bash
curl "https://localhost:5001/tecnologynews?n=10"
```

## ??? Estructura del Proyecto

### Domain Layer
- `Story`: Entidad principal que representa una historia
- `IStoryRepository`: Contrato del repositorio

### Application Layer
- `StoryDto`: DTO para transferencia de datos
- `GetTopStoriesUseCase`: Caso de uso principal

### Infrastructure Layer
- `StoryRepository`: Implementación del repositorio que consume Hacker News API
- `HnApiJsonContext`: Context para JSON serialization
- `StoryApiOptions`: Configuración de la API

### Presentation Layer
- `StoryEndpoints`: Definición de endpoints
- `ServiceCollectionExtensions`: Extensiones para DI
- `UseCaseExtensions`: Registro de casos de uso

## ?? Características Técnicas

- **Native AOT**: Optimizado para compilación ahead-of-time
- **Slim Builder**: Uso de `WebApplication.CreateSlimBuilder` para mejor rendimiento
- **Source Generators**: JSON serialization optimizada
- **Procesamiento Paralelo**: Múltiples requests concurrentes para mejor rendimiento
- **Globalization Invariant**: Configurado para ser independiente de la cultura

## ?? Implementación en Producción

### ?? Dockerización

**Dockerfile**
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY ["hnAPI/hnAPI.csproj", "hnAPI/"]
RUN dotnet restore "hnAPI/hnAPI.csproj"
COPY . .
WORKDIR "/src/hnAPI"
RUN dotnet publish "hnAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hnAPI.dll"]
```

**docker-compose.yml**
```yaml
version: '3.8'
services:
  hnapi:
    build: .
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - StoryApi__BaseUrl=https://hacker-news.firebaseio.com/v0/
    restart: unless-stopped
```

### ?? Configuración de Producción

**appsettings.Production.json**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "StoryApi": {
    "BaseUrl": "https://hacker-news.firebaseio.com/v0/"
  },
  "AllowedHosts": "*",
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://0.0.0.0:8080"
      }
    }
  }
}
```

### ?? Despliegue en la Nube

#### Azure Container Apps
```bash
az containerapp create \
  --name hnapi \
  --resource-group rg-hnapi \
  --environment hnapi-env \
  --image youregistry.azurecr.io/hnapi:latest \
  --target-port 8080 \
  --ingress external
```

#### AWS ECS Fargate
```bash
# Crear task definition y service
aws ecs create-service \
  --cluster hnapi-cluster \
  --service-name hnapi-service \
  --task-definition hnapi-task:1 \
  --desired-count 2 \
  --launch-type FARGATE
```

#### Google Cloud Run
```bash
gcloud run deploy hnapi \
  --image gcr.io/PROJECT-ID/hnapi \
  --platform managed \
  --region us-central1 \
  --allow-unauthenticated \
  --port 8080
```

### ?? Mejoras Recomendadas para Producción

- **Health Checks**: `app.MapHealthChecks("/health")`
- **Rate Limiting**: Configurar límites de requests por minuto
- **CORS**: Configurar orígenes permitidos
- **HTTPS**: Forzar conexiones seguras
- **Logging**: Implementar logging estructurado
- **Monitoring**: Configurar métricas y alertas

## ?? Desarrollo

### Ejecutar en modo desarrollo
```bash
dotnet run --environment Development
```

### Compilar para producción
```bash
dotnet publish -c Release
```

### Compilar con AOT
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

## ?? Tecnologías Utilizadas

- .NET 9
- ASP.NET Core Minimal APIs
- System.Text.Json con Source Generators
- HttpClient para consumo de APIs externas

## ?? Contribución

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## ?? Licencia

Este proyecto está bajo la licencia MIT. Ver el archivo `LICENSE` para más detalles.

## ?? Contacto

Para preguntas o sugerencias, por favor crear un issue en el repositorio.