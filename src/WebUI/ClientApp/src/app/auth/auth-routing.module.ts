import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';

import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { ApplicationPaths } from './api-authorization.constants';

const routes: Routes = [
  { path: ApplicationPaths.Register, component: LoginComponent, data: { title: marker('Register') } },
  { path: ApplicationPaths.Profile, component: LoginComponent, data: { title: marker('Profile') } },
  { path: ApplicationPaths.Login, component: LoginComponent, data: { title: marker('Login') } },
  { path: ApplicationPaths.LoginFailed, component: LoginComponent, data: { title: marker('Login') } },
  { path: ApplicationPaths.LoginCallback, component: LoginComponent, data: { title: marker('Login') } },
  { path: ApplicationPaths.LogOut, component: LogoutComponent, data: { title: marker('Logout') } },
  { path: ApplicationPaths.LoggedOut, component: LogoutComponent, data: { title: marker('Logged out') } },
  { path: ApplicationPaths.LogOutCallback, component: LogoutComponent, data: { title: marker('Logout') } },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class AuthRoutingModule {}
