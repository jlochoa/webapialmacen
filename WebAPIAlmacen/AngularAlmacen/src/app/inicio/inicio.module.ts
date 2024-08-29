import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { FormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';

@NgModule({
  declarations: [LoginComponent, NotFoundComponent],
  imports: [CommonModule, FormsModule, ToastModule]
})
export class InicioModule {}
