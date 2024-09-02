import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthGuard } from '../guards/auth-guard.service';
import { environment } from 'src/environments/environment';
import { IFamilia, IProducto } from './almacen.interfaces';

@Injectable({
  providedIn: 'root'
})
export class AlmacenService {
  urlAPI = environment.urlAPI;
  constructor(private http: HttpClient, private authGuard: AuthGuard) {}

  getFamilias(): Observable<IFamilia[]> {
    const headers = this.getHeaders();
    return this.http.get<IFamilia[]>(`${this.urlAPI}familias`, { headers });
  }

  addFamilia(familia: IFamilia): Observable<IFamilia> {
    const headers = this.getHeaders();
    return this.http.post<IFamilia>(`${this.urlAPI}familias`, familia, { headers });
  }

  updateFamilia(familia: IFamilia): Observable<IFamilia> {
    const headers = this.getHeaders();
    return this.http.put<IFamilia>(`${this.urlAPI}familias`, familia, {
      headers
    });
  }

  deleteFamilia(id: number): Observable<IFamilia> {
    const headers = this.getHeaders();
    return this.http.delete<IFamilia>(`${this.urlAPI}familias/${id}`, {
      headers
    });
  }

  getProductos(): Observable<IProducto[]> {
    const headers = this.getHeaders();
    return this.http.get<IProducto[]>(`${this.urlAPI}Productos`, { headers });
  }

  addProducto(producto: IProducto): Observable<IProducto> {
    const headers = this.getHeaders();
    const formData = new FormData();
    formData.append('nombre', producto.nombre);
    formData.append('precio', producto.precio.toString());
    formData.append('familiaId', producto.familiaId?.toString()!);
    formData.append('descatalogado', producto.descatalogado ? 'true' : 'false');
    formData.append('foto', producto.foto!);

    return this.http.post<IProducto>(`${this.urlAPI}Productos`, formData, { headers });
  }

  deleteProducto(id: number): Observable<IProducto> {
    const headers = this.getHeaders();
    return this.http.delete<IProducto>(`${this.urlAPI}Productos/${id}`, {
      headers
    });
  }

  getHeaders(): HttpHeaders {
    const token = this.authGuard.getToken();
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    return headers;
  }
}
