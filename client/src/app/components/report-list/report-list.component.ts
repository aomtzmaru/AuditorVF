import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';
import { DataService } from 'src/app/_services/data.service';
import { environment } from 'src/environments/environment';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css']
})
export class ReportListComponent implements OnInit {
  @ViewChild('table', { static: false }) TABLE: ElementRef| undefined;
  fileUrl = environment.fileUrl;
  servicesList: any;
  pagination?: Pagination;
  servicesListFilter?: any;
  isLoading = true;
  searchKey?: string;
  searchStatus?: string;

  constructor(
    public dataService: DataService,
    private authService: AuthService,
    private alert: AlertService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10000,
      totalItems: 0,
      totalPages: 0
    }

    this.getUserServices();
  }

  searchClick(event: any) {
    this.searchKey = event.searchKey;
    this.searchStatus = event.searchStatus;
    this.pagination!.currentPage = 1;
    this.getUserServices();
  }

  downloadFile(id: number, fileName: string): void {
    this.dataService.downloadFile(id, fileName).subscribe((res: any) => {
      window.open(this.fileUrl + res.file);
    });
    console.log('downloaded');
  }

  excelExport() {
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.TABLE!.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, 'รายงานคำร้องขอ.xlsx');
  }

  getUserServices() {
    this.isLoading = true;
    this.dataService.getReportServices(
      this.pagination?.currentPage,
      this.pagination?.itemsPerPage,
      this.searchKey,
      this.searchStatus
    )
      .subscribe(
        (res: PaginatedResult<any[]>) => {
          this.servicesList = res.result;
          this.servicesListFilter = res.result;
          this.pagination = res.pagination;
          this.isLoading = false;
          // console.log(this.servicesListFilter);
        }, (error) => {
          console.log(error);
          this.isLoading = false;
        }
      );
  }

  pageChanged(event: any): void {
    this.pagination!.currentPage = event.page;
    this.getUserServices();
    this.isLoading = false;
  }


}
