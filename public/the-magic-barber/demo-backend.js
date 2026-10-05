// Generado por tools/the-magic-barber.mjs a partir de scripts/demo-supabase.mjs del proyecto.
// Imita Supabase (auth + PostgREST) en memoria dentro del navegador. Los datos se reinician al recargar.
(() => {
const DEMO_URL = "https://demo-supabase.invalid";
const base64url = (text) => btoa(String.fromCharCode(...new TextEncoder().encode(text))).replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
const userFromToken = (token) => {
  try {
    const { email } = JSON.parse(atob(token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/')));
    return users[email] ? { ...users[email], email } : null;
  } catch {
    return null;
  }
};

const pad = (n) => String(n).padStart(2, '0');
const at = (offsetDays, time) => {
  const d = new Date();
  d.setDate(d.getDate() + offsetDays);
  const [h, m] = time.split(':').map(Number);
  d.setHours(h, m, 0, 0);
  return d.toISOString();
};
const uid = () => Math.random().toString(36).slice(2, 10);

// ---------- Datos ----------
const users = {
  'cliente@demo.es': { id: 'u1', role: 'client', full_name: 'Fer Hernández', phone: '600112233' },
  'admin@demo.es': { id: 'u0', role: 'admin', full_name: 'Cristian (Admin)', phone: null },
};
const db = {
  profiles: [
    ...Object.entries(users).map(([email, u]) => ({ id: u.id, email, full_name: u.full_name, phone: u.phone, role: u.role, created_at: '2026-03-02T10:00:00Z' })),
    { id: 'u2', email: 'javi.lopez@correo.es', full_name: 'Javier López', phone: '622334455', role: 'client', created_at: '2026-05-12T10:00:00Z' },
    { id: 'u3', email: 'adrian@correo.es', full_name: 'Adrián Gómez', phone: null, role: 'client', created_at: '2026-08-20T10:00:00Z' },
  ],
  business_settings: [{ id: true, name: 'The Magic Barber', tagline: 'Barbería urbana en Getafe', description: 'Cortes, barbas y rituales de afeitado con estilo propio. Reserva tu cita en segundos y elige a tu barbero.', about_text: 'En The Magic Barber ofrecemos un servicio profesional de barbería y peluquería, dedicado únicamente a la estética masculina. Puedes optar desde un corte tradicional, arreglarte la barba con mucha delicadeza o estar a la última moda con nuestros degradados.\n\nNuestros profesionales cuentan con los mejores productos para el cuidado masculino, ofreciendo tratamientos desrizantes, tintes para barba y pelo o mascarillas para el cuidado de tu piel.', address_line: 'Calle Jiménez e Iglesias, 3', postal_code: '28903', city: 'Getafe, Madrid', phone: null, whatsapp_phone: null, email: null, instagram_url: null, google_maps_url: null, google_review_url: 'https://www.google.com/search?q=the+magic+barber+&sca_esv=3d2f9fa888dd4596&rlz=1C1UEAD_esES1078ES1078&sxsrf=APpeQnvx-sJ8-YlMCrYj1MmXcm4IoUbXsg%3A1790933758936&ei=_nq_aqiuOMmVhbIPtYbXsAE&biw=2563&bih=1329&uact=5&oq=the+magic+barber+&gs_lp=Egxnd3Mtd2l6LXNlcnAiEXRoZSBtYWdpYyBiYXJiZXIgMgQQIxgnMg0QLhjHARivARiOBRgnMgoQIxjwBRjJAhgnMgUQABiABDILEC4YgAQYxwEYrwEyChAAGIAEGIoFGEMyBRAAGIAEMgsQLhiABBjHARivATILEC4YgAQYxwEYrwEyBRAAGIAESNUHUABYAHAAeAGQAQCYAXugAXuqAQMwLjG4AQPIAQD4AQGYAgGgAoABmAMAkgcDMC4xoAevELIHAzAuMbgHgAHCBwMyLTHIBwOACAE&sclient=gws-wiz-serp#sv=CAESzQEKuQEStgEKd0FKaVQ0dElnUXpHWEhpWjZ5NXFhREtLTV8tcFdEU202UnpIZkVETlA3STFSZFRKMFRmM0JlOWZYRm16S2JUV0Nmcy0tbDBwNWdFNkZRRm1PWjcwdW8waTg5bG1Mc0g1YVVpRm00cUhiVkI5bUhBM3NLVlY3eE9ZEhdDbnVfYXNPdUVxMkprZFVQbS1qYm1RMBoiQURzcjlmVEV6a3hSUEx5SXFvTXBpc1l0R1dWVnB6a1dmQRIEODA1MRoBMyoAMAA4AUAAGAAgpurzbUoCEAE', timezone: 'Europe/Madrid', slot_interval_minutes: 15, booking_window_days: 60, min_notice_minutes: 30, cancellation_notice_hours: 2, max_active_bookings_per_client: 3, auto_confirm_bookings: true }],
  services: [
    ['Corte MAGIC', 14, 30, 'Nuestro corte de la casa: asesoramiento, corte a máquina y tijera, lavado y peinado.'],
    ['Corte MAGIC + Arreglo barba sencillo', 21, 30, 'Corte MAGIC con perfilado y arreglo de barba.'],
    ['Corte MAGIC + cejas a navaja', 16, 30, 'Corte MAGIC con definición de cejas a navaja.'],
    ['Corte MAGIC + barba sencilla + cejas a navaja', 23, 30, 'El pack completo: corte, arreglo de barba y cejas a navaja.'],
    ['Corte MAGIC + ritual de afeitado', 24, 45, 'Corte MAGIC con afeitado clásico: toalla caliente, navaja y cuidado de la piel.'],
    ['Cortes a tijera: melenas, modcut, old money, etc.', 17, 45, 'Cortes trabajados a tijera para pelo medio y largo.'],
    ['Sesión visagismo + cambio de look extremo', 20, 45, 'Estudiamos tu rostro y estilo para proponerte un cambio de look a medida.'],
    ['Corte MAGIC + Black Mask', 17, 30, 'Corte MAGIC con mascarilla negra de limpieza facial.'],
    ['Afeitado de cabeza completo', 7, 15, 'Afeitado completo de cabeza a navaja.'],
    ['Solo barba', 10, 30, 'Arreglo y perfilado de barba.'],
    ['Arreglo de contornos, cerquillos y cuello', 9, 15, 'Repaso rápido de contornos, cerquillos y cuello entre cortes.'],
  ].map(([name, price, duration_minutes, description], i) => ({ id: 's' + i, name, price, duration_minutes, description, image_url: null, is_active: true, is_featured: [0, 1, 3].includes(i), sort_order: (i + 1) * 10 })),
  barbers: [
    ['Cristian', 'Especialista en degradados y diseños.', ['Degradados', 'Diseños'], 'ES'],
    ['Brayan', 'Especialista en diseños y barbas.', ['Diseños', 'Barbas'], 'VE'],
    ['Jorge', 'Especialista en tintes y barbas.', ['Tintes', 'Barbas'], 'CO'],
    ['Alejandro', 'Especialista en cambios de look y diseños.', ['Cambios de look', 'Diseños'], 'VE'],
  ].map(([full_name, bio, specialties, country_code], i) => ({ id: 'b' + i, full_name, bio, photo_url: `img/equipo/${full_name.toLowerCase()}.webp`, country_code, specialties, email: null, phone: null, is_active: true, sort_order: i * 10 })),
  working_hours: [],
  reviews: [
    // Reseña real (aportada por el negocio, igual que en supabase/migrations/20261001120100_seed_reviews.sql)
    {
      id: 'r0',
      author_name: 'Fernando Hernández Lázaro',
      rating: 5,
      body:
        'Tengo clarísimo que es el mejor sitio de todo Madrid para un buen corte de pelo y arreglo de barba.\n' +
        'Se nota la dedicación, el esfuerzo y el amor al trabajo de Kezmi en cada uno de sus cortes. Un sitio más que recomendado y del que, sin lugar a dudas, nunca te arrepentirás de ir y sobretodo, de volver.\n' +
        'Mis felicitaciones y agradecimientos 😊',
      review_date: null,
      source: 'google',
      is_published: true,
      sort_order: 0,
    },
    // Más reseñas reales (igual que supabase/migrations/20261002120300_seed_more_reviews.sql)
    { id: 'r1', author_name: 'Cesar', rating: 5, body: 'Los mejores, se lo recomiendo a todo el mundo. Te aconsejan que es lo mejor segun tu estructura y forma, aciertan siempre y dan un servicio excelente. Si tienes dudas, acude a ellos, siempre saben que solucion ofrecerte', review_date: null, source: 'google', is_published: true, sort_order: 20 },
    { id: 'r2', author_name: 'toni yeray', rating: 5, body: 'El mejor barbero de madrid, (probablemente de españa), gran atencion de kezmi, buen material y buenas ideas en los cortes, siempre una buen tipo en el que confiar', review_date: null, source: 'google', is_published: true, sort_order: 30 },
  ],
  time_off: [],
  bookings: [],
  // Productos reales (igual que supabase/migrations/20261002120200_seed_products.sql)
  products: [
    ['Peines Texturizadores', 10, 'peine-y-pick-peinado', 'Peines de calidad para darle textura y estilo a tu pelo.'],
    ['Sérum para barbas', 16, 'ossion-beard-care-serum', 'Sérum para el cuidado e hidratación de tu barba.'],
    ['After Shave', 15, 'bandido-colonias', 'Ideal para calmar irritaciones de la piel post-afeitado.'],
    ['Pomada fijadora', 18, 'suavecito-pomade-firme-hold', 'Perfecto para moldear tu peinado y proporcionarle un brillo saludable y vibrante.'],
    ['Agua para peinar', 12.5, 'ossion-sea-salt-spray', 'Idóneo para humedecer, disciplinar y moldear el cabello de forma ligera.'],
    ['Polvos fijadores', 10.5, 'bandido-extra-volume', 'Excelente para aportar volumen instantáneo, textura y una fijación flexible con acabado mate.'],
  ].map(([name, price, image, description], i) => ({ id: 'p' + i, name, description, price, image_url: `img/productos/${image}.webp`, is_active: true, sort_order: (i + 1) * 10 })),
  orders: [],
  order_items: [],
};
for (const b of db.barbers) for (let w = 1; w <= 6; w++) for (const [s, e] of [['10:00:00', '14:00:00'], ['16:00:00', '20:00:00']]) db.working_hours.push({ id: uid(), barber_id: b.id, weekday: w, start_time: s, end_time: e });
db.time_off.push({ id: uid(), barber_id: null, kind: 'holiday', starts_at: at(12, '00:00'), ends_at: at(13, '00:00'), reason: 'Fiesta local', created_at: at(0, '09:00') });
const addBooking = (offset, time, status, s, b, client = 'u1', guest = null) => {
  const service = db.services[s];
  const starts = at(offset, time);
  db.bookings.push({ id: uid(), client_id: client, guest_name: guest, guest_phone: guest ? '611223344' : null, barber_id: 'b' + b, service_id: service.id, starts_at: starts, ends_at: new Date(new Date(starts).getTime() + service.duration_minutes * 60000).toISOString(), status, price: service.price, duration_minutes: service.duration_minutes, notes: null, cancelled_at: null, created_by: client, created_at: at(-3, '10:00') });
};
addBooking(0, '10:00', 'confirmed', 0, 0, 'u2');
addBooking(0, '11:00', 'pending', 1, 1, null, 'Luis (teléfono)');
addBooking(0, '17:00', 'confirmed', 2, 2, 'u2');
addBooking(2, '18:00', 'confirmed', 1, 1);
addBooking(5, '10:30', 'pending', 0, 3, 'u3');
addBooking(-6, '17:00', 'completed', 0, 0);
addBooking(-20, '12:00', 'cancelled', 2, 1);

// ---------- Lógica equivalente a las funciones SQL ----------
const ACTIVE = ['pending', 'confirmed', 'completed'];
const overlaps = (a1, a2, b1, b2) => new Date(a1) < new Date(b2) && new Date(b1) < new Date(a2);
function isFree(barberId, starts, ends, ignoreId) {
  return !db.bookings.some((b) => b.barber_id === barberId && ACTIVE.includes(b.status) && b.id !== ignoreId && overlaps(b.starts_at, b.ends_at, starts, ends))
    && !db.time_off.some((t) => (!t.barber_id || t.barber_id === barberId) && overlaps(t.starts_at, t.ends_at, starts, ends));
}
function slots({ p_service_id, p_date, p_barber_id, p_ignore_booking_id }) {
  const service = db.services.find((s) => s.id === p_service_id && s.is_active);
  const settings = db.business_settings[0];
  if (!service) return [];
  const [y, m, d] = p_date.split('-').map(Number);
  const weekday = ((new Date(y, m - 1, d).getDay() + 6) % 7) + 1;
  const minStart = Date.now() + settings.min_notice_minutes * 60000;
  const out = [];
  for (const barber of db.barbers.filter((b) => b.is_active && (!p_barber_id || b.id === p_barber_id))) {
    for (const wh of db.working_hours.filter((w) => w.barber_id === barber.id && w.weekday === weekday)) {
      const [sh, sm] = wh.start_time.split(':').map(Number);
      const [eh, em] = wh.end_time.split(':').map(Number);
      const end = new Date(y, m - 1, d, eh, em).getTime();
      for (let t = new Date(y, m - 1, d, sh, sm).getTime(); t + service.duration_minutes * 60000 <= end; t += settings.slot_interval_minutes * 60000) {
        const starts = new Date(t).toISOString();
        const ends = new Date(t + service.duration_minutes * 60000).toISOString();
        if (t >= minStart && isFree(barber.id, starts, ends, p_ignore_booking_id)) out.push({ barber_id: barber.id, starts_at: starts, ends_at: ends });
      }
    }
  }
  return out.sort((a, b) => a.starts_at.localeCompare(b.starts_at));
}
const localDate = (iso) => { const d = new Date(iso); return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`; };
class BizError extends Error {}
function pickSlot(serviceId, startsAt, barberId, ignoreId) {
  const available = slots({ p_service_id: serviceId, p_date: localDate(startsAt), p_barber_id: barberId, p_ignore_booking_id: ignoreId })
    .filter((s) => new Date(s.starts_at).getTime() === new Date(startsAt).getTime());
  if (!available.length) throw new BizError('SLOT_UNAVAILABLE');
  return available[0].barber_id;
}
const rpc = {
  get_opening_hours: () => db.working_hours.filter((w) => db.barbers.find((b) => b.id === w.barber_id)?.is_active),
  get_available_slots: slots,
  create_booking: ({ p_service_id, p_starts_at, p_barber_id, p_notes }, user) => {
    const service = db.services.find((s) => s.id === p_service_id);
    const active = db.bookings.filter((b) => b.client_id === user.id && ['pending', 'confirmed'].includes(b.status) && new Date(b.starts_at) > new Date());
    if (active.length >= db.business_settings[0].max_active_bookings_per_client) throw new BizError('TOO_MANY_ACTIVE_BOOKINGS');
    const barberId = pickSlot(p_service_id, p_starts_at, p_barber_id);
    const booking = { id: uid(), client_id: user.id, guest_name: null, guest_phone: null, barber_id: barberId, service_id: p_service_id, starts_at: new Date(p_starts_at).toISOString(), ends_at: new Date(new Date(p_starts_at).getTime() + service.duration_minutes * 60000).toISOString(), status: db.business_settings[0].auto_confirm_bookings ? 'confirmed' : 'pending', price: service.price, duration_minutes: service.duration_minutes, notes: p_notes, cancelled_at: null, created_by: user.id, created_at: new Date().toISOString() };
    db.bookings.push(booking);
    return booking;
  },
  cancel_booking: ({ p_booking_id }, user) => {
    const b = db.bookings.find((x) => x.id === p_booking_id && (x.client_id === user.id || user.role === 'admin'));
    if (!b) throw new BizError('BOOKING_NOT_FOUND');
    b.status = 'cancelled';
    return b;
  },
  reschedule_booking: ({ p_booking_id, p_starts_at, p_barber_id }, user) => {
    const b = db.bookings.find((x) => x.id === p_booking_id && x.client_id === user.id);
    if (!b) throw new BizError('BOOKING_NOT_FOUND');
    b.barber_id = pickSlot(b.service_id, p_starts_at, p_barber_id, b.id);
    b.starts_at = new Date(p_starts_at).toISOString();
    b.ends_at = new Date(new Date(p_starts_at).getTime() + b.duration_minutes * 60000).toISOString();
    return b;
  },
  create_order: ({ p_items, p_notes }, user) => {
    if (!user.id) throw new BizError('NOT_AUTHENTICATED');
    if (!Array.isArray(p_items) || !p_items.length) throw new BizError('EMPTY_CART');
    const quantities = new Map();
    for (const item of p_items) quantities.set(item.product_id, (quantities.get(item.product_id) ?? 0) + Number(item.quantity));
    const lines = [...quantities].map(([id, quantity]) => ({ product: db.products.find((p) => p.id === id && p.is_active), quantity }));
    if (lines.some((l) => !l.product || l.quantity < 1 || l.quantity > 99)) throw new BizError('PRODUCT_UNAVAILABLE');
    const order = { id: uid() + uid(), client_id: user.id, status: 'pending', total: lines.reduce((sum, l) => sum + l.product.price * l.quantity, 0), notes: p_notes || null, created_at: new Date().toISOString() };
    db.orders.push(order);
    for (const l of lines) db.order_items.push({ id: uid(), order_id: order.id, product_id: l.product.id, product_name: l.product.name, unit_price: l.product.price, quantity: l.quantity });
    return order;
  },
  cancel_order: ({ p_order_id }, user) => {
    const order = db.orders.find((o) => o.id === p_order_id && (o.client_id === user.id || user.role === 'admin'));
    if (!order) throw new BizError('ORDER_NOT_FOUND');
    if (order.status !== 'pending') throw new BizError('ORDER_NOT_CANCELLABLE');
    order.status = 'cancelled';
    return order;
  },
  replace_working_hours: ({ p_barber_id, p_ranges }) => {
    db.working_hours = db.working_hours.filter((w) => w.barber_id !== p_barber_id).concat(p_ranges.map((r) => ({ id: uid(), barber_id: p_barber_id, weekday: r.weekday, start_time: r.start_time + ':00', end_time: r.end_time + ':00' })));
    return null;
  },
};

// ---------- Vistas y relaciones ----------
function clientOverview() {
  return db.profiles.filter((p) => p.role === 'client').map((p) => {
    const mine = db.bookings.filter((b) => b.client_id === p.id);
    const past = mine.filter((b) => b.status !== 'cancelled' && new Date(b.starts_at) <= new Date()).map((b) => b.starts_at).sort();
    const next = mine.filter((b) => ['pending', 'confirmed'].includes(b.status) && new Date(b.starts_at) > new Date()).map((b) => b.starts_at).sort();
    return { ...p, bookings_count: mine.filter((b) => b.status !== 'cancelled').length, last_booking_at: past.at(-1) ?? null, next_booking_at: next[0] ?? null };
  });
}
function withDetails(b) {
  const s = db.services.find((x) => x.id === b.service_id);
  const br = db.barbers.find((x) => x.id === b.barber_id);
  const c = db.profiles.find((x) => x.id === b.client_id);
  return { ...b, service: s && { id: s.id, name: s.name }, barber: br && { id: br.id, full_name: br.full_name, photo_url: br.photo_url }, client: c ? { id: c.id, full_name: c.full_name, email: c.email, phone: c.phone } : null };
}
function tableRows(table, user) {
  if (table === 'client_overview') return user.role === 'admin' ? clientOverview() : [];
  if (table === 'bookings') return db.bookings.filter((b) => user.role === 'admin' || b.client_id === user.id).map(withDetails);
  if (table === 'orders') {
    return db.orders
      .filter((o) => user.role === 'admin' || o.client_id === user.id)
      .map((o) => {
        const c = db.profiles.find((p) => p.id === o.client_id);
        return { ...o, items: db.order_items.filter((i) => i.order_id === o.id), client: c ? { id: c.id, full_name: c.full_name, email: c.email, phone: c.phone } : null };
      });
  }
  if (table === 'reviews') return db.reviews.filter((r) => user.role === 'admin' || r.is_published);
  if (table === 'profiles') return db.profiles.filter((p) => user.role === 'admin' || p.id === user.id);
  return db[table] ?? [];
}
function applyFilters(rows, params) {
  for (const [key, raw] of params) {
    if (['select', 'order', 'limit', 'offset', 'or'].includes(key)) continue;
    const dot = raw.indexOf('.');
    const op = raw.slice(0, dot);
    const value = raw.slice(dot + 1);
    rows = rows.filter((row) => {
      const field = row[key];
      if (op === 'eq') return String(field) === value;
      if (op === 'in') return value.slice(1, -1).split(',').includes(String(field));
      if (op === 'gte') return new Date(field) >= new Date(value);
      if (op === 'lt') return new Date(field) < new Date(value);
      return true;
    });
  }
  const or = params.get('or');
  if (or) {
    const term = or.match(/ilike\.%(.*?)%/)?.[1]?.toLowerCase() ?? '';
    rows = rows.filter((r) => [r.full_name, r.email, r.phone].some((v) => v?.toLowerCase().includes(term)));
  }
  const order = params.get('order');
  if (order) {
    const [field, dir] = order.split(',')[0].split('.');
    rows = [...rows].sort((a, b) => String(a[field]).localeCompare(String(b[field]), 'es', { numeric: true }) * (dir === 'desc' ? -1 : 1));
  }
  const limit = Number(params.get('limit'));
  return limit ? rows.slice(0, limit) : rows;
}

// ---------- Auth ----------
const sessions = new Map();
function makeSession(email) {
  const u = users[email];
  const exp = Math.floor(Date.now() / 1000) + 3600 * 12;
  const b64 = (o) => base64url(JSON.stringify(o));
  const token = `${b64({ alg: 'HS256', typ: 'JWT' })}.${b64({ sub: u.id, email, exp, role: 'authenticated' })}.demo`;
  const session = { access_token: token, refresh_token: 'refresh-' + u.id, token_type: 'bearer', expires_in: 43200, expires_at: exp, user: { id: u.id, email, aud: 'authenticated', role: 'authenticated', app_metadata: {}, user_metadata: {}, created_at: '2026-03-02T10:00:00Z' } };
  sessions.set(token, { ...u, email });
  sessions.set(session.refresh_token, email);
  return session;
}
function currentUser(req) {
  const token = (req.headers.authorization ?? '').replace('Bearer ', '');
  return sessions.get(token) ?? userFromToken(token) ?? { id: null, role: 'anon' };
}


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

  try {
    // Auth
    if (url.pathname === '/auth/v1/token') {
      if (url.searchParams.get('grant_type') === 'refresh_token') {
        const email = sessions.get(body.refresh_token) ?? Object.keys(users).find((e) => 'refresh-' + users[e].id === body.refresh_token);
        return email ? send(200, makeSession(email)) : send(400, { error: 'invalid_grant', error_description: 'Invalid Refresh Token' });
      }
      if (!users[body.email] || body.password !== 'demo1234') return send(400, { error: 'invalid_grant', error_description: 'Invalid login credentials', msg: 'Invalid login credentials', code: 'invalid_credentials' });
      return send(200, makeSession(body.email));
    }
    if (url.pathname === '/auth/v1/user') return user.id ? send(200, { id: user.id, email: user.email, aud: 'authenticated', role: 'authenticated' }) : send(401, { msg: 'no session' });
    if (url.pathname === '/auth/v1/logout') return send(204);
    if (url.pathname.startsWith('/auth/v1/')) return send(400, { msg: 'En la demo solo se puede entrar con las cuentas de prueba.' });
    if (url.pathname.startsWith('/storage/v1/')) return send(400, { message: 'La subida de imágenes necesita Supabase real.' });

    // RPC
    const rpcMatch = url.pathname.match(/^\/rest\/v1\/rpc\/(\w+)$/);
    if (rpcMatch) {
      const fn = rpc[rpcMatch[1]];
      if (!fn) return send(404, { message: 'rpc no simulada' });
      const result = fn(body, user);
      return send(200, result && !Array.isArray(result) && rpcMatch[1] !== 'replace_working_hours' ? result : result);
    }

    // Tablas
    const table = url.pathname.replace('/rest/v1/', '');
    const single = (req.headers.accept ?? '').includes('vnd.pgrst.object');
    if (req.method === 'GET' || req.method === 'HEAD') {
      const rows = applyFilters(tableRows(table, user), url.searchParams);
      return send(200, single ? (rows[0] ?? null) : rows, { 'content-range': `0-${rows.length}/${rows.length}` });
    }
    if (user.role !== 'admin' && !(table === 'profiles' && req.method === 'PATCH')) return send(403, { code: '42501', message: 'permission denied' });
    if (req.method === 'POST') {
      const row = { id: uid(), created_at: new Date().toISOString(), ...body };
      if (table === 'bookings' && ['pending', 'confirmed', 'completed'].includes(row.status) && !isFree(row.barber_id, row.starts_at, row.ends_at)) {
        return send(409, { code: '23P01', message: 'conflicting key value violates exclusion constraint "bookings_no_overlap"' });
      }
      db[table].push(row);
      return send(201, single ? row : [row]);
    }
    const target = applyFilters(db[table] ?? [], url.searchParams);
    if (req.method === 'PATCH') {
      for (const row of target) {
        if (table === 'bookings' && body.starts_at && ACTIVE.includes(body.status ?? row.status) && !isFree(body.barber_id ?? row.barber_id, body.starts_at, body.ends_at, row.id)) {
          return send(409, { code: '23P01', message: 'conflicting key value violates exclusion constraint' });
        }
        Object.assign(row, body);
      }
      const out = table === 'bookings' ? target.map(withDetails) : target;
      return send(200, single ? (out[0] ?? null) : out);
    }
    if (req.method === 'DELETE') {
      if (table === 'services' && target.some((s) => db.bookings.some((b) => b.service_id === s.id))) return send(409, { code: '23503', message: 'foreign key violation' });
      if (table === 'products' && target.some((p) => db.order_items.some((i) => i.product_id === p.id))) return send(409, { code: '23001', message: 'restrict violation' });
      db[table] = db[table].filter((row) => !target.includes(row));
      return send(204);
    }
    send(404, {});
  } catch (error) {
    if (error instanceof BizError) return send(400, { code: 'P0001', message: error.message });
    console.error(error);
    send(500, { message: String(error) });
  }

  return response ?? send(404, {});
}

const realFetch = window.fetch.bind(window);
window.fetch = (input, init) => {
  const target = input instanceof Request ? input.url : String(input);
  return target.startsWith(DEMO_URL) ? handle(new Request(input, init)) : realFetch(input, init);
};
})();
