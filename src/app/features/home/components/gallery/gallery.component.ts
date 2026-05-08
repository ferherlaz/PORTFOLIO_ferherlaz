import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageService } from '../../../../core/services/language.service';

interface Project {
  id: number;
  img?: string;
  video?: string;
  alt: string;
  link?: string;
}

@Component({
  selector: 'app-gallery',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './gallery.component.html'
})
export class GalleryComponent {
  public languageService = inject(LanguageService);

  public projects: Project[] = [
    { id: 1, img: 'img/CAP06.png', alt: 'Nirvana Narguile', link: 'nirvana/Portada.html' },
    { id: 2, img: 'img/CAP07.png', alt: 'Quiz Pokemon', link: 'QuizPokemon/QuizPokemon/index.php' },
    { id: 3, video: 'video/ComingSoon.mp4', alt: 'Coming Soon' },
    { id: 4, video: 'video/ComingSoon.mp4', alt: 'Coming Soon' },
    { id: 5, video: 'video/ComingSoon.mp4', alt: 'Coming Soon' }
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
