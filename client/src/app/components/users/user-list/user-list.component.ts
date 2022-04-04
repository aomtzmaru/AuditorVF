import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { user } from 'src/app/_models/user';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  pagination?: Pagination;
  userList?: user[];
  userListFilter?: user[];
  isLoading = true;
  searchKey?: string;
  searchStatus?: string;


  constructor(
    private authService: AuthService,
    private alert: AlertService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.checkAuthorize();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 0,
      totalPages: 0
    }
    this.loadExtUser();
  }

  checkAuthorize(): void {
    if (this.authService.decodeToken.role !== 'admin')
      this.router.navigate(['home']);
  }

  searchClick(event: any) {
    this.searchKey = event.searchKey;
    this.searchStatus = event.searchStatus;
    this.pagination!.currentPage = 1;
    this.loadExtUser();
  }

  loadExtUser() {
    this.isLoading = true;
    this.authService.getUserList(
      this.pagination?.currentPage,
      this.pagination?.itemsPerPage,
      this.searchKey,
      this.searchStatus
    )
      .subscribe(
        (res: PaginatedResult<user[]>) => {
          this.userList = res.result;
          this.userListFilter = res.result;
          this.pagination = res.pagination;
          this.isLoading = false;
        },
        (error) => {
          this.isLoading = false;
        }
      );
  }

  changeExtUserStatus = async (value: any) => {
    console.log(value);
    value.deleted = 1 - value.deleted;
    this.authService.update(value).subscribe((res: any) => {
      value.deleted === 1 ? this.alert.error('ปิดใช้งานแล้ว') : this.alert.success('เปิดใช้งานแล้ว');
    }, (err: any) => {
      value.deleted = 1 - value.deleted;
      this.alert.error('พบข้อผิดพลาด โปรดลองใหม่อีกครั้ง');
    });
  }

  pageChanged(event: any): void {
    this.pagination!.currentPage = event.page;
    this.loadExtUser();
    this.isLoading = false;
  }

}
