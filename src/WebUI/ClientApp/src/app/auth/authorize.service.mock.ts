import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { IAuthenticationResult, IUser } from './authorize.service';

export class MockAuthorizeService {
  public user?: IUser | null = {
    name: 'test',
  };

  public authResult?: IAuthenticationResult = null;

  public getUser(): Observable<IUser | null> {
    return of(this.user);
  }

  public isAuthenticated(): Observable<boolean> {
    return this.getUser().pipe(map((u) => !!u));
  }

  public getAccessToken(): Observable<string> {
    return of('abc-token');
  }

  public signIn = async (state: any) => this.authResult;

  public completeSignIn = async (url: string) => this.authResult;

  public signOut = async (state: any) => this.authResult;

  public completeSignOut = async (url: string) => this.authResult;
}
