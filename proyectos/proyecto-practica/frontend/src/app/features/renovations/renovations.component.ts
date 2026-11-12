import { CurrencyPipe, DatePipe } from '@angular/common';
import { Component, computed, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';
import { BsDatepickerConfig, BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { catchError, of, Subject, switchMap, tap } from 'rxjs';
import { ConsultaRenovaciones, ESTADOS, EstadoPoliza, Orden, Pagina, Renovacion } from '../../core/models';
import { RenovacionesService } from '../../core/services/renovaciones.service';
import { EstadoBadgeComponent } from '../../shared/estado-badge.component';
import { OpcionSelect, SelectComponent } from '../../shared/select.component';

interface Filtros {
  q?: string;
  estado?: EstadoPoliza;
  desde?: Date;
  hasta?: Date;
  importeMin?: number | null;
  importeMax?: number | null;
}

type ClaveFiltro = keyof Filtros;

/**
 * Listado de renovaciones. Filtros, orden y paginación los resuelve la API
 * (GET /api/renovaciones): solo viaja la página que se muestra.
 */
@Component({
  selector: 'app-renovations',
  imports: [FormsModule, RouterLink, TranslatePipe, DatePipe, CurrencyPipe, BsDatepickerModule, EstadoBadgeComponent, SelectComponent],
  templateUrl: './renovations.component.html',
  styleUrl: './renovations.component.css',
})
export class RenovationsComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly service = inject(RenovacionesService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly consultas = new Subject<ConsultaRenovaciones>();

  protected readonly bsConfig: Partial<BsDatepickerConfig> = {
    containerClass: 'theme-blue',
    dateInputFormat: 'DD/MM/YYYY',
    isAnimated: false,
    showWeekNumbers: false,
  };
  protected readonly opcionesEstado: OpcionSelect<EstadoPoliza | undefined>[] = [
    { valor: undefined, etiqueta: 'RENOVATIONS.TodosLosEstados' },
    ...ESTADOS.map((estado) => ({ valor: estado, etiqueta: `UNCOMING_RENOVATIONS.${estado}` })),
  ];
  protected readonly opcionesOrden: OpcionSelect<Orden>[] = [
    { valor: 'importe_desc', etiqueta: 'RENOVATIONS.MayorImporte' },
    { valor: 'importe_asc', etiqueta: 'RENOVATIONS.MenorImporte' },
    { valor: 'vencimiento_asc', etiqueta: 'RENOVATIONS.VencimientoProximo' },
    { valor: 'vencimiento_desc', etiqueta: 'RENOVATIONS.VencimientoLejano' },
  ];
  protected readonly opcionesTamano: OpcionSelect<number>[] = [5, 10, 15, 20].map((t) => ({
    valor: t,
    etiqueta: String(t),
  }));

  /** Borrador del formulario lateral; solo se aplica al pulsar "Aplicar filtros". */
  protected borrador: Filtros = {};
  protected readonly filtros = signal<Filtros>({});
  protected orden: Orden = 'importe_desc';
  protected tamano = 10;
  protected readonly pagina = signal(1);

  protected readonly resultado = signal<Pagina<Renovacion> | null>(null);
  protected readonly cargando = signal(true);
  protected readonly error = signal(false);
  /** Total sin filtros, para la cabecera "N Pólizas". */
  protected readonly totalPolizas = signal<number | null>(null);

  protected readonly filtrosAplicados = computed(
    () => Object.values(this.filtros()).filter((v) => v != null && v !== '').length,
  );

  ngOnInit(): void {
    this.consultas
      .pipe(
        tap(() => {
          this.cargando.set(true);
          this.error.set(false);
        }),
        // Si el usuario cambia de página o filtro antes de que llegue la respuesta, se descarta la anterior.
        switchMap((consulta) =>
          this.service.listar(consulta).pipe(
            catchError(() => {
              this.error.set(true);
              return of(null);
            }),
          ),
        ),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe((pagina) => {
        this.cargando.set(false);
        if (!pagina) return;
        this.resultado.set(pagina);
        if (this.filtrosAplicados() === 0) this.totalPolizas.set(pagina.total);
      });

    this.cargar();
  }

  cargar(): void {
    this.consultas.next({
      ...this.filtros(),
      orden: this.orden,
      pagina: this.pagina(),
      tamano: this.tamano,
    });
  }

  aplicarFiltros(): void {
    this.filtros.set({ ...this.borrador });
    this.irAPagina(1);
  }

  borrarFiltros(): void {
    this.borrador = {};
    this.aplicarFiltros();
  }

  borrarFiltro(clave: ClaveFiltro): void {
    this.borrador = { ...this.filtros(), [clave]: undefined };
    this.aplicarFiltros();
  }

  cambiarOrden(): void {
    this.irAPagina(1);
  }

  cambiarTamanoPagina(): void {
    this.irAPagina(1);
  }

  irAPagina(pagina: number): void {
    const total = this.resultado()?.totalPaginas ?? 1;
    this.pagina.set(Math.max(1, pagina === Infinity ? total : pagina));
    this.cargar();
  }

  paginaAnterior(): void {
    this.irAPagina(this.pagina() - 1);
  }

  paginaSiguiente(): void {
    this.irAPagina(Math.min(this.pagina() + 1, this.totalPaginas()));
  }

  irAUltimaPagina(): void {
    this.irAPagina(this.totalPaginas());
  }

  totalPaginas(): number {
    return Math.max(1, this.resultado()?.totalPaginas ?? 1);
  }

  goDetalles(id: number): void {
    this.router.navigate(['renovaciones', 'detalles', id]);
  }
}
