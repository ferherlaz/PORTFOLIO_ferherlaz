import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL, aFechaIso } from '../api';
import { ConsultaRenovaciones, Pagina, Renovacion } from '../models';

@Injectable({ providedIn: 'root' })
export class RenovacionesService {
  private readonly http = inject(HttpClient);
  private readonly url = `${inject(API_URL)}/api/renovaciones`;

  /** Filtros, orden y paginación se resuelven en el servidor. */
  listar(consulta: ConsultaRenovaciones): Observable<Pagina<Renovacion>> {
    let params = new HttpParams()
      .set('orden', consulta.orden)
      .set('pagina', consulta.pagina)
      .set('tamano', consulta.tamano);

    if (consulta.q?.trim()) params = params.set('q', consulta.q.trim());
    if (consulta.estado) params = params.set('estado', consulta.estado);
    if (consulta.desde) params = params.set('desde', aFechaIso(consulta.desde));
    if (consulta.hasta) params = params.set('hasta', aFechaIso(consulta.hasta));
    if (consulta.importeMin != null) params = params.set('importeMin', consulta.importeMin);
    if (consulta.importeMax != null) params = params.set('importeMax', consulta.importeMax);

    return this.http.get<Pagina<Renovacion>>(this.url, { params });
  }

  proximas(limite = 3): Observable<Renovacion[]> {
    return this.http.get<Renovacion[]>(`${this.url}/proximas`, { params: { limite } });
  }
}
