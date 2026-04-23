import { Component, inject } from '@angular/core';
import { LanguageService } from '../../../../core/services/language.service';

@Component({
  selector: 'app-skills',
  standalone: true,
  templateUrl: './skills.component.html'
})
export class SkillsComponent {
  public languageService = inject(LanguageService);
}
