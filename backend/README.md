# Proyecto Práctica · API .NET 10

Backend del proyecto **"Proyecto Práctica"** del portfolio (área personal de una aseguradora).
Sustituye los mocks JSON del frontend Angular por una API REST real.

**Stack:** .NET 10 · ASP.NET Core Minimal APIs · EF Core 10 (SQLite / PostgreSQL) · OpenAPI + Swagger UI · xUnit · Docker · Render

## Qué resuelve

| # | Antes (solo frontend) | Ahora (con la API) |
|---|---|---|
| 1 | "Guardar" en el detalle no guardaba nada y "Cancelar" no deshacía | `GET /api/polizas/{id}` y un `PUT` por bloque: titular, facturación, pago, correspondencia y contacto |
| 2 | Sin validación | NIF/NIE/CIF con letra o dígito de control, IBAN (módulo 97), código postal (provincia 01-52), email y teléfono. Errores en formato `ProblemDetails` con la clave de cada campo |
| 3 | Se descargaban las 50 renovaciones y se filtraba en el navegador | Filtros, orden y paginación en servidor |
| 4 | Solo 3 de 50 pólizas tenían detalle | Semilla con las 50 pólizas completas y datos válidos, con fechas relativas a hoy |
| 5 | "Próximas renovaciones" escritas a mano y la papelera solo ocultaba la tarjeta | `GET /api/renovaciones/proximas` y `DELETE /api/clientes-potenciales/{id}` real |

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
| GET | `/health` | Comprobación de salud para Render |

Documentación interactiva en `/swagger`. Ejemplos listos para lanzar en [`ProyectoPractica.Api.http`](src/ProyectoPractica.Api/ProyectoPractica.Api.http).

Ejemplo de error de validación:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "iban": ["El IBAN no es válido (revisa los dígitos de control)."]
  }
}
```

## Ejecutar en local

Requiere el SDK de .NET 10.

```bash
cd backend
dotnet run --project src/ProyectoPractica.Api
```

Abre http://localhost:5080/swagger. La primera vez crea `proyecto-practica.db` (SQLite) con la semilla; bórralo para regenerar los datos.

Tests (unitarios de validación + integración contra la API real con SQLite temporal):

```bash
dotnet test
```

## Base de datos

- **Por defecto, SQLite.** Sin configuración. En Render (plan gratuito) el disco es efímero, así que la demo se reinicia con datos limpios cada vez que el servicio arranca.
- **PostgreSQL**: basta con definir la variable `ConnectionStrings__Postgres`. El esquema se crea solo al arrancar (`EnsureCreated`) y la semilla se carga si la base está vacía.

## Configuración

| Variable | Uso |
|---|---|
| `ConnectionStrings__Postgres` | Opcional. Si existe, usa PostgreSQL en lugar de SQLite |
| `Cors__Origins__0`, `Cors__Origins__1`… | Orígenes permitidos (por defecto el dominio del portfolio y `localhost:4200`) |
| `PORT` | Puerto en el que escuchar (lo define Render) |

## Despliegue en Render

El fichero [`render.yaml`](../render.yaml) de la raíz del repositorio define el servicio como Docker, con `rootDir: backend` y health check en `/health`. Solo se redespliega cuando cambia algo dentro de `backend/`.
