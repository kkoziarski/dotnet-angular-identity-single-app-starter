import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { I18nModule } from '@app/i18n';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login.component';
import { LoginComponent as CleanArchLoginComponent } from './login/login.component';
import { LoginMenuComponent as CleanArchLoginMenuComponent } from './login-menu/login-menu.component';
import { LogoutComponent as CleanArchLogoutComponent } from './logout/logout.component';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, TranslateModule, NgbModule, I18nModule, AuthRoutingModule],
  declarations: [LoginComponent, CleanArchLoginComponent, CleanArchLoginMenuComponent, CleanArchLogoutComponent],
  exports: [CleanArchLoginComponent, CleanArchLoginMenuComponent, CleanArchLogoutComponent],
})
export class AuthModule {}
