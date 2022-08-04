import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  jwtHelper = new JwtHelperService();
  baseUrl = environment.apiUrl;
  decodeToken: any;

  constructor(
    private http: HttpClient
  ) {}

  request(model: any): any {
    return this.http.post(this.baseUrl + 'data/AddRequest', model);
  }

  getServiceDetail(id: any): any {
    return this.http.get(this.baseUrl + 'data/GetServiceDeatail/' + id);
  }

  updateService(model: any): any {
    return this.http.put(this.baseUrl + 'data/UpdateService', model);
  }

  getServices(page?: any, itemsPerPage?: any, searchKey?: string, searchStatus?: string): Observable<PaginatedResult<any[]>> {
    const paginatedResult: PaginatedResult<any[]> = new PaginatedResult<any[]>();
    let params = new HttpParams();

    if (searchKey) params = params.append('searchKey', searchKey);

    if (searchStatus) params = params.append('searchStatus', searchStatus);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<any[]>(this.baseUrl + 'Data/GetServices', { observe: 'response', params })
      .pipe(
        map((response: any) => {
          // console.log(response.body);
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getAllServices(page?: any, itemsPerPage?: any, searchKey?: string, searchStatus?: string): Observable<PaginatedResult<any[]>> {
    const paginatedResult: PaginatedResult<any[]> = new PaginatedResult<any[]>();
    let params = new HttpParams();

    if (searchKey) params = params.append('searchKey', searchKey);

    if (searchStatus) params = params.append('searchStatus', searchStatus);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<any[]>(this.baseUrl + 'Data/GetAllServices', { observe: 'response', params })
      .pipe(
        map((response: any) => {
          // console.log(response.body);
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getReportServices(page?: any, itemsPerPage?: any, searchKey?: string, searchStatus?: string): Observable<PaginatedResult<any[]>> {
    const paginatedResult: PaginatedResult<any[]> = new PaginatedResult<any[]>();
    let params = new HttpParams();

    if (searchKey) params = params.append('searchKey', searchKey);

    if (searchStatus) params = params.append('searchStatus', searchStatus);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<any[]>(this.baseUrl + 'Data/GetReportServices', { observe: 'response', params })
      .pipe(
        map((response: any) => {
          // console.log(response.body);
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
          }
          return paginatedResult;
        })
      );
  }

  downloadFile(id: number, fileName: string): any {
    return this.http.get(this.baseUrl + 'data/DownloadFile/' + id + '/' + fileName);
  }

  delFileService(fileId: number): any {
    return this.http.delete(this.baseUrl + 'data/delFile/' + fileId);
  }

  thaiDateTime(enDate: Date): any {
    enDate = new Date(enDate);
    const thDate = enDate.toLocaleDateString('th-TH', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: 'numeric',
      minute: 'numeric'
    });
    return thDate;
  }

  thaiDate(enDate: Date): any {
    if (!enDate) { return '-'; }
    enDate = new Date(enDate);
    const thDate = enDate.toLocaleDateString('th-TH', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
    return thDate;
  }

  thaiShortDate(enDate: Date): any {
    enDate = new Date(enDate);
    const thDate = enDate.toLocaleDateString('th-TH', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
    return thDate;
  }

}
