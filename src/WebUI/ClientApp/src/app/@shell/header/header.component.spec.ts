import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MockAuthorizeService } from '@app/@auth/authorize.service.mock';
import { I18nModule } from '@app/i18n';
import { AuthorizeService } from '@auth';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { LoginMenuComponent } from './../login-menu/login-menu.component';
import { HeaderComponent } from './header.component';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [RouterTestingModule, NgbModule, TranslateModule.forRoot(), I18nModule],
        declarations: [HeaderComponent, LoginMenuComponent],
        providers: [{ provide: AuthorizeService, useClass: MockAuthorizeService }],
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
