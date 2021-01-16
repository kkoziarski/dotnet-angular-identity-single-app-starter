import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ServiceWorkerModule } from '@angular/service-worker';
import { TranslateModule } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { environment } from '@env/environment';
import { CoreModule } from '@core';
import { SharedModule } from '@shared';
import { AuthModule } from '@app/auth';
import { HomeModule } from './home/home.module';
import { ShellModule } from './shell/shell.module';
import { AboutModule } from './about/about.module';
import { TodoModule } from './todo/todo.module';
import { CounterModule } from './counter/counter.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

// cleanarch
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavMenuComponent } from './shell/header/nav-menu/nav-menu.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
// import { ApiAuthorizationModule } from '@app/auth/api-authorization.module';
import { AuthorizeGuard } from '@app/auth/authorize.guard';
import { AuthorizeInterceptor } from '@app/auth/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  imports: [
    // BrowserModule,
    ServiceWorkerModule.register('./ngsw-worker.js', { enabled: environment.production }),
    FormsModule,
    HttpClientModule,
    TranslateModule.forRoot(),
    NgbModule,
    CoreModule,
    SharedModule,
    ShellModule,
    HomeModule,
    AboutModule,
    TodoModule,
    CounterModule,
    AuthModule,

    // cleanarch
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), // cleanarch
    FontAwesomeModule, // x cleanarch
    // ApiAuthorizationModule, // cleanarch
    // RouterModule.forRoot([ // cleanarch
    //   { path: '', component: HomeComponent, pathMatch: 'full' }, // cleanarch
    //   { path: 'counter', component: CounterComponent }, // cleanarch
    //   { path: 'fetch-data', component: FetchDataComponent }, // cleanarch
    //   { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] }, // cleanarch
    // ]), // cleanarch
    BrowserAnimationsModule, // cleanarch
    // ModalModule.forRoot() // cleanarch
    // end cleanarch

    AppRoutingModule, // must be imported as the last module as it contains the fallback route
  ],
  declarations: [
    AppComponent,
    // cleanarch
    // x AppComponent,
    // NavMenuComponent,
    // x HomeComponent,
    // x CounterComponent,
    // FetchDataComponent,
    // x TodoComponent,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
