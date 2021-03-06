import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestFile } from 'src/app/_models/requestFile';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';
import { DataService } from 'src/app/_services/data.service';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {
  serviceName = '';
  requestForm!: FormGroup;
  isLoading = false;
  model: any = {};
  requestFile!: RequestFile[];

  constructor(private route: ActivatedRoute, private dataService: DataService, private alert: AlertService, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.route.params.subscribe((res: any) => {
      if (res.service === 'renew') {
        this.serviceName = 'ขอต่อใบอนุญาต';
      } else if (res.service === 'new') {
        this.serviceName = 'ขอรับบัตรใหม่';
      } else if (res.service === 'replace') {
        this.serviceName = 'ออกใบแทนใบอนุญาต';
      }
    });

    this.initForm();
    this.getUserDetail();
  }

  initForm() {
    this.requestForm = new FormGroup({
      perId: new FormControl(null, [Validators.required, Validators.minLength(13), Validators.maxLength(13)]),
      prefixName: new FormControl(null, Validators.required),
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      mobileNumber: new FormControl(null, Validators.required),
      phoneNumber: new FormControl(null),
      email: new FormControl(null, Validators.required),
      regNumber: new FormControl(null, Validators.required),
      recieveDoc: new FormControl('รับด้วยตนเอง', Validators.required),
      recieveBranch: new FormControl('สทบ. ส่วนกลาง'),
      addressContact: new FormControl(null),
      mooContact: new FormControl(null),
      soiContact: new FormControl(null),
      roadContact: new FormControl(null),
      districtContact: new FormControl(null),
      amphurContact: new FormControl(null),
      provinceContact: new FormControl(null),
      zipCodeContact: new FormControl(null),
    });
  }

  getUserDetail() {
    this.authService.getUserDetail(this.authService.decodeToken.nameid).subscribe((res: any) => {
      // console.log(res);
      this.requestForm.patchValue(res);
    }, (err: any) => {
      console.log(err);
    });
  }

  getRequestFiles(event: any): void{
    this.requestFile = event;
  }

  request() {
    this.isLoading = true;
    if (!this.requestForm!.valid) {
      this.isLoading = false;
      this.alert.sweetError('กรุณากรอกข้อมูลให้ครบถ้วนและถูกต้อง');
      return;
    }

    this.model = Object.assign({}, this.requestForm.value);
    this.model.serviceType = this.serviceName;
    this.model.files = this.requestFile;

    console.log(this.model);

    this.dataService.request(this.model).subscribe((res: any) => {
      console.log(res);
      this.alert.sweetSuccess("บันทึกข้อมูลสำเร็จ", "ข้อมูลคำขอของท่านถูกส่งไปยังคณะกรรมการฯ เรียบร้อยแล้ว ท่านสามารถตรวจสอบสถานะคำขอได้ที่เมนู 'รายการคำร้องขอ'");
      this.isLoading = false;
      this.router.navigate(['list']);
    }, (err: any) => {
      console.log(err);
      this.alert.error(err.message);
      this.isLoading = false;
    });
  }
}
