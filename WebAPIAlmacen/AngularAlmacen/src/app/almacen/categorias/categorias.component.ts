import { Component, OnInit, ViewChild } from '@angular/core';
import { AlmacenService } from '../almacen.service';
import { NgForm } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { IFamilia } from '../almacen.interfaces';

@Component({
  selector: 'app-categorias',
  templateUrl: './categorias.component.html',
  styleUrls: ['./categorias.component.css'],
  providers: [ConfirmationService]
})
export class CategoriasComponent implements OnInit {
  constructor(private almacenService: AlmacenService, private confirmationService: ConfirmationService) {}
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
    this.getFamilias();
  }

  getFamilias() {
    this.almacenService.getFamilias().subscribe({
      next: (data) => {
        console.log(data);
        this.visibleError = false;
        this.familias = data;
      },
      error: (err) => {
        this.visibleError = true;
        this.mensajeError = 'Se ha producido un erro en la carga de familias';
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
        },
        error: (err) => {
          console.log(err);
          this.visibleError = true;
          this.mensajeError = err.error.msg;
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
          this.visibleError = true;
          this.mensajeError = err.error.msg;
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
      header: 'Estás seguro?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Sí',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.deleteFamilia(familia.id)
    });
  }

  deleteFamilia(id: number) {
    this.almacenService.deleteFamilia(id).subscribe({
      next: (data) => {
        this.visibleError = false;
        this.formulario.reset({
          nombre: ''
        });
        this.getFamilias();
      },
      error: (err) => {
        this.visibleError = true;
        this.mensajeError = 'Se ha producido un error';
      }
    });
  }
}
