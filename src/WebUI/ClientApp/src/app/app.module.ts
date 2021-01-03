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
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

// cleanarch
// import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavMenuComponent } from './shell/header/nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
// import { TodoComponent } from './todo/todo.component';
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
    AuthModule,
    AppRoutingModule, // must be imported as the last module as it contains the fallback route
    // cleanarch
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    // ApiAuthorizationModule,
    // RouterModule.forRoot([
    //   { path: '', component: HomeComponent, pathMatch: 'full' },
    //   { path: 'counter', component: CounterComponent },
    //   { path: 'fetch-data', component: FetchDataComponent },
    //   { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] },
    // ]),
    BrowserAnimationsModule,
    // ModalModule.forRoot()
  ],
  declarations: [
    AppComponent,
    // cleanarch
    // AppComponent,
    // NavMenuComponent,
    // HomeComponent,
    // CounterComponent,
    // FetchDataComponent,
    // TodoComponent,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
