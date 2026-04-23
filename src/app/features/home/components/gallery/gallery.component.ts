import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageService } from '../../../../core/services/language.service';

@Component({
  selector: 'app-gallery',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './gallery.component.html'
})
export class GalleryComponent {
  public languageService = inject(LanguageService);

  public projects = [
    { id: 1, img: 'img/CAP01.png', alt: 'Project 1' },
    { id: 2, img: 'img/CAP02.png', alt: 'Project 2' },
    { id: 3, img: 'img/CAP03.png', alt: 'Project 3' },
    { id: 4, img: 'img/CAP04.png', alt: 'Project 4' },
    { id: 5, img: 'img/CAP05.png', alt: 'Project 5' }
  ];

  public currentIndex = 0;

  public next(): void {
    this.currentIndex = (this.currentIndex + 1) % this.projects.length;
  }

  public prev(): void {
    this.currentIndex = (this.currentIndex - 1 + this.projects.length) % this.projects.length;
  }

  public getPositionClass(index: number): string {
    const total = this.projects.length;
    const diff = (index - this.currentIndex + total) % total;

    if (diff === 0) return 'center';
    if (diff === 1) return 'right';
    if (diff === total - 1) return 'left';
    return 'hidden';
  }
}
