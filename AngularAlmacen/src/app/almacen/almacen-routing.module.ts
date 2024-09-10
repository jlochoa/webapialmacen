import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoriasComponent } from './categorias/categorias.component';
import { ProductosComponent } from './productos/productos.component';
import { AlmacenComponent } from './almacen.component';

const appRoutes: Routes = [
  {
    path: '',
    component: AlmacenComponent,
    children: [
      { path: '', redirectTo: '/almacen/categorias', pathMatch: 'full' },
      { path: 'categorias', component: CategoriasComponent },
      { path: 'productos', component: ProductosComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(appRoutes)],
  exports: [RouterModule]
})
export class AlmacenRoutingModule {}
