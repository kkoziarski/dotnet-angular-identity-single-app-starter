import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MockAuthorizeService } from '@app/@auth/authorize.service.mock';
import { I18nModule } from '@app/i18n';
import { AuthorizeService } from '@auth';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { HeaderComponent } from './header/header.component';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { ShellComponent } from './shell.component';

describe('ShellComponent', () => {
  let component: ShellComponent;
  let fixture: ComponentFixture<ShellComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [RouterTestingModule, NgbModule, TranslateModule.forRoot(), I18nModule],
        providers: [{ provide: AuthorizeService, useClass: MockAuthorizeService }],
        declarations: [HeaderComponent, LoginMenuComponent, ShellComponent],
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(ShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
