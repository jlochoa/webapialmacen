import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthGuard } from 'src/app/guards/auth-guard.service';
import { ILogin, ILoginResponse } from 'src/app/interfaces/login.interface';
import { AppService } from 'src/app/services/app.service';
import { SignalrService } from 'src/app/services/signalr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [MessageService]
})
export class LoginComponent implements OnInit {
  infoLogin: ILogin = {
    email: '',
    password: ''
  };

  constructor(
    private router: Router,
    private appService: AppService,
    private messageService: MessageService,
    private authGuard: AuthGuard,
    private singalRService: SignalrService
  ) {}

  ngOnInit() {
    if (this.authGuard.isLoggedIn()) {
      this.router.navigateByUrl('almacen/categorias');
    }
  }

  login() {
    this.appService.login(this.infoLogin).subscribe({
      next: (data) => {
        console.log(data);
        localStorage.setItem('usuario', JSON.stringify(data));
        //  this.router.navigateByUrl('almacen');
        this.singalRService.connect();
        this.router.navigate([`/almacen`], { replaceUrl: true });
      },
      error: (err) => {
        console.log(err);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Credenciales erróneas' });
      }
    });
  }
}
