import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../api';
import { ClientePotencial } from '../models';

@Injectable({ providedIn: 'root' })
export class ClientesService {
  private readonly http = inject(HttpClient);
  private readonly url = `${inject(API_URL)}/api/clientes-potenciales`;

  listar(): Observable<ClientePotencial[]> {
    return this.http.get<ClientePotencial[]>(this.url);
  }

  borrar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
