import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from '@auth';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  menuHidden = true;
  public isAuthenticated: Observable<boolean>;

  constructor(private authorizeService: AuthorizeService) {}

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
  }

  toggleMenu() {
    this.menuHidden = !this.menuHidden;
  }
}
