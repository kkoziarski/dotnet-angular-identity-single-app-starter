// cleanarch
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { inject, TestBed } from '@angular/core/testing';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable } from 'rxjs';
import { QueryParameterNames } from './api-authorization.constants';
import { AuthorizeGuard } from './authorize.guard';
import { AuthorizeService } from './authorize.service';
import { MockAuthorizeService } from './authorize.service.mock';

describe('AuthorizeGuard', () => {
  let authorizeGuard: AuthorizeGuard;
  let authorizeService: MockAuthorizeService;
  let mockRouter: any;
  let mockSnapshot: any;

  beforeEach(() => {
    mockRouter = {
      navigate: jest.fn(),
    };
    mockSnapshot = jest.fn(() => ({
      toString: jest.fn(),
    }));

    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule],
      providers: [
        AuthorizeGuard,
        { provide: AuthorizeService, useClass: MockAuthorizeService },
        { provide: Router, useValue: mockRouter },
      ],
    });

    authorizeGuard = TestBed.inject(AuthorizeGuard);
    authorizeService = TestBed.inject(AuthorizeService);
  });

  it('should create', inject([AuthorizeGuard], (guard: AuthorizeGuard) => {
    expect(guard).toBeTruthy();
  }));

  it('should have a canActivate method', () => {
    expect(typeof authorizeGuard.canActivate).toBe('function');
  });

  xit('should return true if user is authenticated', () => {
    expect(authorizeGuard.canActivate(new ActivatedRouteSnapshot(), mockSnapshot)).toBe(true);
  });

  it('should return true if user is authenticated async', (done) => {
    const canActivate$ = authorizeGuard.canActivate(new ActivatedRouteSnapshot(), mockSnapshot) as Observable<boolean>;
    canActivate$.subscribe((isAuth) => {
      expect(isAuth).toBe(true);
      done();
    });
  });

  it('should return false and redirect to login if user is not authenticated', (done) => {
    // Arrange
    authorizeService.user = null;
    // Act
    const canActivate$ = authorizeGuard.canActivate(new ActivatedRouteSnapshot(), mockSnapshot) as Observable<boolean>;
    canActivate$.subscribe((isAuth) => {
      // Assert
      expect(isAuth).toBe(false);
      expect(mockRouter.navigate).toHaveBeenCalledWith(['authentication', 'login'], {
        queryParams: { [QueryParameterNames.ReturnUrl]: undefined },
        // replaceUrl: true,
      });
      done();
    });
  });

  it('should save url as queryParam if user is not authenticated', (done) => {
    // Arrange
    authorizeService.user = null;
    mockRouter.url = '/about';
    mockSnapshot.url = '/about';
    // Act
    const canActivate$ = authorizeGuard.canActivate(new ActivatedRouteSnapshot(), mockSnapshot) as Observable<boolean>;
    canActivate$.subscribe((isAuth) => {
      // Assert
      expect(isAuth).toBe(false);
      expect(mockRouter.navigate).toHaveBeenCalledWith(['authentication', 'login'], {
        queryParams: { ['returnUrl']: mockRouter.url },
        // replaceUrl: true,
      });
      done();
    });
  });
});
