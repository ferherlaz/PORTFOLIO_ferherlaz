/** Contratos de la API .NET (backend/). Las fechas llegan como 'yyyy-MM-dd'. */

export type EstadoPoliza = 'Pendiente' | 'Pagada' | 'Vencido';

export const ESTADOS: readonly EstadoPoliza[] = ['Pagada', 'Vencido', 'Pendiente'];

export interface Renovacion {
  id: number;
  numPoliza: number;
  nombreRiesgo: string;
  fechaContrato: string;
  fechaVencimiento: string;
  importe: number;
  estado: EstadoPoliza;
}

export interface Pagina<T> {
  items: T[];
  total: number;
  pagina: number;
  tamano: number;
  totalPaginas: number;
}

export type Orden = 'importe_desc' | 'importe_asc' | 'vencimiento_asc' | 'vencimiento_desc';

export interface ConsultaRenovaciones {
  q?: string;
  estado?: EstadoPoliza;
  desde?: Date;
  hasta?: Date;
  importeMin?: number | null;
  importeMax?: number | null;
  orden: Orden;
  pagina: number;
  tamano: number;
}

export interface Titular {
  cif: string;
  nombre: string;
  dirFiscal: string;
}

export interface Facturacion {
  dirFacturacion: string;
  numero: string;
  ciudad: string;
  cp: string;
}

export interface Pago {
  nombre: string;
  cif: string;
  tipoCobro: string;
  iban: string;
  banco: string;
}

export interface Correspondencia {
  datos: string;
  poblacion: string;
  cp: string;
  direccion: string;
  numero: string;
  aclarador: string;
}

export interface Contacto {
  correoPrincipal: string;
  correoSecundario: string;
  telefono: string;
}

export interface Cuota {
  id: number;
  fechaEmision: string;
  periodoDesde: string;
  periodoHasta: string;
  importe: number;
  estado: EstadoPoliza;
}

export interface PolizaDetalle {
  renovacion: Renovacion;
  titular: Titular;
  facturacion: Facturacion;
  pago: Pago;
  correspondencia: Correspondencia;
  contacto: Contacto;
  ultimasCuotas: Cuota[];
}

/** Bloques editables del detalle: cada uno se guarda con su propio PUT. */
export interface BloquesPoliza {
  titular: Titular;
  facturacion: Facturacion;
  pago: Pago;
  correspondencia: Correspondencia;
  contacto: Contacto;
}

export type Bloque = keyof BloquesPoliza;

export interface ClientePotencial {
  id: number;
  nombre: string;
  producto: 'Moto' | 'Casa';
  email: string;
  telefono: string;
}

/** Errores de validación por campo (ProblemDetails.errors). */
export type ErroresCampo = Record<string, string[]>;
