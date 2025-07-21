import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Internship, PaginatedResult, ApiResponse } from '../models';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InternshipService {
  private apiUrl = 'http://localhost:5056/api';
  constructor(private httpClient: HttpClient) { }

  getAll(pageNumber: number, pageSize: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return this.httpClient.get<PaginatedResult<Internship>>(this.apiUrl + '/Internship/GetAll', { observe: 'response', params }).pipe(
      map(response => {
        const data = response.body?.data;
        const totalCount = response.body?.totalCount;
        return new PaginatedResult<Internship>(data, totalCount, pageNumber, pageSize);
      })
    );
  }

  upsert(internship: Internship) {
    return this.httpClient.post<ApiResponse<boolean>>(this.apiUrl + '/Internship/Upsert', internship).pipe(
      map(response => {
        return new ApiResponse<boolean>(response);
      })
    );
  }

  remove(id: number) {
    return this.httpClient.delete<ApiResponse<boolean>>(this.apiUrl + `/Internship/Remove/${id}`).pipe(
      map(response => {
        return new ApiResponse<boolean>(response);
      })
    );
  }

}
