import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { PaginatedResult, Student } from '../models';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private apiUrl = 'http://localhost:5056/api';
  constructor(private httpClient: HttpClient) { }

  getAllStudents(pageNumber: number, pageSize: number) {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return this.httpClient.get<PaginatedResult<Student>>(this.apiUrl + '/Student/GetAll', { observe: 'response', params }).pipe(
      map(response => {
        const data = response.body?.data;
        const totalCount = response.body?.totalCount;
        return new PaginatedResult<Student>(data, totalCount, pageNumber, pageSize);
      })
    );
  }
}
