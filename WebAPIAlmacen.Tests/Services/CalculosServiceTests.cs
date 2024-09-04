using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAlmacen.Services;

namespace WebAPIAlmacen.Tests.Services
{
    [TestClass]
    public class CalculosServiceTests
    {
        [TestMethod]
        public void IvaReducidoCalculaBien()
        {
            // Preparación
            var ivaService = new CalculosService();

            // Ejecución
            var iva = ivaService.CalculoIVA(1000,false);

            // Verificación. Assert permite hacer verificaciones
            Assert.AreEqual(210, iva);
        }

        [TestMethod]
        public void IvaGeneralCalculaBien()
        {
            // Preparación
            var ivaService = new CalculosService();

            // Ejecución
            var iva = ivaService.CalculoIVA(1000, true);

            // Verificación. Assert permite hacer verificaciones
            Assert.AreEqual(100, iva);
        }
    }
}
