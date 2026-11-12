import { Component, HostListener, inject } from '@angular/core';
import { LanguageService } from '../../core/services/language.service';
import { PROFILE } from '../../core/profile';
import { Translation } from '../../core/i18n/translations';

interface NavItem {
  section: string;
  label: keyof Translation['nav'];
}

@Component({
  selector: 'app-navbar',
  standalone: true,
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {
  protected readonly language = inject(LanguageService);
  protected readonly t = this.language.t;
  protected readonly cvUrl = PROFILE.cvUrl;

  protected readonly items: NavItem[] = [
    { section: 'home', label: 'home' },
    { section: 'about', label: 'about' },
    { section: 'services', label: 'services' },
    { section: 'skills', label: 'skills' },
    { section: 'gallery', label: 'projects' },
    { section: 'contact', label: 'contact' },
  ];

  isScrolled = false;
  isMenuOpen = false;
  activeSection = 'home';

  @HostListener('window:scroll')
  onWindowScroll(): void {
    this.isScrolled = window.scrollY > 50;
    this.activeSection = this.sectionInView();
  }

  @HostListener('document:keydown.escape')
  closeMenu(): void {
    this.isMenuOpen = false;
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  /** Última sección cuyo inicio ya ha pasado bajo la cabecera (o la última si se llega al final). */
  private sectionInView(): string {
    const atBottom = window.innerHeight + window.scrollY >= document.documentElement.scrollHeight - 2;
    if (atBottom) {
      return this.items[this.items.length - 1].section;
    }
    let current = this.items[0].section;
    for (const { section } of this.items) {
      const element = document.getElementById(section);
      if (element && element.getBoundingClientRect().top <= 150) {
        current = section;
      }
    }
    return current;
  }
}
