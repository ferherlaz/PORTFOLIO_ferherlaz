import { Component, inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { LanguageService } from '../../../../core/services/language.service';
import { PROFILE } from '../../../../core/profile';

@Component({
  selector: 'app-contact',
  standalone: true,
  templateUrl: './contact.component.html'
})
export class ContactComponent {
  private readonly document = inject(DOCUMENT);
  protected readonly t = inject(LanguageService).t;
  protected readonly profile = PROFILE;

  /**
   * La web es estática y no tiene backend: el formulario abre el cliente de correo del visitante
   * con el mensaje ya redactado. El navegador valida los campos obligatorios antes de llegar aquí.
   */
  send(event: SubmitEvent): void {
    event.preventDefault();
    this.document.location.href = this.mailtoFor(event.target as HTMLFormElement);
  }

  mailtoFor(form: HTMLFormElement): string {
    const data = new FormData(form);
    const field = (name: string) => String(data.get(name) ?? '').trim();
    const name = `${field('firstName')} ${field('lastName')}`.trim();
    const body = [field('message'), '', '--', name, field('email'), field('phone')].join('\n').trim();
    const subject = `${this.t().contact.mailSubject} - ${name}`;
    return `mailto:${PROFILE.email}?subject=${encodeURIComponent(subject)}&body=${encodeURIComponent(body)}`;
  }
}
