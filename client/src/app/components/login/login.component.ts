import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  isLoading = false;

  loginForm = this.fb.group({
    username: [null, Validators.required],
    password: [null, Validators.required]
  });

  constructor(
    private authService: AuthService,
    private alert: AlertService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.loginForm = new FormGroup({
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      pinCode: new FormControl(null),
    });
  }

  login(): void {
    this.isLoading = true;
    if (!this.loginForm!.valid) {
      this.isLoading = false;
      return;
    }

    this.model = Object.assign({}, this.loginForm.value);

    console.log(this.model);

    this.authService.login(this.model).subscribe((next: any) => {
      this.model = {
        username: '',
        password: '',
        pinCode: ''
      };
      this.alert.success('Logged in successfully');
      this.isLoading = false;
    }, () => {
      this.model.password = '';
      this.loginForm.get('password')?.setValue('');
      this.alert.error('Failed to login');
      this.isLoading = false;
    });
  }

}
