import { inject, Injectable, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterStateSnapshot, TitleStrategy } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

/** Los títulos de ruta son claves de traducción: el título de la pestaña cambia con el idioma. */
@Injectable({ providedIn: 'root' })
export class TranslatedTitleStrategy extends TitleStrategy implements OnDestroy {
  private readonly title = inject(Title);
  private readonly translate = inject(TranslateService);
  private subscription?: Subscription;

  override updateTitle(snapshot: RouterStateSnapshot): void {
    const clave = this.buildTitle(snapshot);
    this.subscription?.unsubscribe();
    if (!clave) {
      this.title.setTitle('Proyecto Práctica');
      return;
    }
    this.subscription = this.translate
      .stream(clave)
      .subscribe((texto: string) => this.title.setTitle(`${texto} · Proyecto Práctica`));
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}
