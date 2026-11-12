import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';
import { Renovacion } from '../../../../core/models';
import { RenovacionesService } from '../../../../core/services/renovaciones.service';
import { EstadoBadgeComponent } from '../../../../shared/estado-badge.component';

/** Próximas renovaciones reales: GET /api/renovaciones/proximas. */
@Component({
  selector: 'app-upcoming-renovations',
  imports: [TranslatePipe, DatePipe, CurrencyPipe, EstadoBadgeComponent],
  templateUrl: './upcoming-renovations.component.html',
  styleUrl: './upcoming-renovations.component.css',
})
export class UpcomingRenovationsComponent implements OnInit {
  private readonly service = inject(RenovacionesService);
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly renovaciones = signal<Renovacion[] | null>(null);
  protected readonly error = signal(false);

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.error.set(false);
    this.service
      .proximas(3)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (r) => this.renovaciones.set(r),
        error: () => this.error.set(true),
      });
  }

  verDetalle(id: number): void {
    this.router.navigate(['renovaciones', 'detalles', id]);
  }
}
