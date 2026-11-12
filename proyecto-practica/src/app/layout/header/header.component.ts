import { DOCUMENT } from '@angular/common';
import { Component, computed, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';
import { LanguageService } from '../../core/services/language.service';

interface BootstrapGlobal {
  Offcanvas: { getInstance(el: Element): { hide(): void } | null };
}

@Component({
  selector: 'app-header',
  imports: [RouterLink, RouterLinkActive, TranslatePipe],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  private readonly document = inject(DOCUMENT);
  protected readonly language = inject(LanguageService);

  protected readonly activeLanguageKey = computed(() =>
    this.language.current() === 'en' ? 'HEADER.Ingles' : 'HEADER.Español',
  );

  /** Cierra el menú lateral (móvil) al navegar. */
  closeMenu(): void {
    const menu = this.document.getElementById('offcanvasNavbar');
    const bootstrap = (this.document.defaultView as (Window & { bootstrap?: BootstrapGlobal }) | null)?.bootstrap;
    if (menu) bootstrap?.Offcanvas.getInstance(menu)?.hide();
  }
}
