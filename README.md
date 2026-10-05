# Portfolio · Fernando Hernández Lázaro

Portfolio personal hecho con Angular 18 (componentes standalone y signals), bilingüe (inglés / español).

## Comandos

| Comando | Qué hace |
| --- | --- |
| `npm start` | Servidor de desarrollo en `http://localhost:4200/` |
| `npm run build` | Build de producción en `dist/portfolio/browser/` |
| `npm test` | Tests unitarios con Karma |

## Estructura

```
src/
├── app/
│   ├── core/
│   │   ├── i18n/translations.ts   # Todos los textos en inglés y español
│   │   ├── services/              # Idioma activo (se recuerda entre visitas)
│   │   └── profile.ts             # Correo, teléfono, CV y redes
│   ├── layout/                    # Cabecera y pie
│   └── features/home/components/  # Secciones: hero, sobre mí, servicios, habilidades, proyectos, contacto
├── index.html
└── styles.css                     # Estilos globales
public/                            # Se publica tal cual junto a la web
├── img/, curriculum/              # Imágenes y CV del portfolio
└── <proyecto>/                    # Cada proyecto de la galería, ya compilado
tools/                             # Scripts para volver a publicar proyectos en public/
blog-chile/                        # Código fuente (React) del proyecto Blog Chile
```

## Proyectos de la galería

| Proyecto | Carpeta publicada | Cómo actualizarla |
| --- | --- | --- |
| Nirvana Narguile | `public/nirvana/` | HTML, CSS y JS estáticos: se editan directamente |
| Quiz Pokémon | `public/QuizPokemon/` | Estático (`index.html`); los `.php` son la versión original |
| Secura Insurance | `public/proyecto-practica/` | `npm run build:portfolio` en su proyecto y copiar `dist/portfolio/browser` |
| Blog Chile | `public/blogchile/` | `npm run build` en `blog-chile/` y copiar `build/` (sin los `.map`) |
| The Magic Barber | `public/the-magic-barber/` | `node tools/the-magic-barber.mjs` |

## Formulario de contacto

La web es estática: al enviar el formulario se abre el programa de correo del visitante con el mensaje ya redactado para `ferherlaz@gmail.com`.
