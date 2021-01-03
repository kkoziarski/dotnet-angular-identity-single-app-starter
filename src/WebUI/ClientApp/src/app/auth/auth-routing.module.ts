import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';

import { LoginComponent } from './login.component';
import { LoginComponent as CleanArchLoginComponent } from './login/login.component';
import { LoginMenuComponent as CleanArchLoginMenuComponent } from './login-menu/login-menu.component';
import { LogoutComponent as CleanArchLogoutComponent } from './logout/logout.component';

const routes: Routes = [{ path: 'login', component: LoginComponent, data: { title: marker('Login') } }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class AuthRoutingModule {}
