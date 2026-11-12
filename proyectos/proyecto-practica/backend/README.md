# Proyecto Práctica · API .NET 10

Backend del proyecto **"Proyecto Práctica"** del portfolio (área personal de una aseguradora), consumido por el frontend Angular de [`../frontend`](../frontend/README.md).

- API en producción: https://proyecto-practica-api.onrender.com
- Documentación interactiva: https://proyecto-practica-api.onrender.com/swagger

**Stack:** .NET 10 · ASP.NET Core (controladores) · Clean Architecture · CQRS · FluentValidation · EF Core 10 (SQLite / PostgreSQL) · OpenAPI + Swagger UI · xUnit · Docker · Render

## Arquitectura

Clean Architecture con cuatro proyectos. Las dependencias apuntan siempre hacia el dominio:

```
            ┌──────────────────────────┐
            │  ProyectoPractica.Api    │  Controladores, contratos HTTP, ProblemDetails,
            │  (presentación + raíz    │  CORS, OpenAPI, health checks
            │   de composición)        │
            └────┬────────────────┬────┘
                 │                │
                 ▼                ▼
┌──────────────────────────┐   ┌───────────────────────────────┐
│ ProyectoPractica.        │◄──│ ProyectoPractica.             │  EF Core, repositorios,
│ Application              │   │ Infrastructure                │  consultas de lectura,
│ (casos de uso CQRS)      │   │ (detalles técnicos)           │  semilla de datos
└────────────┬─────────────┘   └───────────────────────────────┘
             ▼
┌──────────────────────────┐
│ ProyectoPractica.Domain  │  Entidades, value objects, reglas de negocio
│ (sin dependencias)       │  e interfaces de repositorio
└──────────────────────────┘
```

| Proyecto | Responsabilidad | Depende de |
|---|---|---|
| **Domain** | Agregado `Poliza` con estado encapsulado (setters privados, métodos `Actualizar*`, `EmitirCuota`) e invariantes; value objects inmutables (`Titular`, `Facturacion`, `Pago`, `Correspondencia`, `Contacto`) que normalizan su entrada; `ClientePotencial`; reglas españolas (NIF/NIE/CIF, IBAN, CP); contratos `IPolizaRepository`, `IClientePotencialRepository` e `IUnitOfWork`. | — |
| **Application** | Un caso de uso por clase: commands y queries con su manejador y su validador. Mediador propio con pipeline de comportamientos (la validación se ejecuta antes de cada manejador). Contratos del lado de lectura (`IRenovacionesLectura`, …), DTOs y excepciones de aplicación. | Domain |
| **Infrastructure** | `AppDbContext` (también es la unidad de trabajo), configuraciones de EF Core por entidad (`IEntityTypeConfiguration`), value objects como *complex types*, repositorios de escritura, consultas de lectura proyectadas a DTO (`AsNoTracking`) y semilla de datos. | Application, Domain |
| **Api** | Controladores finos que traducen HTTP a commands/queries y delegan en `IMediador`. `ManejadorExcepciones` convierte las excepciones en `ProblemDetails` (400, 404, 422). Configuración de CORS, OpenAPI y health checks. Es la raíz de composición. | Application, Infrastructure |

### CQRS

- **Escritura (commands):** `ActualizarTitularCommand`, `ActualizarPagoCommand`, …, `EliminarClientePotencialCommand`. Cargan el agregado por su repositorio, aplican el cambio a través del dominio y confirman con `IUnitOfWork`. Los cinco comandos de bloque comparten flujo mediante `ActualizarBloquePolizaHandler` (patrón *template method*).
- **Lectura (queries):** `ListarRenovacionesQuery`, `ObtenerProximasRenovacionesQuery`, `ObtenerRenovacionQuery`, `ObtenerDetallePolizaQuery`, `ListarClientesPotencialesQuery`. Usan servicios de lectura que proyectan directamente a DTO, sin cargar agregados ni hacer seguimiento de cambios.

### Principios SOLID aplicados

- **S**: un manejador por caso de uso, un validador por petición, un controlador por recurso. Los controladores no contienen lógica.
- **O**: el pipeline del mediador admite nuevos comportamientos (`IComportamientoPeticion<,>`) sin tocar los existentes. Los manejadores y validadores se registran solos por reflexión.
- **L**: los cinco manejadores de bloque se pueden usar donde se espera `ActualizarBloquePolizaHandler`.
- **I**: interfaces pequeñas y separadas para escritura (repositorios) y lectura (`I*Lectura`).
- **D**: Application solo conoce abstracciones; Infrastructure las implementa y Api las conecta.

## Estructura

```
backend/
├── ProyectoPractica.sln
├── Directory.Build.props          ← configuración común (net10.0, nullable, warnings como errores)
├── Directory.Packages.props       ← versiones de paquetes centralizadas
├── Dockerfile
├── src/
│   ├── ProyectoPractica.Domain/
│   │   ├── Comun/                 Entidad, ExcepcionDominio, Texto
│   │   ├── Polizas/               Poliza, Cuota, EstadoPoliza y value objects
│   │   ├── ClientesPotenciales/
│   │   ├── Reglas/                ReglasEspana
│   │   └── Repositorios/          IPolizaRepository, IClientePotencialRepository, IUnitOfWork
│   ├── ProyectoPractica.Application/
│   │   ├── Abstracciones/         Mensajeria (ICommand, IQuery, IMediador…) y Lectura
│   │   ├── Comun/                 Mediador, ComportamientoValidacion, excepciones, PaginaDto
│   │   ├── Renovaciones/Queries/
│   │   ├── Polizas/Commands/ y Polizas/Queries/
│   │   └── ClientesPotenciales/Commands/ y Queries/
│   ├── ProyectoPractica.Infrastructure/
│   │   ├── Persistencia/          AppDbContext, Configuraciones/, Semilla/
│   │   ├── Repositorios/
│   │   └── Lectura/
│   └── ProyectoPractica.Api/
│       ├── Controllers/
│       ├── Contratos/             cuerpos de las peticiones PUT
│       └── Infraestructura/       ManejadorExcepciones, ConfiguracionPresentacion
└── tests/
    ├── ProyectoPractica.Domain.Tests/          reglas e invariantes
    ├── ProyectoPractica.Application.Tests/     validadores y pipeline del mediador con dobles en memoria
    └── ProyectoPractica.Api.IntegrationTests/  API real contra SQLite temporal
```

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/renovaciones?q=&estado=&desde=&hasta=&importeMin=&importeMax=&orden=&pagina=&tamano=` | Listado paginado. `orden`: `vencimiento_asc` (por defecto), `vencimiento_desc`, `importe_asc`, `importe_desc` |
| GET | `/api/renovaciones/proximas?limite=3` | Próximas renovaciones desde hoy |
| GET | `/api/renovaciones/{id}` | Una renovación |
| GET | `/api/polizas/{id}` | Detalle completo + últimas 3 cuotas |
| PUT | `/api/polizas/{id}/titular` · `/facturacion` · `/pago` · `/correspondencia` · `/contacto` | Guarda un bloque (400 con errores por campo si no es válido) |
| GET | `/api/clientes-potenciales` | Clientes potenciales |
| DELETE | `/api/clientes-potenciales/{id}` | Borra un cliente potencial |
| GET | `/health` | Estado del servicio y de la base de datos |

Hay ejemplos listos para lanzar en [`ProyectoPractica.Api.http`](src/ProyectoPractica.Api/ProyectoPractica.Api.http). Ejemplo de error de validación:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "Uno o más campos no son válidos.",
  "status": 400,
  "errors": {
    "iban": ["El IBAN no es válido (revisa los dígitos de control)."]
  }
}
```

## Ejecutar en local

Requiere el SDK de .NET 10 (o posterior). Abre `ProyectoPractica.sln` en Visual Studio y arranca `ProyectoPractica.Api`, o desde la terminal:

```bash
dotnet run --project src/ProyectoPractica.Api
```

Abre http://localhost:5080/swagger. La primera vez crea `proyecto-practica.db` (SQLite) con la semilla; bórralo para regenerar los datos.

```bash
dotnet test
```

## Base de datos

- **Por defecto, SQLite.** Sin configuración. En Render (plan gratuito) el disco es efímero, así que la demo se reinicia con datos limpios cada vez que el servicio arranca.
- **PostgreSQL:** basta con definir `ConnectionStrings__Postgres`. El esquema se crea al arrancar y la semilla se carga si la base está vacía.

## Configuración

| Variable | Uso |
|---|---|
| `ConnectionStrings__Postgres` | Opcional. Si existe, usa PostgreSQL en lugar de SQLite |
| `Cors__Origins__0`, `Cors__Origins__1`… | Orígenes permitidos (por defecto el dominio del portfolio y `localhost:4200`) |
| `PORT` | Puerto en el que escuchar (lo define Render) |

## Despliegue en Render

Servicio web Docker: `dockerfilePath: ./proyectos/proyecto-practica/backend/Dockerfile`, `dockerContext: ./proyectos/proyecto-practica/backend` y health check en `/health` (ver [`render.yaml`](../../../render.yaml)).
