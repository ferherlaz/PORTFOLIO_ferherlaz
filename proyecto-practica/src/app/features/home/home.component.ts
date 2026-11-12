import { Component } from '@angular/core';
import { ClientsComponent } from './components/clients/clients.component';
import { SeeAllButtonComponent } from './components/see-all-button/see-all-button.component';
import { UpcomingRenovationsComponent } from './components/upcoming-renovations/upcoming-renovations.component';
import { WelcomeComponent } from './components/welcome/welcome.component';

@Component({
  selector: 'app-home',
  imports: [WelcomeComponent, UpcomingRenovationsComponent, SeeAllButtonComponent, ClientsComponent],
  template: `
    <app-welcome />
    <main>
      <app-upcoming-renovations />
      <app-see-all-button />
      <app-clients />
    </main>
  `,
})
export class HomeComponent {}
