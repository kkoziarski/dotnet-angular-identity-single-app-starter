import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { I18nModule } from '@app/i18n';
import { AuthModule } from '@auth';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { HeaderComponent } from './header/header.component';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { ShellComponent } from './shell.component';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  imports: [CommonModule, TranslateModule, NgbModule, AuthModule, I18nModule, RouterModule],
  declarations: [HeaderComponent, LoginMenuComponent, ShellComponent, FooterComponent],
})
export class ShellModule {}
