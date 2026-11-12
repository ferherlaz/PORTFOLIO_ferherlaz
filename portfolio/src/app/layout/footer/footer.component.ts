import { Component, inject } from '@angular/core';
import { LanguageService } from '../../core/services/language.service';
import { PROFILE } from '../../core/profile';

@Component({
  selector: 'app-footer',
  standalone: true,
  templateUrl: './footer.component.html'
})
export class FooterComponent {
  protected readonly t = inject(LanguageService).t;
  protected readonly profile = PROFILE;
  protected readonly currentYear = new Date().getFullYear();
}
