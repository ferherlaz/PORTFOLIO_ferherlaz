import { isPlatformBrowser } from '@angular/common';
import { inject, Injectable, PLATFORM_ID, signal } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

const IDIOMAS = ['es', 'en'] as const;
type Idioma = (typeof IDIOMAS)[number];
const POR_DEFECTO: Idioma = 'es';
const CLAVE = 'language';

@Injectable({ providedIn: 'root' })
export class LanguageService {
  private readonly translate = inject(TranslateService);
  private readonly bsLocale = inject(BsLocaleService);
  private readonly isBrowser = isPlatformBrowser(inject(PLATFORM_ID));

  readonly current = signal<Idioma>(POR_DEFECTO);

  init(): void {
    this.translate.addLangs([...IDIOMAS]);
    const guardado = this.isBrowser ? this.leer() : null;
    const candidato = guardado ?? this.translate.getBrowserLang() ?? POR_DEFECTO;
    this.apply(this.isSupported(candidato) ? candidato : POR_DEFECTO);
  }

  use(idioma: Idioma): void {
    this.apply(idioma);
    if (this.isBrowser) {
      try {
        localStorage.setItem(CLAVE, idioma);
      } catch {
        /* almacenamiento no disponible: no pasa nada */
      }
    }
  }

  private apply(idioma: Idioma): void {
    this.translate.use(idioma);
    this.bsLocale.use(idioma);
    this.current.set(idioma);
  }

  private leer(): string | null {
    try {
      return localStorage.getItem(CLAVE);
    } catch {
      return null;
    }
  }

  private isSupported(idioma: string): idioma is Idioma {
    return (IDIOMAS as readonly string[]).includes(idioma);
  }
}
