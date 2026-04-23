import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  isSpanish = signal<boolean>(false);

  toggleLanguage() {
    this.isSpanish.update(val => !val);
  }
}
