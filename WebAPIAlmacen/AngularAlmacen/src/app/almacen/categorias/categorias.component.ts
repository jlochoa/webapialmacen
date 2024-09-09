import { Component, OnInit, ViewChild } from '@angular/core';
import { AlmacenService } from '../almacen.service';
import { NgForm } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { IFamilia } from '../almacen.interfaces';
import { SignalrService } from 'src/app/services/signalr.service';
import { AuthGuard } from 'src/app/guards/auth-guard.service';

@Component({
  selector: 'app-categorias',
  templateUrl: './categorias.component.html',
  styleUrls: ['./categorias.component.css'],
  providers: [ConfirmationService, MessageService]
})
export class CategoriasComponent implements OnInit {
  constructor(
    private almacenService: AlmacenService,
    private confirmationService: ConfirmationService,
    private signalrService: SignalrService,
    private messageService: MessageService,
    private authGuard: AuthGuard
  ) {}

  @ViewChild('formulario') formulario!: NgForm;
  visibleError = false;
  mensajeError = '';
  familias: IFamilia[] = [];
  visibleConfirm = false;

  familia: IFamilia = {
    id: 0,
    nombre: ''
  };

  ngOnInit(): void {
    this.getMessages();
    this.getFamilias();
  }

  getMessages() {
    this.signalrService.messageSubscription.subscribe({
      next: (message) => {
        this.messageService.add({ severity: 'info', summary: 'Alerta', detail: message });
        this.getFamilias();
      }
    });
  }

  getFamilias() {
    this.almacenService.getFamilias().subscribe({
      next: (data) => {
        console.log(data);
        this.visibleError = false;
        this.familias = data;
      },
      error: (err) => {
        this.controlarError(err);
      }
    });
  }

  guardar() {
    if (this.familia.id === 0) {
      this.almacenService.addFamilia(this.familia).subscribe({
        next: (data) => {
          this.visibleError = false;
          this.formulario.reset();
          this.getFamilias();
          this.signalrService.sendMessage(this.authGuard.getUser() + ' ha agregado la familia ' + this.familia.nombre);
        },
        error: (err) => {
          this.controlarError(err);
        }
      });
    } else {
      this.almacenService.updateFamilia(this.familia).subscribe({
        next: (data) => {
          this.visibleError = false;
          this.cancelarEdicion();
          this.formulario.reset();
          this.getFamilias();
        },
        error: (err) => {
          this.controlarError(err);
        }
      });
    }
  }

  edit(familia: IFamilia) {
    this.familia = { ...familia };
  }

  cancelarEdicion() {
    this.familia = {
      id: 0,
      nombre: ''
    };
  }

  confirmDelete(familia: IFamilia) {
    this.confirmationService.confirm({
      message: `Eliminar la categoría ${familia.nombre}?`,
      header: '¿Estás seguro?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Sí',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.deleteFamilia(familia.id!)
    });
  }

  deleteFamilia(id: number) {
    this.almacenService.deleteFamilia(id).subscribe({
      next: (data) => {
        this.visibleError = false;
        this.formulario.reset({
          nombreFamilia: ''
        });
        this.getFamilias();
      },
      error: (err) => {
        this.controlarError(err);
      }
    });
  }

  controlarError(err: any) {
    this.visibleError = true;
    if (err.error && typeof err.error === 'object' && err.error.message) {
      this.mensajeError = err.error.message;
    } else if (typeof err.error === 'string') {
      // Si `err.error` es un string, se asume que es el mensaje de error
      this.mensajeError = err.error;
    } else {
      // Maneja el caso en el que no se recibe un mensaje de error útil
      this.mensajeError = 'Se ha producido un error inesperado';
    }
  }
}
