import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from '@angular/service-worker';
import { AuthModule } from '@app/auth';
import { AuthorizeInterceptor } from '@app/auth/authorize.interceptor';
import { CoreModule } from '@core';
import { environment } from '@env/environment';
// cleanarch
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '@shared';
import { AboutModule } from './about/about.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CounterModule } from './counter/counter.module';
import { FetchDataModule } from './fetch-data/fetch-data.module';
import { HomeModule } from './home/home.module';
import { ShellModule } from './shell/shell.module';
import { TodoModule } from './todo/todo.module';

// import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }), // cleanarch
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
    FetchDataModule,
    AuthModule,

    // cleanarch
    FontAwesomeModule, // x cleanarch
    BrowserAnimationsModule, // cleanarch
    // ModalModule.forRoot() // cleanarch
    // end cleanarch

    AppRoutingModule, // must be imported as the last module as it contains the fallback route
  ],
  declarations: [AppComponent],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
