using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.DTOs;
using WebAPIAlmacen.Models;

namespace WebAPIAlmacen.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {

        private readonly MiAlmacenContext context;

        public ProductosController(MiAlmacenContext context)
        {
            this.context = context;
        }

        [HttpGet("productosagrupadospordescatalogado")]
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

    }

}
