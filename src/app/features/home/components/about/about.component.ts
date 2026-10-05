import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../../core/services/language.service';
import { PROFILE } from '../../../../core/profile';

@Component({
  selector: 'app-about',
  standalone: true,
  templateUrl: './about.component.html'
})
export class AboutComponent {
  protected readonly t = inject(LanguageService).t;
  protected readonly cvUrl = PROFILE.cvUrl;
}
