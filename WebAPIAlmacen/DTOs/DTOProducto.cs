namespace WebAPIAlmacen.DTOs
{
    public class DTOProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public bool Descatalogado { get; set; }
        public string? FotoUrl { get; set; }
        public int FamiliaId { get; set; }
        public string Familia { get; set; }
    }
}
