// cleanarch
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterModule } from '@angular/router';
import { CoreModule } from '@core';
import { SharedModule } from '@shared';
import { AuthModule } from './../../auth/auth.module';
import { AuthorizeService } from './../../auth/authorize.service';
import { LoginMenuComponent } from './login-menu.component';

xdescribe('LoginMenuComponent', () => {
  let component: LoginMenuComponent;
  let fixture: ComponentFixture<LoginMenuComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [
          // CommonModule,
          // TranslateModule,
          // NgbModule,
          // I18nModule,
          RouterModule,
          CoreModule,
          SharedModule,
          AuthModule,
          HttpClientTestingModule,
        ],
        declarations: [LoginMenuComponent],
        providers: [AuthorizeService],
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
