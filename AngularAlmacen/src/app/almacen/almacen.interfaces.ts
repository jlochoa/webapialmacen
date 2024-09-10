export interface IFamilia {
  id: number;
  nombre: string;
}

export interface IProducto {
  id: number;
  nombre: string;
  precio: number;
  descatalogado: boolean;
  foto?: File | null;
  fotoUrl?: string;
  familiaId: number | null;
  familia?: { nombre: string };
}
