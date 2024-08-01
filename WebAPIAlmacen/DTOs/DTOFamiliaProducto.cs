namespace WebAPIAlmacen.DTOs
{
    public class DTOFamiliaProducto
    {
        public int IdFamilia { get; set; }
        public string Nombre { get; set; }
        public int TotalProductos { get; set; }
        public List<DTOProductoItem> Productos { get; set; }
    }

    public class DTOProductoItem
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
    }

}
