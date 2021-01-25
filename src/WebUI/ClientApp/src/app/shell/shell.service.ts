import { Route, Routes } from '@angular/router';
import { AuthorizeGuard } from '@app/auth';
import { ShellComponent } from './shell.component';

/**
 * Provides helper methods to create routes.
 */
export class Shell {
  /**
   * Creates routes using the shell component and authentication.
   * @param routes The routes to add.
   * @return The new route using shell as the base.
   */
  static childRoutes(routes: Routes, useAuthGuard: boolean = true): Route {
    return {
      path: '',
      component: ShellComponent,
      children: routes,
      canActivate: useAuthGuard ? [AuthorizeGuard] : [],
      // Reuse ShellComponent instance when navigating between child views
      data: { reuse: true },
    };
  }
}
