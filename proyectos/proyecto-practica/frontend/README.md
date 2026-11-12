# Proyecto Práctica · Frontend Angular

Área de cliente de la aseguradora ficticia *Secura Insurance*. Angular 19 (standalone, signals, control flow), Bootstrap 5, ngx-bootstrap (datepicker) y ngx-translate (español/inglés).

Consume la API .NET 10 de [`../backend`](../backend/README.md), desplegada en Render:

- API: https://proyecto-practica-api.onrender.com
- Swagger: https://proyecto-practica-api.onrender.com/swagger

## Pantallas

| Ruta | Qué hace | Endpoint |
|---|---|---|
| `#/inicio` | Próximas renovaciones y clientes potenciales (la papelera borra de verdad) | `GET /api/renovaciones/proximas`, `GET`/`DELETE /api/clientes-potenciales` |
| `#/renovaciones` | Listado con filtros, orden y paginación resueltos en el servidor | `GET /api/renovaciones?q=&estado=&desde=&hasta=&importeMin=&importeMax=&orden=&pagina=&tamano=` |
| `#/renovaciones/detalles/:id` | Detalle con cinco tarjetas editables. "Guardar" hace un `PUT` del bloque, "Cancelar" restaura lo guardado y los errores de validación salen bajo cada campo | `GET /api/polizas/{id}`, `PUT /api/polizas/{id}/{bloque}` |

Si la API tarda más de 2,5 s (arranque en frío del plan gratuito de Render) se muestra un aviso arriba de la página.

## Desarrollo

```bash
npm install
npm start          # http://localhost:4200, usa la API local (http://localhost:5080)
```

Para la API local: `dotnet run --project ../backend/src/ProyectoPractica.Api`.

## Publicar en el portfolio

```bash
npm run build
```

Compila en `publicados/proyecto-practica/` (en la raíz del repositorio), que el portfolio sirve como estático (Vercel). La URL de la API de producción está en `src/environments/environment.ts`.
