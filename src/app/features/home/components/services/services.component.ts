import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../../core/services/language.service';

@Component({
  selector: 'app-services',
  standalone: true,
  templateUrl: './services.component.html'
})
export class ServicesComponent {
  public languageService = inject(LanguageService);
}
