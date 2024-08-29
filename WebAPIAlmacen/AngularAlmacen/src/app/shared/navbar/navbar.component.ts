import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth-guard.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  userName = '';
  constructor(private authGuard: AuthGuard, private router: Router) {}

  ngOnInit(): void {
    this.userName = this.authGuard.getUser();
  }

  cerrarSesion() {
    localStorage.removeItem('usuario');
    this.router.navigateByUrl('/login');
  }
}
