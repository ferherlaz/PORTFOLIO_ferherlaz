import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../../core/services/language.service';

@Component({
  selector: 'app-contact',
  standalone: true,
  templateUrl: './contact.component.html'
})
export class ContactComponent {
  public languageService = inject(LanguageService);
}
