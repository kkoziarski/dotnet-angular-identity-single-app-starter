import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService, AuthorizeService, CredentialsService } from '@app/auth';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  menuHidden = true;
  public isAuthenticated: Observable<boolean>;

  constructor(private authorizeService: AuthorizeService, private credentialsService: CredentialsService) {}

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated().pipe(tap((x) => console.log('isAuth....', x)));
  }

  toggleMenu() {
    this.menuHidden = !this.menuHidden;
  }

  // logout() {
  //   console.log('logout...');
  //   this.authorizeService.fakeSetAuth(false);
  //   return;
  //   this.authenticationService.logout().subscribe(() => this.router.navigate(['/login'], { replaceUrl: true }));
  // }

  // login() {
  //   console.log('login...');
  //   this.authorizeService.fakeSetAuth(true);
  // }

  get username(): string | null {
    const credentials = this.credentialsService.credentials;
    return credentials ? credentials.username : null;
  }
}
