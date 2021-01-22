import { Component, OnInit } from '@angular/core';
import { AuthorizeService, CredentialsService } from '@app/auth';
import { Observable } from 'rxjs';
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
}
