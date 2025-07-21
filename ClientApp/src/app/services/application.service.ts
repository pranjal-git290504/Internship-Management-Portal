import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Application, PaginatedResult } from '../models';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {
  private apiUrl = 'http://localhost:5056/api';
  constructor(private httpClient: HttpClient) { }

  getAllApplications(pageNumber: number, pageSize: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return this.httpClient.get<PaginatedResult<Application>>(this.apiUrl + '/Application/GetAll', { observe: 'response', params }).pipe(
      map(response => {
        const data = response.body?.data;
        const totalCount = response.body?.totalCount;
        return new PaginatedResult<Application>(data, totalCount, pageNumber, pageSize);
      })
    );
  }
}
