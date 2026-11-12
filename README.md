# Portfolio · Fernando Hernández Lázaro

Portfolio personal publicado en https://ferherlazporfolio.com. La web principal está hecha con Angular 18 (componentes standalone y signals) y es bilingüe (inglés / español). Desde su galería se abre cada proyecto, servido dentro del propio portfolio.

## Estructura

```
PORTFOLIO_ferherlaz/
├── portfolio/                      Web principal (Angular) · se despliega en Vercel
│   ├── src/
│   │   ├── app/
│   │   │   ├── core/               i18n (textos en/es), servicio de idioma, datos de perfil
│   │   │   ├── layout/             Cabecera y pie
│   │   │   └── features/home/      Secciones: hero, sobre mí, servicios, habilidades, proyectos, contacto
│   │   ├── index.html
│   │   └── styles.css
│   ├── public/                     Recursos propios del portfolio: img/, curriculum/, favicon, manifest
│   ├── angular.json
│   └── package.json
│
├── proyectos/                      Una carpeta por proyecto de la galería, con todo lo suyo dentro
│   ├── blog-chile/
│   │   ├── frontend/               Código React
│   │   └── publicado/              Build que se ve en el portfolio
│   ├── nirvana/
│   │   └── publicado/              HTML, CSS y JS (no necesita compilarse)
│   ├── proyecto-practica/          "Secura Insurance"
│   │   ├── frontend/               Angular 19 + Bootstrap
│   │   ├── backend/                API .NET 10 · Clean Architecture + CQRS · se despliega en Render
│   │   └── publicado/              Build del frontend
│   ├── quiz-pokemon/
│   │   └── publicado/              HTML, CSS y JS + la versión PHP original
│   └── the-magic-barber/
│       ├── publicar.mjs            Genera su build (el código fuente está en otro repositorio)
│       └── publicado/
│
├── vercel.json                     Cómo compila Vercel la web (desde portfolio/)
└── render.yaml                     Referencia del servicio de la API en Render
```

**Regla general:** cada proyecto vive en `proyectos/<proyecto>/`. Lo que hay en su carpeta `publicado/` es exactamente lo que se ve en la web; si el proyecto tiene código fuente, está al lado (`frontend/`, `backend/`).

## Cómo se publica cada proyecto

Al compilar el portfolio, Angular copia en la web final, además de `portfolio/public/`, la carpeta `publicado/` de cada proyecto:

| URL en la web | Origen en el repositorio |
| --- | --- |
| `/blogchile/` | `proyectos/blog-chile/publicado/` |
| `/nirvana/` | `proyectos/nirvana/publicado/` |
| `/proyecto-practica/` | `proyectos/proyecto-practica/publicado/` |
| `/QuizPokemon/QuizPokemon/` | `proyectos/quiz-pokemon/publicado/` |
| `/the-magic-barber/` | `proyectos/the-magic-barber/publicado/` |

Esta correspondencia está en los `assets` de [`portfolio/angular.json`](portfolio/angular.json). Las URLs son las de siempre, así que los enlaces de la galería no cambian.

| Proyecto | Cómo actualizarlo |
| --- | --- |
| Blog Chile | `npm run build` en `frontend/` y copiar `build/` (sin los `.map`) a `publicado/` |
| Nirvana Narguile | Editar directamente `publicado/` |
| Secura Insurance | `npm run build` en `frontend/` (compila directamente en `publicado/`) |
| Quiz Pokémon | Editar directamente `publicado/` |
| The Magic Barber | Desde la raíz: `node proyectos/the-magic-barber/publicar.mjs` |

## Comandos de la web principal

Desde `portfolio/`:

| Comando | Qué hace |
| --- | --- |
| `npm start` | Servidor de desarrollo en `http://localhost:4200/` |
| `npm run build` | Build de producción en `portfolio/dist/portfolio/browser/` |
| `npm test` | Tests unitarios con Karma |

## Despliegue

- **Web (Vercel):** `vercel.json` indica a Vercel que instale y compile dentro de `portfolio/` y publique `portfolio/dist/portfolio/browser`.
- **API (Render):** servicio Docker con `dockerfilePath: ./proyectos/proyecto-practica/backend/Dockerfile` y `dockerContext: ./proyectos/proyecto-practica/backend`. Detalles en el [README del backend](proyectos/proyecto-practica/backend/README.md).

## Formulario de contacto

La web es estática: al enviar el formulario se abre el programa de correo del visitante con el mensaje ya redactado para `ferherlaz@gmail.com`.
