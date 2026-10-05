import { computed, effect, inject, Injectable, signal } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Language, TRANSLATIONS } from '../i18n/translations';

const STORAGE_KEY = 'portfolio-language';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private readonly document = inject(DOCUMENT);

  readonly language = signal<Language>(this.storedLanguage());
  readonly isSpanish = computed(() => this.language() === 'es');
  /** Textos del idioma activo: `t().hero.greeting`. */
  readonly t = computed(() => TRANSLATIONS[this.language()]);

  constructor() {
    effect(() => {
      const language = this.language();
      this.document.documentElement.lang = language;
      try {
        localStorage.setItem(STORAGE_KEY, language);
      } catch {
        // Sin almacenamiento (modo privado): el idioma simplemente no se recuerda.
      }
    });
  }

  toggleLanguage(): void {
    this.language.update(language => (language === 'es' ? 'en' : 'es'));
  }

  private storedLanguage(): Language {
    try {
      const stored = localStorage.getItem(STORAGE_KEY);
      if (stored === 'es' || stored === 'en') {
        return stored;
      }
    } catch {
      // Ignorado: se usa el idioma por defecto.
    }
    return 'en';
  }
}
