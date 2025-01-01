import { CanActivateFn } from '@angular/router';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  if (!inject(AuthService).currentUser) {
    inject(Router).navigate([
      '/register-passenger',
      { requestedUrl: state.url }, // Navigate the user to the initially requested path after logging in 
    ]);
  }

  return true;
};
