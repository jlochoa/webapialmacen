using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIMicroservicioImagenes.DTOs;
using WebAPIMicroservicioImagenes.Services;

namespace WebAPIMicroservicioImagenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly GestorArchivosLocal _gestorArchivosLocal;

        public ArchivosController(GestorArchivosLocal gestorArchivosLocal)
        {
            _gestorArchivosLocal = gestorArchivosLocal;
        }

        [HttpPost]
        public async Task<ActionResult> PostArchivos([FromBody] DTOArchivo archivo)
        {
            string nombreArchivo;
            var extension = Path.GetExtension(archivo.Nombre);
            nombreArchivo = await _gestorArchivosLocal.GuardarArchivo(archivo.Contenido, extension, archivo.Carpeta, archivo.ContentType);

            return Ok(nombreArchivo);
        }

        [HttpPost("eliminar")]
        public async Task<ActionResult> DeleteArchivos([FromBody] DTOArchivoEliminar archivo)
        {
            await _gestorArchivosLocal.BorrarArchivo(archivo.Url, archivo.Carpeta);
            return Ok();
        }
    }

}
