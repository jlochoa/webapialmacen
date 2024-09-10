import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth-guard.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  userName = '';
  constructor(private authGuard: AuthGuard, private router: Router, private signalRService: SignalrService) {}

  ngOnInit(): void {
    this.userName = this.authGuard.getUser();
  }

  cerrarSesion() {
    localStorage.removeItem('usuario');
    this.signalRService.disconnect();
    this.router.navigateByUrl('/login');
  }
}
