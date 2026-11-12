import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-see-all-button',
  imports: [RouterLink, TranslatePipe],
  template: `
    <section class="ver-todas">
      <button class="see-all" type="button" routerLink="/renovaciones">{{ 'BUTTON.VerTodas' | translate }}</button>
    </section>
  `,
  styleUrl: './see-all-button.component.css',
})
export class SeeAllButtonComponent {}
