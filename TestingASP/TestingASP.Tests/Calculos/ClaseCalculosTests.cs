using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingASP.Calculos;

namespace TestingASP.Tests.Calculos
{
    [TestClass()]
    public class ClaseCalculosTests
    {
        [TestMethod()]
        public void LetraDNITest()
        {
            var calculos = new ClaseCalculos();
            var letra = calculos.LetraDNI(33420108);
            Assert.AreEqual(letra, 'G');
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ImporteFacturaNegativoError()
        {
            var calculos = new ClaseCalculos();
            var importe = calculos.ImporteNetoFactura(-1500);
        }

        [TestMethod()]
        public void ImporteNetoFacturaTest()
        {
            decimal importe;
            var calculos = new ClaseCalculos();
            importe = calculos.ImporteNetoFactura(1500);
            Assert.AreEqual(importe, 1350);
            importe = calculos.ImporteNetoFactura(500);
            Assert.AreEqual(importe, 500);
        }
    }
}
