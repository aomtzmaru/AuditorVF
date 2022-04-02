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
  mode = 'register';

  loginForm!: FormGroup;
  registerForm!: FormGroup;

  constructor(
    private authService: AuthService,
    private alert: AlertService
  ) { }

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.loginForm = new FormGroup({
      username: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required)
    });

    this.registerForm = new FormGroup({
      perId: new FormControl(null, [Validators.required, Validators.minLength(13)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(8)]),
      prefixName: new FormControl(null, Validators.required),
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      addressHouse: new FormControl(null, Validators.required),
      addressContact: new FormControl(null, Validators.required),
    });
  }

  changeMode() {
    this.mode = this.mode === 'login' ? this.mode = 'register' : this.mode = 'login';
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

  register() {

  }

}
