import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlmacenComponent } from './almacen.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { ProductosComponent } from './productos/productos.component';
import { AlmacenRoutingModule } from './almacen-routing.module';
import { AlmacenService } from './almacen.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [AlmacenComponent, CategoriasComponent, ProductosComponent],
  imports: [CommonModule, AlmacenRoutingModule, SharedModule],
  providers: [AlmacenService]
})
export class AlmacenModule {}
