import { Observable, of } from 'rxjs';
import { IUser } from './authorize.service';

export class MockAuthorizeService {
  user: IUser | null = {
    name: 'test',
  };

  public getUser(): Observable<IUser | null> {
    return of(this.user);
  }

  public isAuthenticated(): Observable<boolean> {
    return of(true);
  }
}
