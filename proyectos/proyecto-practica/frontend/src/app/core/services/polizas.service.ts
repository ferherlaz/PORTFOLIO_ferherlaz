import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../api';
import { Bloque, BloquesPoliza, PolizaDetalle } from '../models';

@Injectable({ providedIn: 'root' })
export class PolizasService {
  private readonly http = inject(HttpClient);
  private readonly url = `${inject(API_URL)}/api/polizas`;

  detalle(id: number): Observable<PolizaDetalle> {
    return this.http.get<PolizaDetalle>(`${this.url}/${id}`);
  }

  /** PUT /api/polizas/{id}/{bloque}: devuelve el bloque tal y como quedó guardado (normalizado). */
  guardar<K extends Bloque>(id: number, bloque: K, datos: BloquesPoliza[K]): Observable<BloquesPoliza[K]> {
    return this.http.put<BloquesPoliza[K]>(`${this.url}/${id}/${bloque}`, datos);
  }
}
