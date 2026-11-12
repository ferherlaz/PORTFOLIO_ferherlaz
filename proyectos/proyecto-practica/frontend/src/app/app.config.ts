import { HttpClient, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, TitleStrategy, withComponentInputBinding, withHashLocation } from '@angular/router';
import { provideTranslateService, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { routes } from './app.routes';
import { conexionInterceptor } from './core/services/conexion.service';
import { TranslatedTitleStrategy } from './core/translated-title.strategy';

const crearLoader = (http: HttpClient) => new TranslateHttpLoader(http, 'assets/i18n/', '.json');

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    // Rutas con # : la app se sirve como estática dentro del portfolio (/proyecto-practica/).
    provideRouter(routes, withComponentInputBinding(), withHashLocation()),
    { provide: TitleStrategy, useClass: TranslatedTitleStrategy },
    provideHttpClient(withFetch(), withInterceptors([conexionInterceptor])),
    provideTranslateService({
      defaultLanguage: 'es',
      loader: { provide: TranslateLoader, useFactory: crearLoader, deps: [HttpClient] },
    }),
    provideAnimationsAsync(),
    importProvidersFrom(BsDatepickerModule.forRoot()),
  ],
};
