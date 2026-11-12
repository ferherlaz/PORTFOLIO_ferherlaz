import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';
import { ConexionService } from './core/services/conexion.service';
import { LanguageService } from './core/services/language.service';
import { FooterComponent } from './layout/footer/footer.component';
import { HeaderComponent } from './layout/header/header.component';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent, FooterComponent, RouterOutlet, TranslatePipe],
  template: `
    <app-header />
    @if (conexion.hayPeticionLenta()) {
      <div class="aviso-servidor" role="status">
        <span class="spinner-border spinner-border-sm" aria-hidden="true"></span>
        {{ 'API.Despertando' | translate }}
      </div>
    }
    <router-outlet />
    <app-footer />
  `,
  styles: `
    .aviso-servidor {
      position: sticky;
      top: 0;
      z-index: 1050;
      display: flex;
      gap: 10px;
      align-items: center;
      max-width: 1440px;
      margin: auto;
      padding: 10px 24px;
      background: #fff3cd;
      color: #664d03;
      font-size: 14px;
    }
  `,
})
export class AppComponent {
  protected readonly conexion = inject(ConexionService);

  constructor() {
    inject(LanguageService).init();
  }
}
