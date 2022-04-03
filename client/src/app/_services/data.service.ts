import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { HttpClient } from '@angular/common/http';

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

}
