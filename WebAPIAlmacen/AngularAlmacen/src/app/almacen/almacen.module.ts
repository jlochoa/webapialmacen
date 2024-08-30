import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { AlmacenComponent } from './almacen.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { ProductosComponent } from './productos/productos.component';
import { AlmacenRoutingModule } from './almacen-routing.module';
import { AlmacenService } from './almacen.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [AlmacenComponent, CategoriasComponent, ProductosComponent],
  imports: [
    SharedModule,
    CommonModule,
    AlmacenRoutingModule,
    FormsModule,
    DialogModule,
    TableModule,
    ButtonModule,
    ConfirmDialogModule
  ],
  providers: [AlmacenService]
})
export class AlmacenModule {}
