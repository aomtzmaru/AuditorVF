import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  confirmPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const newPassword = control.get('newPassword');
    const confirmPassword = control.get('confirmPassword');
    if (newPassword?.value !== confirmPassword?.value)
      confirmPassword?.setErrors({ missingPassword: true })
    return null;
  };

  changeForm = this.fb.group({
    oldPassword: [null, [Validators.required, Validators.minLength(8)]],
    newPassword: [null, [Validators.required, Validators.minLength(8)]],
    confirmPassword: [null, [Validators.required, Validators.minLength]]
  }, { validators: [this.confirmPasswordValidator] });

  user: any;
  hide1 = true;
  hide2 = true;
  hide3 = true;
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private alert: AlertService
  ) { }

  ngOnInit(): void {
    this.checkAuthorize();
  }

  checkAuthorize(): void {
    if (this.authService.decodeToken.nameid.includes('@dsi.go.th'))
      this.router.navigate(['home']);
  }

  changPassword(): void {
    if (this.changeForm.valid) {
      this.isLoading = true;
      const oldPassword = this.changeForm.get('oldPassword')?.value;
      const newPassword = this.changeForm.get('newPassword')?.value;
      this.user = {
        username: this.authService.decodeToken.nameid,
        password: oldPassword,
        newPassword: newPassword
      };
      this.authService.changePassword(this.user).subscribe((res: any) => {
        if (res) {
          this.alert.success('เปลี่ยนรหัสผ่านเรียบร้อยแล้ว กรุณาเข้าสู่ระบบใหม่อีกครั้ง');
          this.authService.logout();
          this.isLoading = false;
          this.router.navigate(['home']);
        } else {
          this.isLoading = false;
          this.alert.sweetError('รหัสผ่านเดิมไม่ถูกต้อง');
        }
      }, (err: any) => {
        this.isLoading = false;
        this.alert.sweetError('รหัสผ่านเดิมไม่ถูกต้อง');
        console.log(err);
      });
    }
    
  }

}
