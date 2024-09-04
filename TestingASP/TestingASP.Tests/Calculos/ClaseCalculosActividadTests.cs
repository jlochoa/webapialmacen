using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingASP.Calculos;

namespace TestingASP.Tests.Calculos
{
    [TestClass()]
    public class ClaseCalculosActividadTests
    {
        [TestMethod()]
        public void EsParTest()
        {
            var calculos = new ClaseCalculosActividad();
            var par = calculos.EsPar(8);
            Assert.AreEqual(par, true);
           // Assert.IsTrue(par);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FactorialError()
        {
            var calculos = new ClaseCalculosActividad();
            var importe = calculos.Factorial(-15);
        }

        [TestMethod()]
        public void FactorialTest()
        {
            var calculos = new ClaseCalculosActividad();
            long factorial = 0;
            factorial = calculos.Factorial(0);
            Assert.AreEqual(factorial, 1);
            factorial = calculos.Factorial(4);
            Assert.AreEqual(factorial, 24);
        }

        [TestMethod()]
        public void AreaTrianguloTest()
        {
            var calculos = new ClaseCalculosActividad();
            var area = calculos.AreaTriangulo(5,2);
            Assert.AreEqual(area, 5);
        }

    }
}
