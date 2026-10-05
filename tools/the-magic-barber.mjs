// Publica The Magic Barber en public/the-magic-barber SIN modificar el proyecto original.
//
// La app necesita Supabase. En el portfolio no hay servidor, así que se usa el mismo backend de
// demostración del proyecto (scripts/demo-supabase.mjs), convertido para ejecutarse en el navegador:
// demo-backend.js intercepta las peticiones a DEMO_URL y responde con esa misma lógica.
//
// Sobre una copia temporal del proyecto solo se cambia:
//   - environment.ts  -> apunta a DEMO_URL
//   - app.config.ts   -> rutas con '#' (la app vive en una subcarpeta de una web estática)
//
// Uso: node tools/the-magic-barber.mjs ["ruta del proyecto"]
import { execSync } from 'node:child_process';
import { cpSync, existsSync, mkdirSync, readFileSync, rmSync, symlinkSync, writeFileSync } from 'node:fs';
import { tmpdir } from 'node:os';
import { join, resolve } from 'node:path';

const SRC = resolve(process.argv[2] ?? 'C:/Users/ferhe/OneDrive/Documentos/Mis Documentos/MIS PROYECTOS/the-magic-barber-portfolio');
const OUT = resolve('public/the-magic-barber');
const WORK = join(tmpdir(), 'the-magic-barber-portfolio-build');
const DEMO_URL = 'https://demo-supabase.invalid';

/** Sustituye exactamente una aparición; si el original cambia, falla en vez de generar algo roto. */
function replaceOnce(text, search, replacement, what) {
  const count = text.split(search).length - 1;
  if (count !== 1) throw new Error(`[the-magic-barber] No se pudo adaptar ${what} (${count} coincidencias)`);
  return text.replace(search, replacement);
}

// 1. Copia de trabajo (node_modules enlazado, no copiado)
rmSync(WORK, { recursive: true, force: true });
mkdirSync(WORK, { recursive: true });
for (const item of ['src', 'public', 'angular.json', 'package.json', 'tsconfig.json', 'tsconfig.app.json']) {
  cpSync(join(SRC, item), join(WORK, item), { recursive: true });
}
symlinkSync(join(SRC, 'node_modules'), join(WORK, 'node_modules'), 'junction');

// 2. Adaptaciones mínimas para servirla como estático
writeFileSync(join(WORK, 'src/environments/environment.ts'), `// Build del portfolio: Supabase simulado en el navegador (demo-backend.js).
export const environment = {
  supabaseUrl: ${JSON.stringify(DEMO_URL)},
  supabaseAnonKey: "demo-anon-key",
};
`);
const configPath = join(WORK, 'src/app/app.config.ts');
let config = readFileSync(configPath, 'utf8');
config = replaceOnce(config, 'withComponentInputBinding, withInMemoryScrolling', 'withComponentInputBinding, withHashLocation, withInMemoryScrolling', 'los imports del router');
config = replaceOnce(config, 'withComponentInputBinding(),', 'withComponentInputBinding(),\n      withHashLocation(),', 'provideRouter');
writeFileSync(configPath, config);

// 3. Build (ng build directamente: el prebuild del proyecto regeneraría environment.ts)
execSync('npx ng build --base-href ./', { cwd: WORK, stdio: 'inherit' });

// 4. Backend de demo para el navegador, generado a partir de scripts/demo-supabase.mjs
const demo = readFileSync(join(SRC, 'scripts/demo-supabase.mjs'), 'utf8');
const between = (start, end) => {
  const from = demo.indexOf(start);
  const to = demo.indexOf(end, from);
  if (from < 0 || to < 0) throw new Error(`[the-magic-barber] demo-supabase.mjs ha cambiado: no encuentro "${start}"`);
  return demo.slice(from, to);
};
let logic = between('const pad =', '// ---------- Servidor ----------');
logic = replaceOnce(logic, "Buffer.from(JSON.stringify(o)).toString('base64url')", 'base64url(JSON.stringify(o))', 'la creación del token');
// En el navegador las sesiones en memoria se pierden al recargar: se reconstruyen desde el token.
logic = replaceOnce(logic, "return sessions.get(token) ?? { id: null, role: 'anon' };", "return sessions.get(token) ?? userFromToken(token) ?? { id: null, role: 'anon' };", 'currentUser');
const handlerBody = between('  try {', '}).listen(');
const handler = replaceOnce(handlerBody, 'const email = sessions.get(body.refresh_token);', "const email = sessions.get(body.refresh_token) ?? Object.keys(users).find((e) => 'refresh-' + users[e].id === body.refresh_token);", 'el refresco de sesión');

const backend = `// Generado por tools/the-magic-barber.mjs a partir de scripts/demo-supabase.mjs del proyecto.
// Imita Supabase (auth + PostgREST) en memoria dentro del navegador. Los datos se reinician al recargar.
(() => {
const DEMO_URL = ${JSON.stringify(DEMO_URL)};
const base64url = (text) => btoa(String.fromCharCode(...new TextEncoder().encode(text))).replace(/\\+/g, '-').replace(/\\//g, '_').replace(/=+$/, '');
const userFromToken = (token) => {
  try {
    const { email } = JSON.parse(atob(token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/')));
    return users[email] ? { ...users[email], email } : null;
  } catch {
    return null;
  }
};

${logic}
async function handle(request) {
  const url = new URL(request.url);
  const req = { method: request.method, headers: { authorization: request.headers.get('authorization') ?? '', accept: request.headers.get('accept') ?? '' } };
  let response;
  const send = (status, body, extra = {}) => {
    const empty = req.method === 'HEAD' || body === undefined || status === 204;
    response = new Response(empty ? null : JSON.stringify(body), { status, headers: { 'content-type': 'application/json', ...extra } });
    return response;
  };
  if (req.method === 'OPTIONS') return send(204);
  const raw = await request.text();
  const body = raw ? JSON.parse(raw) : {};
  const user = currentUser(req);

${handler}
  return response ?? send(404, {});
}

const realFetch = window.fetch.bind(window);
window.fetch = (input, init) => {
  const target = input instanceof Request ? input.url : String(input);
  return target.startsWith(DEMO_URL) ? handle(new Request(input, init)) : realFetch(input, init);
};
})();
`;

// 5. Publicar en el portfolio
rmSync(OUT, { recursive: true, force: true });
cpSync(join(WORK, 'dist/the-magic-barber/browser'), OUT, { recursive: true });
writeFileSync(join(OUT, 'demo-backend.js'), backend);
const indexPath = join(OUT, 'index.html');
writeFileSync(indexPath, replaceOnce(readFileSync(indexPath, 'utf8'), '</head>', '  <script src="demo-backend.js"></script>\n</head>', 'index.html'));

if (!existsSync(join(OUT, 'brand/logo.png'))) throw new Error('[the-magic-barber] Falta brand/logo.png en el build');
console.log(`[the-magic-barber] Publicado en ${OUT}`);
