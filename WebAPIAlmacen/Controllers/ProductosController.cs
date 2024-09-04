using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.DTOs;
using WebAPIAlmacen.Models;
using WebAPIAlmacen.Services;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/productos")]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly MiAlmacenContext context;
        private readonly OperacionesService operacionesService;
        private readonly IGestorArchivos gestorArchivosLocal;
        private readonly CalculosService calculosService;

        public ProductosController(MiAlmacenContext context,OperacionesService operacionesService, IGestorArchivos gestorArchivosLocal, CalculosService calculosService)
        {
            this.context = context;
            this.operacionesService = operacionesService;
            this.gestorArchivosLocal = gestorArchivosLocal;
            this.calculosService = calculosService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProductos()
        {
            var productos = await context.Productos.Select(x => new DTOProducto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Precio = x.Precio,
                Descatalogado = x.Descatalogado,
                FotoUrl = x.FotoUrl,
                FamiliaId = x.FamiliaId,
                Familia = x.Familia.Nombre
            }).ToListAsync();

            return Ok(productos);
        }

        [HttpGet("basica")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProductosBasica()
        {
            var productos = await context.Productos.ToListAsync();
            await operacionesService.AddOperacion("Get", "Productos");
            return Ok(productos);
        }

        [HttpGet("productosagrupadospordescatalogado")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProductosAgrupadosPorDescatalogado()
        {
            var productos = await context.Productos.GroupBy(g => g.Descatalogado)
                .Select(x => new
                {
                    Descatalogado = x.Key,
                    Total = x.Count(),
                    Productos = x.ToList()
                }).ToListAsync();

            return Ok(productos);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult> GetFiltroMultiple([FromQuery] DTOProductosFiltro filtroProductos)
        {
            // AsQueryable nos permite ir construyendo paso a paso el filtrado y ejecutarlo al final.
            // Si lo convertimos a una lista (toListAsync) el resto de filtros los hacemos en memoria
            // porque toListAsync ya trae a la memoria del servidor los datos desde el servidor de base de datos
            // Hacer los filtros en memoria es menos eficiente que hacerlos en una base de datos.
            // Construimos los filtros de forma dinámica y hasta que no hacemos el ToListAsync no vamos a la base de datos
            // para traer la información
            var productosQueryable = context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(filtroProductos.Nombre))
            {
                productosQueryable = productosQueryable.Where(x => x.Nombre.Contains(filtroProductos.Nombre));
            }

            if (filtroProductos.Descatalogado)
            {
                productosQueryable = productosQueryable.Where(x => x.Descatalogado);
            }

            if (filtroProductos.FamiliaId != 0)
            {
                productosQueryable = productosQueryable.Where(x => x.FamiliaId == filtroProductos.FamiliaId);
            }

            var productos = await productosQueryable.ToListAsync();

            return Ok(productos);
        }

        [HttpPost]
        public async Task<ActionResult> PostProductos([FromForm] DTOProductoAgregar producto)
        {
            Producto newProducto = new Producto
            {
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Descatalogado = false,
                FechaAlta = DateOnly.FromDateTime(DateTime.Now),
                FamiliaId = producto.FamiliaId,
                FotoUrl = ""
            };

            if (producto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Extraemos la imagen de la petición
                    await producto.Foto.CopyToAsync(memoryStream);
                    // La convertimos a un array de bytes que es lo que necesita el método de guardar
                    var contenido = memoryStream.ToArray();
                    // La extensión la necesitamos para guardar el archivo
                    var extension = Path.GetExtension(producto.Foto.FileName);
                    // Recibimos el nombre del archivo
                    // El servicio Transient GestorArchivosLocal instancia el servicio y cuando se deja de usar se destruye
                    newProducto.FotoUrl = await gestorArchivosLocal.GuardarArchivo(contenido, extension, "imagenes",
                        producto.Foto.ContentType);
                }
            }

            await context.AddAsync(newProducto);
            await context.SaveChangesAsync();
            return Ok(newProducto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductos([FromRoute] int id)
        {
            var producto = await context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            await gestorArchivosLocal.BorrarArchivo(producto.FotoUrl, "imagenes");
            context.Remove(producto);
            await context.SaveChangesAsync();
            return Ok();
        }
    }

}

