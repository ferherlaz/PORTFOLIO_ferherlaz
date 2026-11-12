import { Component } from '@angular/core';
import { TranslatePipe } from '@ngx-translate/core';

@Component({
  selector: 'app-footer',
  imports: [TranslatePipe],
  template: `
    <footer class="footer">
      <div class="row justify-content-between">
        <div class="footer-help col-12 col-md-4">
          <div class="footer-help1">
            <h4>{{ 'FOOTER.Ayuda' | translate }}</h4>
            <div class="mail d-flex"><span class="icon-envelop"></span><p>atencionalcliente&#64;securainsurance.com</p></div>
            <div class="phone d-flex"><span class="icon-phone"></span><p>900 926 676</p></div>
          </div>
        </div>
        <div class="footer-links col-12 col-md-3">
          <div class="footer-help2">
            <p>SECURA INSURANCE ESPAÑA, S.A.U</p>
            <p>Calle Ribera del Loira 46, Madrid, 28042</p>
          </div>
        </div>
      </div>
      <div class="row justify-content-between">
        <div class="footer-help col-12 col-md-4">
          <div class="footer-links1">
            <a href="#" (click)="$event.preventDefault()">{{ 'FOOTER.PoliticaCookies' | translate }}</a>
            <div class="vr"></div>
            <a href="#" (click)="$event.preventDefault()">{{ 'FOOTER.AvisoLegal' | translate }}</a>
            <div class="vr"></div>
            <a href="#" (click)="$event.preventDefault()">{{ 'FOOTER.PoliticaPrivacidad' | translate }}</a>
          </div>
        </div>
        <div class="footer-links col-12 col-md-3">
          <div class="footer-links2"><p>© 2022 SecuraInsurance</p></div>
        </div>
      </div>
    </footer>
  `,
  styleUrl: './footer.component.css',
})
export class FooterComponent {}
