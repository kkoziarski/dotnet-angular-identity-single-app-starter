// cleanarch
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { inject, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthorizeGuard } from './authorize.guard';
import { AuthorizeService } from './authorize.service';
import { MockAuthorizeService } from './authorize.service.mock';

describe('AuthorizeGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule],
      providers: [AuthorizeGuard, { provide: AuthorizeService, useClass: MockAuthorizeService }],
    });
  });

  it('should create', inject([AuthorizeGuard], (guard: AuthorizeGuard) => {
    expect(guard).toBeTruthy();
  }));
});
