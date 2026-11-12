import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { LanguageService } from './core/services/language.service';

describe('AppComponent', () => {
  beforeEach(async () => {
    localStorage.clear();
    await TestBed.configureTestingModule({
      imports: [AppComponent],
    }).compileComponents();
  });

  it('crea la aplicación con todas las secciones', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    for (const id of ['home', 'about', 'services', 'skills', 'gallery', 'contact']) {
      expect(compiled.querySelector('#' + id)).withContext(id).not.toBeNull();
    }
  });

  it('cambia de idioma y actualiza textos y lang del documento', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.greeting')?.textContent).toContain('Welcome');

    TestBed.inject(LanguageService).toggleLanguage();
    fixture.detectChanges();
    TestBed.flushEffects();
    expect(compiled.querySelector('.greeting')?.textContent).toContain('Bienvenido');
    expect(document.documentElement.lang).toBe('es');
  });

  it('recupera el idioma guardado en una visita anterior', () => {
    localStorage.setItem('portfolio-language', 'es');
    const service = TestBed.runInInjectionContext(() => new LanguageService());
    expect(service.isSpanish()).toBeTrue();
  });
});
