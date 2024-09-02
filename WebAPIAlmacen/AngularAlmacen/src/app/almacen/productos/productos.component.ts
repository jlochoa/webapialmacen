import { Component, OnInit, ViewChild } from '@angular/core';
import { AlmacenService } from '../almacen.service';
import { ConfirmationService } from 'primeng/api';
import { NgForm } from '@angular/forms';
import { IFamilia, IProducto } from '../almacen.interfaces';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.css'],
  providers: [ConfirmationService]
})
export class ProductosComponent implements OnInit {
  constructor(private almacenService: AlmacenService, private confirmationService: ConfirmationService) {}
  @ViewChild('formulario') formulario!: NgForm;
  visibleError = false;
  mensajeError = '';
  productos: IProducto[] = [];
  categorias: IFamilia[] = [];
  visibleConfirm = false;
  urlImagen = '';
  visibleFoto = false;
  foto = '';

  producto: IProducto = {
    id: 0,
    nombre: '',
    precio: 0,
    foto: null,
    descatalogado: false,
    familiaId: null
  };

  ngOnInit(): void {
    this.getCategorias();
    this.getProductos();
  }

  getCategorias() {
    this.almacenService.getFamilias().subscribe({
      next: (data) => {
        this.visibleError = false;
        this.categorias = data;
      },
      error: (err) => {
        this.visibleError = true;
        this.mensajeError = 'Se ha producido un erro en la carga de familias';
      }
    });
  }

  getProductos() {
    this.almacenService.getProductos().subscribe({
      next: (data) => {
        this.visibleError = false;
        this.productos = data;
        console.log(this.productos);
      },
      error: (err) => {
        this.visibleError = true;
        this.mensajeError = 'Se ha producido un erro en la carga de productos';
      }
    });
  }

  onChange(event: any) {
    const file = event.target.files;

    if (file) {
      this.producto.foto = file[0];
    }
  }

  showImage(producto: IProducto) {
    this.foto = producto.fotoUrl!;
    this.visibleFoto = true;
  }

  guardar() {
    this.almacenService.addProducto(this.producto).subscribe({
      next: (data: any) => {
        this.visibleError = false;
        this.formulario.reset();
        this.getProductos();
      },
      error: (err: any) => {
        this.visibleError = true;
        this.mensajeError = err.error.msg;
      }
    });
  }

  confirmDelete(producto: IProducto) {
    this.confirmationService.confirm({
      message: `Eliminar el producto ${producto.nombre}?`,
      header: 'Estás seguro?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Sí',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => this.deleteProducto(producto.id)
    });
  }

  deleteProducto(id: number) {
    this.almacenService.deleteProducto(id).subscribe({
      next: (data: IProducto) => {
        this.visibleError = false;
        this.getProductos();
      },
      error: (err: any) => {
        this.visibleError = true;
        this.mensajeError = err.error.msg;
      }
    });
  }
}
