using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingASP.Listas;

namespace TestingASP.Tests.Listas
{
    [TestClass()]
    public class ListaPersonasTests
    {
        List<Persona> _personas = new List<Persona>() {
               new Persona { Nombre = "Eduardo", Edad = 30, FechaIngresoALaEmpresa = new DateTime(2021, 1, 2), Soltero = true },
               new Persona { Nombre = "Juan Luis", Edad = 19, FechaIngresoALaEmpresa = new DateTime(2015, 11, 22), Soltero = true },
               new Persona { Nombre = "Ana", Edad = 45, FechaIngresoALaEmpresa = new DateTime(2020, 4, 12), Soltero = false },
               new Persona { Nombre = "Marta", Edad = 24, FechaIngresoALaEmpresa = new DateTime(2021, 7, 8), Soltero = false },
               new Persona { Nombre = "Susana", Edad = 61, FechaIngresoALaEmpresa = DateTime.Now.AddDays(-1), Soltero = false },
        };

        private ListaPersonas _listaPersonas;

        [TestInitialize]
        public void Setup()
        {
            _listaPersonas = new ListaPersonas(_personas);
        }

        [TestMethod()]
        public void SolterosTest()
        {
            Assert.AreEqual(_listaPersonas.Solteros().Count, 2);
        }

        [TestMethod()]
        public void PersonaMayorTest()
        {
            Assert.AreEqual(_listaPersonas.PersonaMayor().Nombre, "Susana");
        }

        [TestMethod()]
        public void TodosMayoresEdadTest()
        {
            Assert.IsTrue(_listaPersonas.TodosMayoresEdad());
        }

        [TestMethod()]
        public void TodosMayoresDeUnaEdadTest()
        {
            Assert.IsFalse(_listaPersonas.TodosMayoresOIgualesDeUnaEdad(50));
        }
    }


}
