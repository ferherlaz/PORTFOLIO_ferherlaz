import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { TranslatePipe } from '@ngx-translate/core';
import { ClientePotencial } from '../../../../core/models';
import { ClientesService } from '../../../../core/services/clientes.service';

/** Clientes potenciales: la papelera hace un DELETE real en la API. */
@Component({
  selector: 'app-clients',
  imports: [TranslatePipe],
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.css',
})
export class ClientsComponent implements OnInit {
  private readonly service = inject(ClientesService);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly clientes = signal<ClientePotencial[] | null>(null);
  protected readonly error = signal(false);
  protected readonly borrando = signal<number | null>(null);
  protected readonly errorBorrar = signal(false);

  protected readonly claveProducto: Record<ClientePotencial['producto'], string> = {
    Moto: 'CLIENTS.SeguroMoto',
    Casa: 'CLIENTS.SeguroCasa',
  };

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.error.set(false);
    this.service
      .listar()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (c) => this.clientes.set(c),
        error: () => this.error.set(true),
      });
  }

  borrarCliente(cliente: ClientePotencial): void {
    this.borrando.set(cliente.id);
    this.errorBorrar.set(false);
    this.service
      .borrar(cliente.id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.clientes.update((lista) => lista?.filter((c) => c.id !== cliente.id) ?? null);
          this.borrando.set(null);
        },
        error: () => {
          this.errorBorrar.set(true);
          this.borrando.set(null);
        },
      });
  }
}
