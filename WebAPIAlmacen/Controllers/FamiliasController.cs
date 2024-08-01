using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen.Models;

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
        public async Task<IEnumerable<Familia>> GetFamilias()
        {
            return await context.Familias.ToListAsync();
        }
    }
}
