import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { MaterialModule } from './_modules/material/material.module';
import { ToastrModule } from 'ngx-toastr';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { MAT_DATE_LOCALE } from '@angular/material/core';

export function tokenGetter(): any {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5001'],
        disallowedRoutes: ['localhost:5001/api/auth/login']
      }
    })
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'th-TH' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
