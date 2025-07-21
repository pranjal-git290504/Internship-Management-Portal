import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse, User } from '../models';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5056/api';
  constructor(private httpClient: HttpClient) { }

  getById(id: number) {
    return this.httpClient.get<ApiResponse<User>>(this.apiUrl + `/User/GetByUserId/${id}`).pipe(
      map(response => {
        return new ApiResponse<User>(response);
      })
    );
  }
}
