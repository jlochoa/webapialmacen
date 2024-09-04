using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAlmacen.Validators;

namespace WebAPIAlmacen.Tests.Validators
{
    [TestClass]
    public class PrimeraMayusculaAttributeTests
    {
        [TestMethod]
        public void PrimeraLetraMinuscula_DevuelveError()
        {
            // Preparación
            var primeraLetraMayuscula = new PrimeraMayusculaAttribute();
            var valor = "juan luis";
            var valContext = new ValidationContext(new { Nombre = valor });

            // Ejecución
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            // Verificación. Assert permite hacer verificaciones
            Assert.AreEqual("La primera letra debe ser mayúscula", resultado.ErrorMessage);
        }

        [TestMethod]
        public void ValorNulo_DevuelveError()
        {
            // Preparación
            var primeraLetraMayuscula = new PrimeraMayusculaAttribute();
            string valor = null;
            var valContext = new ValidationContext(new { Nombre = valor });

            // Ejecución. Si no hay errores retorna null
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            // Verificación
            Assert.AreEqual("El valor no puede ser nulo", resultado.ErrorMessage);
        }

        [TestMethod]
        public void ValorConPrimeraLetraMayuscula_NoDevuelveError()
        {
            // Preparación
            var primeraLetraMayuscula = new PrimeraMayusculaAttribute();
            string valor = "Juan Luis";
            var valContext = new ValidationContext(new { Nombre = valor });

            // Ejecución
            var resultado = primeraLetraMayuscula.GetValidationResult(valor, valContext);

            // Verificación
            Assert.AreEqual(ValidationResult.Success, resultado);
        }
    }

}
