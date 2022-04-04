import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PrivateListComponent } from './components/private-list/private-list.component';
import { RequestComponent } from './components/request/request.component';
import { ChangePasswordComponent } from './components/users/change-password/change-password.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { UserUpdateComponent } from './components/users/user-update/user-update.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'home', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'request/:service', component: RequestComponent},
      {path: 'user/change-password', component: ChangePasswordComponent},
      {path: 'user/list', component: UserListComponent},
      {path: 'user/update/:id', component: UserUpdateComponent},
      {path: 'list', component: PrivateListComponent},
      
      { path: '**', component: HomeComponent, pathMatch: 'full' }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
