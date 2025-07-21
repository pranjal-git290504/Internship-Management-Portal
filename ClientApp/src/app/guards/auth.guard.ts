import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AuthService} from "../services/auth.service";

export const authGuard: CanActivateFn = (route, state) => {
  const expectedRole = route.data['role'] as string;
  if (!inject(AuthService).isAuthenticated()) {
    inject(AuthService).logout();
    return false;
  }

  // Validate Role
  if(expectedRole && !inject(AuthService).hasRole(expectedRole)) {
    inject(AuthService).logout();
    return false;
  }
  return true;
};