import { HttpInterceptorFn } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { finalize } from 'rxjs';

/**
 * Cuenta las peticiones en curso para avisar cuando la API tarda (arranque en frío de Render:
 * el plan gratuito duerme el servicio tras 15 min sin tráfico).
 */
@Injectable({ providedIn: 'root' })
export class ConexionService {
  private readonly lentas = signal(0);
  readonly hayPeticionLenta = computed(() => this.lentas() > 0);

  seguir(): () => void {
    let marcada = false;
    const temporizador = setTimeout(() => {
      marcada = true;
      this.lentas.update((n) => n + 1);
    }, 2500);

    return () => {
      clearTimeout(temporizador);
      if (marcada) this.lentas.update((n) => n - 1);
    };
  }
}

export const conexionInterceptor: HttpInterceptorFn = (req, next) => {
  // Las traducciones son ficheros estáticos: no cuentan como llamada a la API.
  if (req.url.includes('assets/')) return next(req);

  const terminar = inject(ConexionService).seguir();
  return next(req).pipe(finalize(terminar));
};
