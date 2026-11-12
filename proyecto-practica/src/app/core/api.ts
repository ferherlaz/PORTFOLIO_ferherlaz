import { HttpErrorResponse } from '@angular/common/http';
import { InjectionToken } from '@angular/core';
import { environment } from '../../environments/environment';
import { ErroresCampo } from './models';

export const API_URL = new InjectionToken<string>('API_URL', {
  providedIn: 'root',
  factory: () => environment.apiUrl,
});

/** Fecha local a 'yyyy-MM-dd' (lo que espera DateOnly en .NET). */
export function aFechaIso(fecha: Date): string {
  const mm = String(fecha.getMonth() + 1).padStart(2, '0');
  const dd = String(fecha.getDate()).padStart(2, '0');
  return `${fecha.getFullYear()}-${mm}-${dd}`;
}

/** Extrae los errores por campo de una respuesta 400 con ProblemDetails; null si no es de validación. */
export function erroresDeValidacion(error: unknown): ErroresCampo | null {
  if (error instanceof HttpErrorResponse && error.status === 400 && error.error?.errors) {
    return error.error.errors as ErroresCampo;
  }
  return null;
}
