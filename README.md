# hnAPI - Hacker News Minimal API

Una API minimalista construida con .NET 9 que obtiene las mejores historias de tecnología desde Hacker News.

## Características

- API Minimalista con .NET 9
- Arquitectura limpia (Clean Architecture)
- Obtiene las mejores historias de Hacker News
- Optimizada con Native AOT
- Procesamiento paralelo para mejor rendimiento
- Source Generators para JSON serialization

## Arquitectura

El proyecto sigue los principios de Clean Architecture:

```
hnAPI/
     Domain/          # Entidades y contratos
     Application/     # Casos de uso y DTOs
     Infrastructure/  # Repositorios e implementaciones externas
     Presentation/    # Endpoints y extensiones de configuración
```

## Requisitos

- .NET 9.0 SDK

## Instalación y Configuración

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

## Endpoints

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

## Estructura del Proyecto

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

## Características Técnicas

- **Native AOT**: Optimizado para compilación ahead-of-time
- **Slim Builder**: Uso de `WebApplication.CreateSlimBuilder` para mejor rendimiento
- **Source Generators**: JSON serialization optimizada
- **Procesamiento Paralelo**: Múltiples requests concurrentes para mejor rendimiento

## Implementación

###  Configuración 

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

### Compilar para producción
```bash
dotnet publish -c Release
```

### Compilar con AOT
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

###  Mejoras Recomendadas para Producción

- **Health Checks**: `app.MapHealthChecks("/health")`
- **Rate Limiting**: Configurar límites de requests por minuto
- **CORS**: Configurar orígenes permitidos
- **HTTPS**: Forzar conexiones seguras
- **Logging**: Implementar logging estructurado
- **Monitoring**: Configurar métricas y alertas
- **Tokens**: Agregar seguridad por token
- **Redis**: Agregar redis para no consultar los servicios en cada consulta

