import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { I18nModule } from '@app/i18n';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { AuthRoutingModule } from './auth-routing.module';
import { AuthorizeInterceptor } from './authorize.interceptor';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  imports: [CommonModule, HttpClientModule, TranslateModule, NgbModule, I18nModule, AuthRoutingModule],
  declarations: [LoginComponent, LogoutComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }],
  exports: [LoginComponent, LogoutComponent],
})
export class AuthModule {}
