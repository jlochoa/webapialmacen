using WebAPIAlmacen.Validators;

namespace WebAPIAlmacen.DTOs
{
    public class DTOProductoAgregar
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
        public int FamiliaId { get; set; }

    }
}
