import { Component } from '@angular/core';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-welcome',
  imports: [TranslatePipe],
  template: `
    <section class="intro">
      <div class="intro-content">
        <h1>{{ 'WELCOME.Bienvenido' | translate }}</h1>
        <p>{{ 'WELCOME.Texto02' | translate }}</p>
      </div>
    </section>
  `,
  styleUrl: './welcome.component.css',
})
export class WelcomeComponent {}
