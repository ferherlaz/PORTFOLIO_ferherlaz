import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es';
import { bootstrapApplication } from '@angular/platform-browser';
import { defineLocale, esLocale } from 'ngx-bootstrap/chronos';
import { AppComponent } from './app/app.component';
import { appConfig } from './app/app.config';

registerLocaleData(localeEs);
defineLocale('es', esLocale);

bootstrapApplication(AppComponent, appConfig).catch((err) => console.error(err));
