using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPIAlmacen.DTOs;
using WebAPIAlmacen.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPIAlmacen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliasController : ControllerBase
    {
        private readonly MiAlmacenContext context;

        // Inyección de dependencia.
        // Como nuestro controller depende de MiAlmacenContext para poder desempeñar sus funciones, lo podemos inyectar en el constructor
        // La inyección de dependencia trae al constructor de la clase todas las dependencias que necesita y las pasa a variables privadas
        // de la clase

        public FamiliasController(MiAlmacenContext context)
        {
            this.context = context;
        }

        [HttpGet]
        // No podemos tener dos get con la misma ruta
        [HttpGet] // api/familias
                  // [HttpGet("listadofamilias")] // api/familias/listadofamilias
                  //  [HttpGet("/listadofamilias")] // listadofamilias
        public async Task<IEnumerable<Familia>> GetFamilias()

        {
            return await context.Familias.ToListAsync();
        }

        [HttpGet("sync")]
        public IEnumerable<Familia> GetFamiliasSync()
        {
            return context.Familias.ToList(); // No debería hacerse así
        }

        // Ejemplo de programación asíncrona
        // async hace de la función una función asíncrona. Una función async debe resolver algo de tipo Task (como una promesa)
        // Task sería para no retornar un valor al final de la ejecución de la función asíncrona (algo así como un void) y Task<T> sería para retornar un valor

        [HttpGet("actionresult")]
        public async Task<ActionResult<List<Familia>>> GetFamiliasActionResult()
        {
            try
            {
                var listaOrdenada = await context.Familias.ToListAsync();
                return Ok(listaOrdenada);
            }
            catch (Exception)
            {

                //  return StatusCode(StatusCodes.Status500InternalServerError);
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = "Se ha producido un error de acceso a la base de datos"
                };
            }

        }

        // Tracking-->Permite controlar si las consultas que hacemos deben ir "anotando" los cambios que después
        // en el mismo procedimiento pueden sufrir.Por ejemplo, si hacemos una get para luego modificar
        // los datos, la get debe tener tracking.Pero si no, no es necesario y se gana rapidez
        [HttpGet("notracking")]
        public async Task<IEnumerable<Familia>> GetFamiliasNoTracking()
        {
            return await context.Familias.AsNoTracking().ToListAsync();
        }

        // [HttpGet("{id}")] // api/familias/1 -->Si llamamos a api/familias/juan da 400
        [HttpGet("{id:int}")] // api/familias/1 -->Si llamamos a api/familias/juan da 404 por la restricción
        public async Task<ActionResult<Familia>> GetFamiliaPorId(int id)
        {
            var familia = await context.Familias.FindAsync(id);
            if (familia == null)
            {
                return NotFound();
            }
            return Ok(familia);
        }

        [HttpGet("{contiene}")]
        //  [HttpGet("{contiene}/{param2?}")] // api/familias/a/b  --> param2 es opcional por el ?
        //  [HttpGet("{contiene}/{param2=hogar}")] // api/familias/a/b  --> param2 tiene el valor por defecto hogar
        // public async Task<ActionResult<Familia>> PrimeraFamiliaPorContiene(string contiene, string param2)
        public async Task<ActionResult<Familia>> GetPrimeraFamiliaPorContiene(string contiene)
        {
            var familia = await context.Familias.FirstOrDefaultAsync(x => x.Nombre.Contains(contiene));
            if (familia == null)
            {
                return NotFound();
            }
            return Ok(familia);
        }

        [HttpGet("parametrocontienequerystring")] // api/familias/parametrocontienequerystring?contiene=tec
        public async Task<ActionResult<IEnumerable<Familia>>> GetFamiliasContieneQueryString([FromQuery] string contiene)
        {
            var familias = await context.Familias.Where(x => x.Nombre.Contains(contiene)).ToListAsync();
            return Ok(familias);
        }

        [HttpGet("ordennombre/{desc}")]
        public async Task<ActionResult<IEnumerable<Familia>>> GetFamiliasOrdenNombre(bool desc)
        {
            List<Familia> familias = new List<Familia>();
            if (desc)
            {
                familias = await context.Familias.OrderBy(x => x.Nombre).ToListAsync();
            }
            else
            {
                familias = await context.Familias.OrderByDescending(x => x.Nombre).ToListAsync();
            }

            return Ok(familias);
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Familia>>> GetFamiliasPaginacion()
        {
            var familias = await context.Familias.Take(2).ToListAsync();
            var familias2 = await context.Familias.Skip(1).Take(2).ToListAsync();
            return Ok(new { take = familias, takeSkip = familias2 });
        }

        [HttpGet("paginacion2/{pagina?}")]
        public async Task<ActionResult<IEnumerable<Familia>>> GetFamiliasPaginacionPersonalizada(int pagina = 1)
        {
            int registrosPorPagina = 2;
            var familias = await context.Familias.Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToListAsync();
            return Ok(familias);
        }

        [HttpGet("seleccioncampos")]
        public async Task<ActionResult> GetFamiliasSeleccionCampos()
        {
            var familias = await context.Familias.Select(x => new { Id = x.Id, Nombre = x.Nombre }).ToListAsync();
            var familias2 = await (from x in context.Familias
                                   select new
                                   {
                                       Id = x.Id,
                                       Nombre = x.Nombre
                                   }).ToListAsync();
            return Ok(new { familias = familias, familias2 = familias2 });
        }

        [HttpGet("seleccioncamposdto")]
        public async Task<ActionResult> GetFamiliasSeleccionCamposDTO()
        {
            var familias = await context.Familias.Select(x => new DTOFamilia { Id = x.Id, Nombre = x.Nombre }).ToListAsync();
            var familias2 = await (from x in context.Familias
                                   select new DTOFamilia
                                   {
                                       Id = x.Id,
                                       Nombre = x.Nombre
                                   }).ToListAsync();
            return Ok(new { familias = familias, familias2 = familias2 });
        }

        [HttpGet("familiasproductos/{id:int}")]
        public async Task<ActionResult<Familia>> GetFamiliasProductosEager(int id)
        {
            // Familia llama a producto y producto a familia, lo que provoca un ciclo infinito del que informa swagger.
            // Por eso, hay que ir al Program y el la configuración de los controllers determinar que se ignoren los ciclos
            // Con ThenInclude podemos profundizar más en las relaciones
            var familia = await context.Familias.Include(x => x.Productos).FirstOrDefaultAsync(x => x.Id == id);
            if (familia == null)
            {
                return NotFound();
            }
            return Ok(familia);
        }

        [HttpGet("familiasproductosselect/{id:int}")]
        public async Task<ActionResult<Familia>> GetFamiliasProductosSelect(int id)
        {
            // Probar los dos
            //var familia = await context.Familias
            //    .Select(x=> new DTOFamiliaProducto
            //    {
            //        IdFamilia = x.Id,
            //        Nombre = x.Nombre,
            //        TotalProductos = x.Productos.Count(),
            //        Productos = x.Productos.Select(y => new DTOProductoItem
            //        {
            //                IdProducto=y.Id,
            //                Nombre = y.Nombre
            //        }).ToList(),
            //    })
            //    .FirstOrDefaultAsync(x => x.IdFamilia == id);

            var familia = await (from x in context.Familias
                                 select new DTOFamiliaProducto
                                 {
                                     IdFamilia = x.Id,
                                     Nombre = x.Nombre,
                                     TotalProductos = x.Productos.Count(),
                                     Productos = x.Productos.Select(y => new DTOProductoItem
                                     {
                                         IdProducto = y.Id,
                                         Nombre = y.Nombre
                                     }).ToList(),
                                 }).FirstOrDefaultAsync(x => x.IdFamilia == id);

            if (familia == null)
            {
                return NotFound();
            }
            return Ok(familia);
        }

        [HttpPost]
        public async Task<ActionResult> PostFamilia(DTOFamilia familia)
        {
            var newFamilia = new Familia()
            {
                Nombre = familia.Nombre
            };

            var estatus1 = context.Entry(newFamilia).State;

            await context.AddAsync(newFamilia);
            var estatus2 = context.Entry(newFamilia).State;

            await context.SaveChangesAsync();
            var estatus3 = context.Entry(newFamilia).State;

            return Created("Familia", new { familia = newFamilia });
        }

        [HttpPost("varios")]
        public async Task<ActionResult> PostFamilias(DTOFamilia[] familias)
        {
            List<Familia> variasFamilias = new();
            foreach (var f in familias)
            {
                variasFamilias.Add(new Familia
                {
                    Nombre = f.Nombre
                });
            }
            await context.AddRangeAsync(variasFamilias);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutFamilia([FromBody] DTOFamilia familia)
        {
            var familiaUpdate = await context.Familias.AsTracking().FirstOrDefaultAsync(x => x.Id == familia.Id);
            if (familiaUpdate == null)
            {
                return NotFound();
            }
            familiaUpdate.Nombre = familia.Nombre;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var hayProductos = await context.Productos.AnyAsync(x => x.FamiliaId == id);
            if (hayProductos)
            {
                return BadRequest("Hay productos relacionados");
            }
            var familia = await context.Familias.FirstOrDefaultAsync(x => x.Id == id);

            if (familia is null)
            {
                return NotFound();
            }

            context.Remove(familia);
            await context.SaveChangesAsync();
            return Ok();
        }

        // Borra relacionados
        [HttpDelete("relacionados/{id:int}")]
        public async Task<ActionResult> DeleteFamiliaProductos(int id)
        {
            var familia = await context.Familias.Include(x => x.Productos).FirstOrDefaultAsync(x => x.Id == id);

            if (familia is null)
            {
                return NotFound();
            }

            context.RemoveRange(familia.Productos);

            context.Remove(familia);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("sql/{id:int}")]
        public async Task<ActionResult<Familia>> FamiliaPorIdSQL(int id)
        {
            var familia = await context.Familias
                        .FromSqlInterpolated($"SELECT * FROM Familias WHERE Id = {id}")
                        .FirstOrDefaultAsync();

            if (familia == null)
            {
                return NotFound();
            }
            return Ok(familia);
        }

        [HttpPost("sql")]
        public async Task<ActionResult> Post(DTOFamilia familia)
        {
            //Ejemplo de sentencia SQL de inserción	
            await context.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO Familias(Nombre) VALUES({familia.Nombre})");

            return Ok();
        }

        // Acceso a procedimientos almacenados

        [HttpGet("procedimiento_almacenado/{id:int}")]
        public async Task<ActionResult<Familia>> GetSP(int id)
        {
            var familias = context.Familias
                        .FromSqlInterpolated($"EXEC Familias_ObtenerPorId {id}")
                        .IgnoreQueryFilters()
                        .AsAsyncEnumerable(); // No podemos volcar los resultados de un procedimiento en un List o First porque el procedimiento almacenado no es una consulta que EF pueda manejar 

            // Solo obtenemos uno
            await foreach (var familia in familias)
            {
                return familia;
            }

            return NotFound();
        }

        [HttpPost("Procedimiento_almacenado")]
        public async Task<ActionResult> PostSP(DTOFamilia familia)
        {
            var outputId = new SqlParameter();
            outputId.ParameterName = "@id";
            outputId.SqlDbType = System.Data.SqlDbType.Int;
            outputId.Direction = System.Data.ParameterDirection.Output;

            await context.Database
                .ExecuteSqlRawAsync("EXEC Familias_Insertar @nombre = {0}, @id = {1} OUTPUT",
                familia.Nombre, outputId);

            var id = (int)outputId.Value;
            return Ok(id);
        }


    }
}
