import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'inicio',
    title: 'HEADER.Home',
    loadComponent: () => import('./features/home/home.component').then((m) => m.HomeComponent),
  },
  {
    path: 'renovaciones',
    title: 'HEADER.ProximasRenovaciones',
    loadComponent: () =>
      import('./features/renovations/renovations.component').then((m) => m.RenovationsComponent),
  },
  {
    path: 'renovaciones/detalles/:id',
    title: 'DETAILS.DetallePoliza',
    loadComponent: () => import('./features/details/details.component').then((m) => m.DetailsComponent),
  },
  { path: '', redirectTo: 'inicio', pathMatch: 'full' },
  { path: '**', redirectTo: 'inicio' },
];
