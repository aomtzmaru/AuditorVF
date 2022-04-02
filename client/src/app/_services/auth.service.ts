import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { HttpClient } from '@angular/common/http';

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

  update(model: any): any {
    return this.http.put(this.baseUrl + 'auth/updateUser', model);
  }

  getUserById(id: any): any {
    return this.http.get(this.baseUrl + 'auth/getuser/' + id);
  }

  // getExtUserList(page?: any, itemsPerPage?: any, searchKey?: string, searchStatus?: string): Observable<PaginatedResult<extUser[]>> {
  //   const paginatedResult: PaginatedResult<extUser[]> = new PaginatedResult<extUser[]>();
  //   let params = new HttpParams();

  //   if (searchKey) params = params.append('searchKey', searchKey);

  //   if (searchStatus) params = params.append('searchStatus', searchStatus);

  //   if (page != null && itemsPerPage != null) {
  //     params = params.append('pageNumber', page);
  //     params = params.append('pageSize', itemsPerPage);
  //   }

  //   return this.http
  //     .get<extUser[]>(this.baseUrl + 'auth/GetExtUserList', { observe: 'response', params })
  //     .pipe(
  //       map((response: any) => {
  //         // console.log(response.body);
  //         paginatedResult.result = response.body;
  //         if (response.headers.get('Pagination') != null) {
  //           paginatedResult.pagination = JSON.parse(response.headers.get('pagination'));
  //         }
  //         return paginatedResult;
  //       })
  //     );
  // }

  changePassword(user: any): any {
    // console.log(user);
    return this.http.post(this.baseUrl + 'auth/ChangePassword', user);
  }

}