// cleanarch
import { inject, TestBed } from '@angular/core/testing';
import { AuthorizeGuard } from './authorize.guard';

xdescribe('AuthorizeGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthorizeGuard],
    });
  });

  it('should ...', inject([AuthorizeGuard], (guard: AuthorizeGuard) => {
    expect(guard).toBeTruthy();
  }));
});
