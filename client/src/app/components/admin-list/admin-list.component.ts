import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AlertService } from 'src/app/_services/alert.service';
import { AuthService } from 'src/app/_services/auth.service';
import { DataService } from 'src/app/_services/data.service';

@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.css']
})
export class AdminListComponent implements OnInit {

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
      itemsPerPage: 10,
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

  getUserServices() {
    this.isLoading = true;
    this.dataService.getAllServices(
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
          console.log(this.servicesListFilter);
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
