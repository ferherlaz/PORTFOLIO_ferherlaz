import { Component, computed, ElementRef, forwardRef, inject, input, signal } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { TranslatePipe } from '@ngx-translate/core';

export interface OpcionSelect<T> {
  valor: T;
  /** Clave de traducción o texto literal (si no existe la clave, se muestra tal cual). */
  etiqueta: string;
}

let siguienteId = 0;

/**
 * Desplegable con el estilo del proyecto (el <select> nativo no permite dar estilo a la lista abierta).
 * Funciona con ngModel, con teclado (flechas, Inicio/Fin, Enter/Espacio, Escape) y sigue el patrón ARIA "combobox".
 */
@Component({
  selector: 'app-select',
  imports: [TranslatePipe],
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => SelectComponent), multi: true }],
  host: {
    '[class.formulario]': "variante() === 'formulario'",
    '(document:click)': 'alClicFuera($event)',
  },
  template: `
    <button
      type="button"
      class="disparador"
      role="combobox"
      aria-haspopup="listbox"
      [id]="inputId()"
      [attr.aria-expanded]="abierto()"
      [attr.aria-controls]="listaId"
      [attr.aria-activedescendant]="abierto() ? listaId + '-' + activo() : null"
      [disabled]="deshabilitado()"
      (click)="alternar()"
      (keydown)="teclado($event)"
      (keyup.space)="$event.preventDefault()"
      (blur)="alTocar()"
    >
      <span class="texto">{{ seleccionada()?.etiqueta ?? '' | translate }}</span>
      <span class="flecha" aria-hidden="true"></span>
    </button>

    @if (abierto()) {
      <ul class="panel" role="listbox" [id]="listaId" [class.derecha]="alinear() === 'derecha'">
        @for (opcion of opciones(); track $index) {
          <li
            role="option"
            [id]="listaId + '-' + $index"
            [attr.aria-selected]="opcion.valor === valor()"
            [class.seleccionada]="opcion.valor === valor()"
            [class.activa]="$index === activo()"
            (mousedown)="$event.preventDefault()"
            (mouseenter)="activo.set($index)"
            (click)="elegir(opcion)"
          >
            <span>{{ opcion.etiqueta | translate }}</span>
            @if (opcion.valor === valor()) {
              <span class="icon-checkmark" aria-hidden="true"></span>
            }
          </li>
        }
      </ul>
    }
  `,
  styles: `
    :host {
      position: relative;
      display: inline-block;
      font-family: Roboto, var(--bs-font-sans-serif);
    }
    :host(.formulario) {
      display: block;
    }

    .disparador {
      display: flex;
      align-items: center;
      justify-content: space-between;
      gap: 12px;
      width: 100%;
      padding: 5px 10px;
      background: #fff;
      border: 1px solid #cdcdcd;
      border-radius: 5px;
      color: #353535;
      font-size: 16px;
      font-weight: 700;
      text-align: left;
      cursor: pointer;
      transition: border-color 0.15s, box-shadow 0.15s;
    }
    :host(.formulario) .disparador {
      min-height: 38px;
      padding: 0.375rem 0.75rem;
      border-color: #dee2e6;
      border-radius: 0.375rem;
      font-weight: 400;
    }
    .disparador:hover {
      border-color: #9fc3e2;
    }
    .disparador:focus-visible,
    .disparador[aria-expanded='true'] {
      outline: none;
      border-color: #4b98d9;
      box-shadow: 0 0 0 3px #cfe1f1;
    }
    .disparador:disabled {
      opacity: 0.6;
      cursor: default;
    }

    /* Misma flecha que los desplegables de la cabecera */
    .flecha {
      flex: none;
      border-top: 0.35em solid #4b98d9;
      border-left: 0.35em solid transparent;
      border-right: 0.35em solid transparent;
      transition: transform 0.15s;
    }
    .disparador[aria-expanded='true'] .flecha {
      transform: rotate(180deg);
    }

    /* Panel con el estilo de .dropdown-menu del proyecto: sin borde, radio 12px y separadores grises */
    .panel {
      position: absolute;
      top: calc(100% + 6px);
      left: 0;
      z-index: 1060;
      min-width: 100%;
      max-height: 280px;
      margin: 0;
      padding: 0;
      overflow-y: auto;
      list-style: none;
      background: #fff;
      border-radius: 12px;
      box-shadow: 0 4px 8px 1px #3746491a, 0 3px 5px #37464933;
    }
    .panel.derecha {
      left: auto;
      right: 0;
    }

    .panel li {
      display: flex;
      align-items: center;
      justify-content: space-between;
      gap: 16px;
      padding: 10px 16px;
      border-bottom: 1.5px solid #e6e7e8;
      border-left: 2px solid transparent;
      color: #353535;
      font-size: 16px;
      font-weight: 400;
      white-space: nowrap;
      cursor: pointer;
    }
    .panel li:last-child {
      border-bottom: none;
    }
    .panel li.activa {
      background: #f7f7fc;
      color: #4b98d9;
    }
    .panel li.seleccionada {
      border-left-color: #4b98d9;
      font-weight: 700;
    }
    .icon-checkmark {
      color: #4b98d9;
      font-size: 14px;
    }
  `,
})
export class SelectComponent<T> implements ControlValueAccessor {
  private readonly host = inject<ElementRef<HTMLElement>>(ElementRef);

  readonly opciones = input.required<OpcionSelect<T>[]>();
  /** 'compacto' para ordenar/paginar (texto en negrita); 'formulario' ocupa todo el ancho como un form-select. */
  readonly variante = input<'compacto' | 'formulario'>('compacto');
  /** Lado hacia el que se abre el panel; 'derecha' para controles pegados al borde derecho. */
  readonly alinear = input<'izquierda' | 'derecha'>('izquierda');
  /** Id del botón, para enlazarlo con su <label for>. */
  readonly inputId = input<string | null>(null);

  protected readonly listaId = `app-select-${++siguienteId}`;
  protected readonly valor = signal<T | undefined>(undefined);
  protected readonly abierto = signal(false);
  protected readonly activo = signal(0);
  protected readonly deshabilitado = signal(false);
  protected readonly seleccionada = computed(() => this.opciones().find((o) => o.valor === this.valor()));

  private onChange: (valor: T) => void = () => {};
  private onTouched: () => void = () => {};

  writeValue(valor: T): void {
    this.valor.set(valor);
  }

  registerOnChange(fn: (valor: T) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(deshabilitado: boolean): void {
    this.deshabilitado.set(deshabilitado);
  }

  alternar(): void {
    if (this.abierto()) this.cerrar();
    else this.abrir();
  }

  elegir(opcion: OpcionSelect<T>): void {
    this.cerrar();
    if (opcion.valor === this.valor()) return;
    this.valor.set(opcion.valor);
    this.onChange(opcion.valor);
  }

  teclado(evento: KeyboardEvent): void {
    const total = this.opciones().length;
    switch (evento.key) {
      case 'ArrowDown':
      case 'ArrowUp': {
        evento.preventDefault();
        if (!this.abierto()) {
          this.abrir();
          return;
        }
        const paso = evento.key === 'ArrowDown' ? 1 : -1;
        this.activo.update((i) => (i + paso + total) % total);
        this.desplazarHastaActiva();
        break;
      }
      case 'Home':
      case 'End':
        if (this.abierto()) {
          evento.preventDefault();
          this.activo.set(evento.key === 'Home' ? 0 : total - 1);
          this.desplazarHastaActiva();
        }
        break;
      case 'Enter':
      case ' ':
        evento.preventDefault();
        if (this.abierto()) this.elegir(this.opciones()[this.activo()]);
        else this.abrir();
        break;
      case 'Escape':
        if (this.abierto()) {
          // Que no cierre también el panel lateral de filtros.
          evento.preventDefault();
          evento.stopPropagation();
          this.cerrar();
        }
        break;
      case 'Tab':
        this.cerrar();
        break;
    }
  }

  alClicFuera(evento: Event): void {
    if (this.abierto() && !this.host.nativeElement.contains(evento.target as Node)) this.cerrar();
  }

  alTocar(): void {
    this.onTouched();
  }

  private abrir(): void {
    const indice = this.opciones().findIndex((o) => o.valor === this.valor());
    this.activo.set(Math.max(0, indice));
    this.abierto.set(true);
    this.desplazarHastaActiva();
  }

  private cerrar(): void {
    this.abierto.set(false);
  }

  private desplazarHastaActiva(): void {
    queueMicrotask(() =>
      this.host.nativeElement.querySelector(`#${this.listaId}-${this.activo()}`)?.scrollIntoView({ block: 'nearest' }),
    );
  }
}
