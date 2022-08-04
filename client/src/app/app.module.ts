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
import { RequestComponent } from './components/request/request.component';
import { ChangePasswordComponent } from './components/users/change-password/change-password.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { UserListSearchComponent } from './components/users/user-list-search/user-list-search.component';
import { LoadingComponent } from './components/loading/loading.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UserUpdateComponent } from './components/users/user-update/user-update.component';
import { FileUploadComponent } from './components/file-upload/file-upload.component';
import { FileUploadModule } from 'ng2-file-upload';
import { PrivateListComponent } from './components/private-list/private-list.component';
import { ServiceListSearchComponent } from './components/service-list-search/service-list-search.component';
import { ServiceEditComponent } from './components/service-edit/service-edit.component';
import { FileListComponent } from './components/file-list/file-list.component';
import { AdminListComponent } from './components/admin-list/admin-list.component';
import { ReportListComponent } from './components/report-list/report-list.component';

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
    HomeComponent,
    RequestComponent,
    ChangePasswordComponent,
    UserListComponent,
    UserListSearchComponent,
    LoadingComponent,
    UserUpdateComponent,
    FileUploadComponent,
    PrivateListComponent,
    ServiceListSearchComponent,
    ServiceEditComponent,
    FileListComponent,
    AdminListComponent,
    ReportListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    FileUploadModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:5001'],
        disallowedRoutes: ['localhost:5001/api/auth/login']
      }
    }),
    PaginationModule.forRoot()
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'th-TH' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
