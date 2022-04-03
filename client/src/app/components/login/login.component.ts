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
      confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(8)]),
      prefixName: new FormControl(null, Validators.required),
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      addressHouse: new FormControl(null, Validators.required),
      mooHouse: new FormControl(null, Validators.required),
      soiHouse: new FormControl(null),
      roadHouse: new FormControl(null),
      districtHouse: new FormControl(null, Validators.required),
      amphurHouse: new FormControl(null, Validators.required),
      provinceHouse: new FormControl(null, Validators.required),
      zipCodeHouse: new FormControl(null, Validators.required),
      addressContact: new FormControl(null, Validators.required),
      mooContact: new FormControl(null, Validators.required),
      soiContact: new FormControl(null),
      roadContact: new FormControl(null),
      districtContact: new FormControl(null, Validators.required),
      amphurContact: new FormControl(null, Validators.required),
      provinceContact: new FormControl(null, Validators.required),
      zipCodeContact: new FormControl(null, Validators.required),
      mobileNumber: new FormControl(null, Validators.required),
      phoneNumber: new FormControl(null),
      email: new FormControl(null, Validators.required),
      educateDegree: new FormControl(null, Validators.required),
      occupation: new FormControl(null, Validators.required),
      workPlace: new FormControl(null, Validators.required),
      addressWork: new FormControl(null, Validators.required),
      mooWork: new FormControl(null, Validators.required),
      soiWork: new FormControl(null),
      roadWork: new FormControl(null),
      districtWork: new FormControl(null, Validators.required),
      amphurWork: new FormControl(null, Validators.required),
      provinceWork: new FormControl(null, Validators.required),
      zipCodeWork: new FormControl(null, Validators.required),
      emailWork: new FormControl(null, Validators.required),
      phoneWork: new FormControl(null),
      majority: new FormControl(null),
      domicile: new FormControl(null),
      bankrupt: new FormControl(null),
      insane: new FormControl(null),
      imprisonment: new FormControl(null),
      revoke: new FormControl(null),
      registration: new FormControl(null),
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
