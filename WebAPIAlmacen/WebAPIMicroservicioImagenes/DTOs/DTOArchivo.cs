namespace WebAPIMicroservicioImagenes.DTOs
{
    public class DTOArchivo
    {
        public string Nombre { get; set; }
        public byte[] Contenido { get; set; }
        public string Carpeta { get; set; }
        public string ContentType { get; set; }
    }

    public class DTOArchivoEliminar
    {
        public string Url { get; set; }
        public string Carpeta { get; set; }
    }
}
