import { AlertService } from 'src/app/_services/alert.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertService: AlertService
  ){}

  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertService.error('คุณไม่มีสิทธิเข้าหน้านี้!');
    this.router.navigate(['/']);
    return false;
  }

}
