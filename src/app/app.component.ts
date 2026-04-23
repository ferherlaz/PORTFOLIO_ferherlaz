import { Component } from '@angular/core';
import { NavbarComponent } from './layout/navbar/navbar.component';
import { HeroComponent } from './features/home/components/hero/hero.component';
import { AboutComponent } from './features/home/components/about/about.component';
import { ServicesComponent } from './features/home/components/services/services.component';
import { SkillsComponent } from './features/home/components/skills/skills.component';
import { GalleryComponent } from './features/home/components/gallery/gallery.component';
import { ContactComponent } from './features/home/components/contact/contact.component';
import { FooterComponent } from './layout/footer/footer.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    NavbarComponent,
    HeroComponent,
    AboutComponent,
    ServicesComponent,
    SkillsComponent,
    GalleryComponent,
    ContactComponent,
    FooterComponent
  ],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'portfolio';
}
