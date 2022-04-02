import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private toastr: ToastrService) { }

  success(message: string): any {
    this.toastr.success(message);
  }

  error(message: string): any {
    this.toastr.error(message);
  }

  warning(message: string): any {
    this.toastr.warning(message);
  }

  info(message: string): any {
    this.toastr.info(message);
  }

  sweetSuccess(title: string, body: string): any {
    Swal.fire({
      title,
      html: body,
      icon: 'success',
      confirmButtonColor: '#4e9682',
      confirmButtonText: 'ตกลง'
    });
  }

  sweetSuccessAutoClose(message: string): any {
    Swal.fire({
      // position: 'top-end',
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 1500
    });
  }

  sweetError(message: string): any {
    Swal.fire({
      icon: 'error',
      title: 'พบข้อผิดพลาด',
      text: message,
      confirmButtonColor: '#4e9682',
      confirmButtonText: 'รับทราบ'
    });
  }

  sweetWarning(message: string): any {
    Swal.fire({
      icon: 'warning',
      title: 'ข้อความแจ้งเตือน',
      text: message,
      confirmButtonColor: '#4e9682',
      confirmButtonText: 'รับทราบ'
    });
  }

  sweetInfo(message: string): any {
    Swal.fire({
      icon: 'info',
      title: 'โปรดทราบ',
      text: message,
      confirmButtonColor: '#4e9682',
      confirmButtonText: 'รับทราบ'
    });
  }

  confirm(question: string, warning: string): any {
    return Swal.fire({
      title: question,
      text: warning,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'ยืนยัน',
      cancelButtonText: 'ยกเลิก'
    });
  }

}
