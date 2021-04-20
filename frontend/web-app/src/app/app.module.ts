import './prototypes';
import { AppRoutingModule } from './app-routing.module';
import { NgModule, ErrorHandler } from '@angular/core';
import { NavbarModule } from './components/navbar/navbar.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppService } from './services/app.service';
import { ErrorHandlingService } from './services/error-handling.service';
import { HomeModule } from './components/pages/home/home.module';
import { SharedModule } from './components/shared/shared.module';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { environment } from 'src/environments/environment';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { ODataModule } from 'angular-odata';
import { SambaModule } from './components/pages/samba/samba.module';
import { TransmissionModule } from './components/pages/transmission/transmission.module';
import { BrowserModule } from '@angular/platform-browser';
import { CronModule } from './components/pages/cron/cron.module';
import { ODataService } from './services/odata.service';
import { AuthService } from './services/auth.service';
import { AppComponent } from './app.component';
import { LoginModule } from './components/pages/login/login.module';
import { AuthInterceptor } from './auth.interceptor';
import { AuthGuard } from './auth-guard';
import { JwtModule } from '@auth0/angular-jwt';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    ODataModule.forRoot({
      serviceRootUrl: `${environment.urls.api}odata`
    }),
    BrowserModule,
    BrowserAnimationsModule,
    HomeModule,
    SambaModule,
    LoginModule,
    CronModule,
    TransmissionModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    NavbarModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('auth-token')
      },
    })
  ],
  providers: [
    AppService,
    AuthGuard,
    ErrorHandlingService,
    ODataService,
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: ErrorHandler, useClass: ErrorHandlingService },
    { provide: MAT_DATE_LOCALE, useValue: 'pl-PL' },
  ],
  bootstrap: [
    AppComponent
  ], entryComponents: [

  ]
})
export class AppModule { }

// required for AOT compilation
// eslint-disable-next-line
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, `${environment.urls.app}assets/i18n/`);
}
