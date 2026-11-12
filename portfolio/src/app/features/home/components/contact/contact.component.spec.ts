import { TestBed } from '@angular/core/testing';
import { ContactComponent } from './contact.component';

describe('ContactComponent', () => {
  it('redacta el correo con los datos del formulario', () => {
    localStorage.clear();
    const fixture = TestBed.createComponent(ContactComponent);
    fixture.detectChanges();
    const form = fixture.nativeElement.querySelector('form') as HTMLFormElement;
    const set = (name: string, value: string) => ((form.elements.namedItem(name) as HTMLInputElement).value = value);
    set('firstName', 'Ana');
    set('lastName', 'López');
    set('email', 'ana@example.com');
    set('message', 'Hola, me interesa tu perfil');

    const url = new URL(fixture.componentInstance.mailtoFor(form));
    expect(url.protocol).toBe('mailto:');
    expect(url.pathname).toBe('ferherlaz@gmail.com');
    expect(url.searchParams.get('subject')).toBe('Contact from portfolio - Ana López');
    expect(url.searchParams.get('body')).toBe('Hola, me interesa tu perfil\n\n--\nAna López\nana@example.com');
  });
});
