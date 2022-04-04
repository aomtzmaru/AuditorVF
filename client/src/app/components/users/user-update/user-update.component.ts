import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { user } from 'src/app/_models/user';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {
  registerForm!: FormGroup;
  model: any;
  isLoading = false;
  user!: user;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private alert: AlertService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.checkAuthorize();
    this.initForm();
    this.route.params.subscribe(data => {
      if (data.id === undefined) {
        this.alert.error('ไม่สามารถเชื่อมต่อข้อมูลได้');
        this.router.navigate(['user/list']);
      } else {
        this.getUserDetail(data.id);
      }
    });
  }

  initForm(): void {
    this.registerForm = new FormGroup({
      perId: new FormControl(null, [Validators.required, Validators.minLength(13), Validators.maxLength(13)]),
      password: new FormControl(null, [Validators.minLength(8)]),
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
      workPlace: new FormControl(null),
      addressWork: new FormControl(null),
      mooWork: new FormControl(null),
      soiWork: new FormControl(null),
      roadWork: new FormControl(null),
      districtWork: new FormControl(null),
      amphurWork: new FormControl(null),
      provinceWork: new FormControl(null),
      zipCodeWork: new FormControl(null),
      emailWork: new FormControl(null),
      phoneWork: new FormControl(null),
    });
  }

  getUserDetail(id: any) {
    this.authService.getUserDetail(id).subscribe((res: any) => {
      console.log(res);
      this.user = res;
      this.registerForm.patchValue(res);
    }, (err: any) => {
      console.log(err);
    });
  }

  checkAuthorize(): void {
    if (this.authService.decodeToken.role !== 'admin')
      this.router.navigate(['home']);
  }

  update(): void {
    if (!this.registerForm.valid) return;
    this.isLoading = true;
    this.model = Object.assign({}, this.registerForm!.value);
    this.model.username = this.user.username;
    this.model.role = this.user.role;
    // console.log(this.model);
    this.authService.update(this.model).subscribe((res: any) => {
      this.alert.success('แก้ไขข้อมูลสำเร็จ');
      this.router.navigate(['user/list']);
    }, (err: any) => {
      this.alert.error('แก้ไขข้อมูลไม่สำเร็จ ' + err.error.text);
      this.isLoading = false;
    });
  }

}
