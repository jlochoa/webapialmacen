import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ILoginResponse } from '../interfaces/login.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(private router: Router) {}

  isLoggedIn() {
    const user = localStorage.getItem('usuario');
    if (user) {
      return true;
    }

    this.router.navigate(['login']);
    return false;
  }

  getUser(): string {
    const infoUser = localStorage.getItem('usuario');
    if (infoUser) {
      const userInfo: ILoginResponse = JSON.parse(infoUser);
      return userInfo.email;
    }
    return '';
  }

  getToken(): string {
    const infoUser = localStorage.getItem('usuario');
    if (infoUser) {
      const userInfo: ILoginResponse = JSON.parse(infoUser);
      return userInfo.token;
    }
    return '';
  }
}
