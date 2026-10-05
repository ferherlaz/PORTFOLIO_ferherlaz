import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../../core/services/language.service';
import { PROFILE } from '../../../../core/profile';

@Component({
  selector: 'app-hero',
  standalone: true,
  templateUrl: './hero.component.html'
})
export class HeroComponent {
  protected readonly t = inject(LanguageService).t;
  protected readonly cvUrl = PROFILE.cvUrl;
}
