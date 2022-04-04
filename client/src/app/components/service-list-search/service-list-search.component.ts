import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { UserSearch } from 'src/app/_models/userSearch';

@Component({
  selector: 'app-service-list-search',
  templateUrl: './service-list-search.component.html',
  styleUrls: ['./service-list-search.component.css']
})
export class ServiceListSearchComponent implements OnInit {

  @Output() searchParams = new EventEmitter();
  @Output() clearValue = new EventEmitter();

  isLoading = false;
  searchForm = this.fb.group({
    searchKey: [''],
    searchStatus: ['สถานะทั้งหมด']
  });
  txtStatus = 'สถานะทั้งหมด';
  icoStatus = 'fa-filter';
  btnStatus = 'primary';
  
  searchKey: string = '';
  searchData: UserSearch = {searchKey: this.searchKey, searchStatus: this.txtStatus};

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }


  search() {
    this.searchKey = this.searchForm!.get('searchKey')?.value;
    this.searchData.searchKey = this.searchKey;
    this.searchData.searchStatus = this.txtStatus;
    this.searchParams.emit(this.searchData);
  }

  clear() {
    this.searchForm.reset();
    this.txtStatus = 'สถานะทั้งหมด';
    this.icoStatus = 'fa-filter';
    this.btnStatus = 'primary';
    this.search();
  }

  statusFilter(status: string): void {
    this.txtStatus = status;
    this.icoStatus = 'fa-comment-dots';
    this.btnStatus = 'primary';
    if (status === 'อยู่ระหว่างดำเนินการ') {
      this.icoStatus = 'fa-comment-dots';
      this.btnStatus = 'danger';
    }
    if (status === 'รับไว้ดำเนินการ') {
      this.icoStatus = 'fa-briefcase';
      this.btnStatus = 'warning';
    }
    if (status === 'ดำเนินการเสร็จ') {
      this.icoStatus = 'fa-check-circle';
      this.btnStatus = 'success';
    }
  }

}
