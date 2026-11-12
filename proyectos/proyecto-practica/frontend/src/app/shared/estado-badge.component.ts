import { Component, input } from '@angular/core';
import { TranslatePipe } from '@ngx-translate/core';
import { EstadoPoliza } from '../core/models';

@Component({
  selector: 'app-estado-badge',
  imports: [TranslatePipe],
  template: `
    @switch (estado()) {
      @case ('Pendiente') {
        <span class="estado pendiente"><span class="icon-clock"></span>{{ 'UNCOMING_RENOVATIONS.Pendiente' | translate }}</span>
      }
      @case ('Pagada') {
        <span class="estado pagada"><span class="icon-checkmark"></span>{{ 'UNCOMING_RENOVATIONS.Pagada' | translate }}</span>
      }
      @case ('Vencido') {
        <span class="estado vencida"><span class="icon-cross"></span>{{ 'UNCOMING_RENOVATIONS.Vencido' | translate }}</span>
      }
    }
  `,
  styles: `
    :host { display: block; }
    .estado {
      display: flex;
      padding: 4px 10px;
      width: fit-content;
      gap: 5px;
      align-items: center;
      border-radius: 12px;
      font-family: Roboto;
      font-weight: 400;
      font-size: 16px;
      letter-spacing: 0em;
    }
    .estado.pendiente { background-color: #ffe74f66; color: #a87d00; }
    .estado.pagada { background-color: #57ae6026; color: #3ba83b; }
    .estado.vencida { background-color: rgba(219, 57, 0, 0.12); color: #b32e00; }
  `,
})
export class EstadoBadgeComponent {
  readonly estado = input.required<EstadoPoliza>();
}
