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
├── proyectos/                      Código fuente de los proyectos de la galería
│   ├── proyecto-practica/          "Secura Insurance"
│   │   ├── frontend/               Angular 19 + Bootstrap
│   │   └── backend/                API .NET 10 · Clean Architecture + CQRS · se despliega en Render
│   ├── blog-chile/                 React
│   ├── nirvana/                    HTML, CSS y JS (se publica tal cual)
│   └── quiz-pokemon/               HTML, CSS y JS + la versión PHP original
│
├── publicados/                     Builds compilados que se sirven dentro del portfolio (no se editan a mano)
│   ├── proyecto-practica/
│   ├── blogchile/
│   └── the-magic-barber/
│
├── scripts/                        Scripts de publicación de proyectos
├── vercel.json                     Cómo compila Vercel la web (desde portfolio/)
└── render.yaml                     Referencia del servicio de la API en Render
```

**Regla general:** el código se edita en `portfolio/` o en `proyectos/`. Lo que hay en `publicados/` sale de compilar un proyecto y se sustituye entero cada vez.

## Cómo se publica cada proyecto

Al compilar el portfolio, Angular copia en la web final, además de `portfolio/public/`:

| URL en la web | Origen en el repositorio |
| --- | --- |
| `/proyecto-practica/` | `publicados/proyecto-practica/` |
| `/blogchile/` | `publicados/blogchile/` |
| `/the-magic-barber/` | `publicados/the-magic-barber/` |
| `/nirvana/` | `proyectos/nirvana/` |
| `/QuizPokemon/QuizPokemon/` | `proyectos/quiz-pokemon/` |

Esta correspondencia está en los `assets` de [`portfolio/angular.json`](portfolio/angular.json). Las URLs son las de siempre, así que los enlaces de la galería no cambian.

| Proyecto | Cómo actualizarlo |
| --- | --- |
| Secura Insurance | `npm run build` en `proyectos/proyecto-practica/frontend/` (compila directamente en `publicados/proyecto-practica/`) |
| Blog Chile | `npm run build` en `proyectos/blog-chile/` y copiar `build/` (sin los `.map`) a `publicados/blogchile/` |
| The Magic Barber | `node scripts/the-magic-barber.mjs` (el código fuente está en otro repositorio) |
| Nirvana Narguile | Editar directamente `proyectos/nirvana/` |
| Quiz Pokémon | Editar directamente `proyectos/quiz-pokemon/` |

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
