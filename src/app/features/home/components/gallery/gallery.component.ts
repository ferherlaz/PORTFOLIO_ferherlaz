import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageService } from '../../../../core/services/language.service';

interface Project {
  id: number;
  img?: string;
  video?: string;
  alt: string;
  link?: string;
  description?: string;
  descriptionEs?: string;
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
    {
      id: 1,
      img: 'img/CAP06.png',
      alt: 'Nirvana Narguile',
      link: 'nirvana/Portada.html',
      description: 'Premium landing page for a tea house and hookah lounge, developed in 2024 with a modern, elegant, and interactive design. Implemented primarily with HTML, CSS, and JavaScript, incorporating dynamic elements to enhance the visual experience and user navigation.',
      descriptionEs: 'Landing page premium para una tetería y hookah lounge, desarrollada en 2024 con un diseño moderno, elegante e interactivo. Implementada principalmente con HTML, CSS y JavaScript, incorporando elementos dinámicos para mejorar la experiencia visual y la navegación del usuario.'
    },
    {
      id: 2,
      img: 'img/CAP08.png',
      alt: 'Quiz Pokemon',
      link: 'QuizPokemon/QuizPokemon/index.html',
      description: 'Interactive quiz game inspired by the Pokémon universe, originally developed in pure PHP and later adapted with HTML, CSS, and JavaScript for optimal browser visualization and interactive experience. Project completed in 2024.',
      descriptionEs: 'Juego de preguntas interactivo inspirado en el universo Pokémon, desarrollado originalmente en PHP puro y posteriormente adaptado con HTML, CSS y JavaScript para su visualización y experiencia interactiva en el navegador. Proyecto realizado en 2024.'
    },
    {
      id: 3,
      video: 'video/ComingSoon.mp4',
      alt: 'Coming Soon',
      description: 'More amazing projects are on the way! Stay tuned for upcoming web applications.',
      descriptionEs: '¡Más proyectos increíbles están en camino! Mantente al tanto de las próximas aplicaciones web.'
    },
    {
      id: 4,
      video: 'video/ComingSoon.mp4',
      alt: 'Coming Soon',
      description: 'More amazing projects are on the way! Stay tuned for upcoming web applications.',
      descriptionEs: '¡Más proyectos increíbles están en camino! Mantente al tanto de las próximas aplicaciones web.'
    },
    {
      id: 5,
      img: 'blogchile/img/LogoChile.jpg',
      alt: 'Blog Chile',
      link: 'blogchile/index.html',
      description: 'Travel blog focused on highlighting the diversity and beauty of Chile, from the Atacama Desert to Patagonia. Developed in 2024 with React, HTML, CSS, and JavaScript, incorporating an interactive design and a navigation organized by regions to offer a more intuitive and engaging user experience.',
      descriptionEs: 'Blog de viajes enfocado en destacar la diversidad y belleza de Chile, desde el Desierto de Atacama hasta la Patagonia. Desarrollado en 2024 con React, HTML, CSS y JavaScript, incorporando un diseño interactivo y una navegación organizada por regiones para ofrecer una experiencia de usuario más intuitiva y atractiva.'
    }
  ];

  public currentIndex = 0;
  public selectedProject: Project | null = null;
  public selectedTitleWhite: string = '';
  public selectedTitleAccent: string = '';

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

  public openModal(project: Project): void {
    if (project.link) {
      this.selectedProject = project;
      const parts = project.alt.split(' ');
      this.selectedTitleWhite = parts[0] ? parts[0] + ' ' : '';
      this.selectedTitleAccent = parts.slice(1).join(' ');
      document.body.style.overflow = 'hidden'; // Prevent scrolling
    }
  }

  public closeModal(): void {
    this.selectedProject = null;
    document.body.style.overflow = 'auto'; // Restore scrolling
  }

  public goToProject(event: MouseEvent, link?: string): void {
    event.stopPropagation();
    if (link) {
      window.open(link, '_blank');
    }
  }
}
