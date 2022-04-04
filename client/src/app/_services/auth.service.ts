import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { user } from '../_models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  baseUrl = environment.apiUrl;
  decodeToken: any;

  constructor(
    private http: HttpClient
  ) {
    const token = localStorage.getItem('token');
    this.decodeToken = token && this.jwtHelper.decodeToken(token);
  }

  loggedIn(): any {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token!);
  }

  login(model: any): any {
    return this.http.post(this.baseUrl + 'auth/login', model).pipe(
      map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.token);
          this.decodeToken = this.jwtHelper.decodeToken(response.token);
        }
      })
    );
  }

  register(model: any): any {
    return this.http.post(this.baseUrl + 'auth/register', model);
  }

  decodeTokenData(): any {
    const token = localStorage.getItem('token');
    this.decodeToken = this.jwtHelper.decodeToken(token!);
    return this.decodeToken;
  }

  getRole(): any {
    return (this.decodeToken.role[0]);
  }

  logout(): any {
    localStorage.removeItem('token');
    this.decodeToken = null;
  }

  getUserDetail(username: any): any {
    return this.http.get(this.baseUrl + 'auth/GetUserDetail/' + username);
  }

  getUserList(page?: any, itemsPerPage?: any, searchKey?: string, searchStatus?: string): Observable<PaginatedResult<user[]>> {
    const paginatedResult: PaginatedResult<user[]> = new PaginatedResult<user[]>();
    let params = new HttpParams();

    if (searchKey) params = params.append('searchKey', searchKey);

    if (searchStatus) params = params.append('searchStatus', searchStatus);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<user[]>(this.baseUrl + 'auth/GetUserList', { observe: 'response', params })
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

  changePassword(user: any): any {
    // console.log(user);
    return this.http.post(this.baseUrl + 'auth/ChangePassword', user);
  }

  update(model: any): any {
    // console.log(model);
    return this.http.put(this.baseUrl + 'auth/updateUser', model);
  }

}
