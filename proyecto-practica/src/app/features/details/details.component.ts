import { CurrencyPipe, DatePipe, NgTemplateOutlet } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, DestroyRef, inject, input, numberAttribute, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed, toObservable } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from '@ngx-translate/core';
import { catchError, of, switchMap, tap } from 'rxjs';
import { erroresDeValidacion } from '../../core/api';
import { Bloque, BloquesPoliza, ErroresCampo, PolizaDetalle } from '../../core/models';
import { PolizasService } from '../../core/services/polizas.service';
import { EstadoBadgeComponent } from '../../shared/estado-badge.component';

interface Campo {
  clave: string;
  /** Clave de traducción, o texto literal si no empieza por "DETAILS.". */
  etiqueta: string;
  tipo?: 'text' | 'email' | 'tel';
}

interface DefinicionBloque {
  bloque: Bloque;
  titulo: string;
  campos: Campo[];
}

type Formulario = Record<string, string>;
type EstadoGuardado = 'guardado' | 'invalido' | 'error' | null;

const BLOQUES: Record<Bloque, DefinicionBloque> = {
  titular: {
    bloque: 'titular',
    titulo: 'DETAILS.Titular',
    campos: [
      { clave: 'cif', etiqueta: 'CIF / NIF' },
      { clave: 'nombre', etiqueta: 'DETAILS.Nombre' },
      { clave: 'dirFiscal', etiqueta: 'DETAILS.DireccionFiscal' },
    ],
  },
  facturacion: {
    bloque: 'facturacion',
    titulo: 'DETAILS.DireccionFacturacion',
    campos: [
      { clave: 'dirFacturacion', etiqueta: 'DETAILS.Direccion' },
      { clave: 'numero', etiqueta: 'DETAILS.Numero' },
      { clave: 'ciudad', etiqueta: 'DETAILS.Ciudad' },
      { clave: 'cp', etiqueta: 'DETAILS.CodigoPostal' },
    ],
  },
  pago: {
    bloque: 'pago',
    titulo: 'DETAILS.MetodoPago',
    campos: [
      { clave: 'nombre', etiqueta: 'DETAILS.Nombre' },
      { clave: 'cif', etiqueta: 'CIF / NIF' },
      { clave: 'tipoCobro', etiqueta: 'DETAILS.TipoCobro' },
      { clave: 'iban', etiqueta: 'IBAN' },
      { clave: 'banco', etiqueta: 'DETAILS.Banco' },
    ],
  },
  correspondencia: {
    bloque: 'correspondencia',
    titulo: 'DETAILS.DirCorrespondencia',
    campos: [
      { clave: 'datos', etiqueta: 'DETAILS.Datos' },
      { clave: 'poblacion', etiqueta: 'DETAILS.Poblacion' },
      { clave: 'cp', etiqueta: 'DETAILS.CodigoPostal' },
      { clave: 'direccion', etiqueta: 'DETAILS.Direccion' },
      { clave: 'numero', etiqueta: 'DETAILS.Numero' },
      { clave: 'aclarador', etiqueta: 'DETAILS.Aclarador' },
    ],
  },
  contacto: {
    bloque: 'contacto',
    titulo: 'DETAILS.Contacto',
    campos: [
      { clave: 'correoPrincipal', etiqueta: 'DETAILS.CorreoPrincipal', tipo: 'email' },
      { clave: 'correoSecundario', etiqueta: 'DETAILS.Correo2', tipo: 'email' },
      { clave: 'telefono', etiqueta: 'DETAILS.TelefonoPrincipal', tipo: 'tel' },
    ],
  },
};

const NOMBRES_BLOQUES = Object.keys(BLOQUES) as Bloque[];

/**
 * Detalle de póliza. Cada tarjeta se edita y se guarda por separado (PUT por bloque).
 * "Cancelar" restaura lo último guardado y los errores de la API se muestran bajo cada campo.
 */
@Component({
  selector: 'app-details',
  imports: [FormsModule, RouterLink, NgTemplateOutlet, TranslatePipe, DatePipe, CurrencyPipe, EstadoBadgeComponent],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css',
})
export class DetailsComponent implements OnInit {
  private readonly service = inject(PolizasService);
  private readonly destroyRef = inject(DestroyRef);

  readonly id = input.required({ transform: numberAttribute });
  private readonly id$ = toObservable(this.id);

  protected readonly filaSuperior = [BLOQUES.titular, BLOQUES.facturacion];
  protected readonly filaInferior = [BLOQUES.pago, BLOQUES.correspondencia, BLOQUES.contacto];

  protected readonly detalle = signal<PolizaDetalle | null>(null);
  protected readonly cargando = signal(true);
  protected readonly errorCarga = signal<'no-encontrada' | 'conexion' | null>(null);

  /** Valores en edición (enlazados a los inputs) y última versión guardada en el servidor. */
  protected formularios = this.formulariosVacios();
  private guardados = this.formulariosVacios();

  protected readonly editando = signal(new Set<Bloque>());
  protected readonly guardando = signal(new Set<Bloque>());
  protected readonly errores = signal<Partial<Record<Bloque, ErroresCampo>>>({});
  protected readonly estadoGuardado = signal<Partial<Record<Bloque, EstadoGuardado>>>({});

  ngOnInit(): void {
    this.id$
      .pipe(
        tap(() => {
          this.cargando.set(true);
          this.errorCarga.set(null);
        }),
        switchMap((id) =>
          this.service.detalle(id).pipe(
            catchError((e: unknown) => {
              this.errorCarga.set(e instanceof HttpErrorResponse && e.status === 404 ? 'no-encontrada' : 'conexion');
              return of(null);
            }),
          ),
        ),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe((detalle) => {
        this.cargando.set(false);
        this.detalle.set(detalle);
        if (detalle) this.cargarFormularios(detalle);
      });
  }

  reintentar(): void {
    this.cargando.set(true);
    this.service
      .detalle(this.id())
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (d) => {
          this.cargando.set(false);
          this.errorCarga.set(null);
          this.detalle.set(d);
          this.cargarFormularios(d);
        },
        error: () => {
          this.cargando.set(false);
          this.errorCarga.set('conexion');
        },
      });
  }

  estaBloqueado(bloque: Bloque): boolean {
    return !this.editando().has(bloque);
  }

  estaGuardando(bloque: Bloque): boolean {
    return this.guardando().has(bloque);
  }

  editar(bloque: Bloque): void {
    this.editando.update((s) => new Set(s).add(bloque));
    this.marcar(bloque, null);
  }

  cancelar(bloque: Bloque): void {
    this.formularios[bloque] = { ...this.guardados[bloque] };
    this.limpiarErrores(bloque);
    this.marcar(bloque, null);
    this.bloquear(bloque);
  }

  guardar(bloque: Bloque): void {
    if (this.estaBloqueado(bloque) || this.estaGuardando(bloque)) return;

    this.guardando.update((s) => new Set(s).add(bloque));
    this.marcar(bloque, null);

    const datos = this.formularios[bloque] as unknown as BloquesPoliza[typeof bloque];
    this.service
      .guardar(this.id(), bloque, datos)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (guardado) => {
          // La API devuelve el bloque normalizado (CIF en mayúsculas, espacios recortados...).
          this.guardados[bloque] = this.aFormulario(guardado);
          this.formularios[bloque] = { ...this.guardados[bloque] };
          this.limpiarErrores(bloque);
          this.terminarGuardado(bloque);
          this.bloquear(bloque);
          this.marcar(bloque, 'guardado');
        },
        error: (e: unknown) => {
          const errores = erroresDeValidacion(e);
          this.errores.update((todos) => ({ ...todos, [bloque]: errores ?? {} }));
          this.terminarGuardado(bloque);
          this.marcar(bloque, errores ? 'invalido' : 'error');
        },
      });
  }

  formularioDe(bloque: Bloque): Formulario {
    return this.formularios[bloque];
  }

  estadoDe(bloque: Bloque): EstadoGuardado {
    return this.estadoGuardado()[bloque] ?? null;
  }

  errorDe(bloque: Bloque, campo: string): string | null {
    return this.errores()[bloque]?.[campo]?.[0] ?? null;
  }

  esClaveTraduccion(etiqueta: string): boolean {
    return etiqueta.startsWith('DETAILS.');
  }

  private cargarFormularios(detalle: PolizaDetalle): void {
    for (const bloque of NOMBRES_BLOQUES) {
      this.guardados[bloque] = this.aFormulario(detalle[bloque]);
      this.formularios[bloque] = { ...this.guardados[bloque] };
    }
    this.editando.set(new Set());
    this.errores.set({});
    this.estadoGuardado.set({});
  }

  private aFormulario(valor: object): Formulario {
    return Object.fromEntries(Object.entries(valor).map(([k, v]) => [k, String(v ?? '')]));
  }

  private formulariosVacios(): Record<Bloque, Formulario> {
    return { titular: {}, facturacion: {}, pago: {}, correspondencia: {}, contacto: {} };
  }

  private bloquear(bloque: Bloque): void {
    this.editando.update((s) => {
      const nuevo = new Set(s);
      nuevo.delete(bloque);
      return nuevo;
    });
  }

  private terminarGuardado(bloque: Bloque): void {
    this.guardando.update((s) => {
      const nuevo = new Set(s);
      nuevo.delete(bloque);
      return nuevo;
    });
  }

  private limpiarErrores(bloque: Bloque): void {
    this.errores.update((todos) => ({ ...todos, [bloque]: {} }));
  }

  private marcar(bloque: Bloque, estado: EstadoGuardado): void {
    this.estadoGuardado.update((todos) => ({ ...todos, [bloque]: estado }));
  }
}
