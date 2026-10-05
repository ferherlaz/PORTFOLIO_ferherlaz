import { Component, computed, HostListener, inject, signal } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { LanguageService } from '../../../../core/services/language.service';
import { Language } from '../../../../core/i18n/translations';

interface Project {
  id: number;
  title: string;
  img: string;
  link: string;
  description: Record<Language, string>;
}

@Component({
  selector: 'app-gallery',
  standalone: true,
  templateUrl: './gallery.component.html'
})
export class GalleryComponent {
  private readonly document = inject(DOCUMENT);
  protected readonly language = inject(LanguageService);
  protected readonly t = this.language.t;

  protected readonly projects: Project[] = [
    {
      id: 1,
      title: 'Nirvana Narguile',
      img: 'img/CAP06.png',
      link: 'nirvana/Portada.html',
      description: {
        en: 'Premium landing page for a tea house and hookah lounge, developed in 2024 with a modern, elegant, and interactive design. Implemented primarily with HTML, CSS, and JavaScript, incorporating dynamic elements to enhance the visual experience and user navigation.',
        es: 'Landing page premium para una tetería y hookah lounge, desarrollada en 2024 con un diseño moderno, elegante e interactivo. Implementada principalmente con HTML, CSS y JavaScript, incorporando elementos dinámicos para mejorar la experiencia visual y la navegación del usuario.',
      },
    },
    {
      id: 2,
      title: 'Quiz Pokemon',
      img: 'img/CAP08.png',
      link: 'QuizPokemon/QuizPokemon/index.html',
      description: {
        en: 'Interactive quiz game inspired by the Pokémon universe, originally developed in pure PHP and later adapted with HTML, CSS, and JavaScript for optimal browser visualization and interactive experience. Project completed in 2024.',
        es: 'Juego de preguntas interactivo inspirado en el universo Pokémon, desarrollado originalmente en PHP puro y posteriormente adaptado con HTML, CSS y JavaScript para su visualización y experiencia interactiva en el navegador. Proyecto realizado en 2024.',
      },
    },
    {
      id: 3,
      title: 'Secura Insurance',
      img: 'proyecto-practica/assets/img/hero-cover3.jpg',
      link: 'proyecto-practica/index.html',
      description: {
        en: 'Insurance customer area developed with Angular, Bootstrap and ngx-bootstrap, featuring policy renewals with filters and sorting, policy details (holder, billing, payments, correspondence and contacts), and bilingual support (Spanish/English) with a responsive design.',
        es: 'Área de cliente para una aseguradora desarrollada con Angular, Bootstrap y ngx-bootstrap, con listado de renovaciones de pólizas con filtros y ordenación, detalle de póliza (titular, facturación, pagos, correspondencia y contactos) y soporte bilingüe (español/inglés) con diseño responsive.',
      },
    },
    {
      id: 4,
      title: 'Blog Chile',
      img: 'blogchile/img/LogoChile.jpg',
      link: 'blogchile/index.html',
      description: {
        en: 'Travel blog focused on highlighting the diversity and beauty of Chile, from the Atacama Desert to Patagonia. Developed in 2024 with React, HTML, CSS, and JavaScript, incorporating an interactive design and a navigation organized by regions to offer a more intuitive and engaging user experience.',
        es: 'Blog de viajes enfocado en destacar la diversidad y belleza de Chile, desde el Desierto de Atacama hasta la Patagonia. Desarrollado en 2024 con React, HTML, CSS y JavaScript, incorporando un diseño interactivo y una navegación organizada por regiones para ofrecer una experiencia de usuario más intuitiva y atractiva.',
      },
    },
    {
      id: 5,
      title: 'The Magic Barber',
      img: 'img/the-magic-barber-logo.png',
      link: 'the-magic-barber/index.html',
      description: {
        en: 'Booking and management web app for a barbershop in Getafe (Madrid), developed in 2026 with Angular 19 and Supabase. Clients book appointments by service, barber and time, manage their bookings and order shop products; the admin panel manages the agenda, clients, services, barbers and opening hours. Demo: cliente@demo.es or admin@demo.es, password demo1234.',
        es: 'Aplicación web de reservas y gestión para una barbería de Getafe (Madrid), desarrollada en 2026 con Angular 19 y Supabase. Los clientes reservan cita por servicio, barbero y hora, gestionan sus reservas y encargan productos de la tienda; el panel de administración gestiona agenda, clientes, servicios, barberos y horarios. Demo: cliente@demo.es o admin@demo.es, contraseña demo1234.',
      },
    },
  ];

  protected readonly currentIndex = signal(0);
  protected readonly selectedProject = signal<Project | null>(null);

  /** El título del modal se pinta con la primera palabra en blanco y el resto en el color de acento. */
  protected readonly selectedTitle = computed(() => {
    const [first, ...rest] = this.selectedProject()?.title.split(' ') ?? [];
    return { start: first ? first + ' ' : '', accent: rest.join(' ') };
  });

  next(): void {
    this.currentIndex.update(index => (index + 1) % this.projects.length);
  }

  prev(): void {
    this.currentIndex.update(index => (index - 1 + this.projects.length) % this.projects.length);
  }

  positionClass(index: number): string {
    const total = this.projects.length;
    const diff = (index - this.currentIndex() + total) % total;
    if (diff === 0) return 'center';
    if (diff === 1) return 'right';
    if (diff === total - 1) return 'left';
    return 'hidden';
  }

  openModal(project: Project): void {
    this.selectedProject.set(project);
    this.document.body.style.overflow = 'hidden';
  }

  @HostListener('document:keydown.escape')
  closeModal(): void {
    this.selectedProject.set(null);
    this.document.body.style.overflow = '';
  }

  goToProject(link: string): void {
    window.open(link, '_blank', 'noopener');
  }
}
