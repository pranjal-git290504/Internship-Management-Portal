import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable, DestroyRef, inject } from '@angular/core';
import { Observable, catchError, tap, of } from 'rxjs';
import {LoginResponse } from '../types/login-response.type';
import { LoginSuccess } from '../interfaces';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5056/api';

  constructor(private http: HttpClient, private router: Router) { }

  register(userData: { FirstName: string; LastName: string; Username: string; Email: string; Password: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/User/Create`, userData)
      .pipe(
        catchError(error => {
          console.error('Registration error:', error);
          if (error.status === 400) {
            alert(error.error?.message || 'Invalid registration data');
          } else if (error.status === 0) {
            alert('Cannot connect to the server. Please check if the backend is running.');
          } else {
            alert('An error occurred during registration. Please try again.');
          }
          throw error;
        }),
        tap(() => {
          alert('Registration successful! Please login.');
          this.router.navigate(['/login']);
        })
      );
  }

  login(credentials: { Username: string; Password: string }): Observable<LoginResponse> {
    console.log('Attempting login with username:', credentials.Username);
    return this.http.post<LoginResponse>(`${this.apiUrl}/User/Login`, credentials)
      .pipe(
        catchError(error => {
          console.error('Login error details:', error);
          if (error.status === 401) {
            alert('Invalid username or password');
          } else if (error.status === 0) {
            alert('Cannot connect to the server. Please check if the backend is running.');
          } else if (error.status === 400) {
            alert(error.error?.message || 'Invalid request format');
          } else if (error.status === 500) {
            alert('Server error occurred. Please try again later.');
          } else {
            alert('An unexpected error occurred during login. Please try again.');
          }
          throw error;
        }),
        tap(data => {
          console.log('Login successful');
          const loginSuccessData = data as LoginSuccess;
          this.storeTokens(loginSuccessData);
          this.router.navigate(['/']);
        })
      );
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/User/ForgotPassword`, { email })
      .pipe(
        catchError(error => {
          console.error('Forgot password error:', error);
          if (error.status === 404) {
            alert('Email not found');
          } else if (error.status === 0) {
            alert('Cannot connect to the server. Please check if the backend is running.');
          } else {
            alert('An error occurred. Please try again.');
          }
          throw error;
        })
      );
  }

  resetPassword(token: string, newPassword: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/User/ResetPassword`, { token, newPassword })
      .pipe(
        catchError(error => {
          console.error('Reset password error:', error);
          if (error.status === 400) {
            alert('Invalid or expired reset token');
          } else if (error.status === 0) {
            alert('Cannot connect to the server. Please check if the backend is running.');
          } else {
            alert('An error occurred. Please try again.');
          }
          throw error;
        }),
        tap(() => {
          alert('Password reset successful! Please login with your new password.');
          this.router.navigate(['/login']);
        })
      );
  }

  storeTokens(data: LoginSuccess): void {
    localStorage.setItem('token', data.token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    if (!token) return false;
    
    try {
      const decoded: any = jwtDecode(token);
      const currentTime = Date.now() / 1000;
      return decoded.exp > currentTime;
    } catch {
      return false;
    }
  }

  getUserRole(): string {
    const token = this.getToken();
    if (!token) return '';
    
    const decoded: any = jwtDecode(token);
    return decoded.role;
  }

  getUserId(): number {
    const token = this.getToken();
    if (!token) return 0;

    const decoded: any = jwtDecode(token);
    return decoded.userId;
  }

  hasRole(role: string): boolean {
    const userRole = this.getUserRole();
    return userRole?.toLowerCase() === role?.toLowerCase();
  }
}
