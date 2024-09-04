using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingASP.Listas;

namespace TestingASP.Tests.Listas
{
    [TestClass()]
    public class ListaBandasTests
    {
        List<Banda> _bandas = new List<Banda>() {
               new Banda { Nombre = "Banda 1", GeneroMusical = "Rock", FechaFundacion = new DateTime(2021, 1, 2), NumeroDiscosVendidos=100000 },
               new Banda { Nombre = "Banda 2", GeneroMusical = "Rock", FechaFundacion = new DateTime(2015, 11, 22), NumeroDiscosVendidos=200000 },
               new Banda { Nombre = "Banda 3", GeneroMusical = "Pop", FechaFundacion = new DateTime(2020, 4, 12), NumeroDiscosVendidos=50000 },
               new Banda { Nombre = "Banda 4", GeneroMusical = "Pop", FechaFundacion = new DateTime(2021, 7, 8), NumeroDiscosVendidos=1000000 },
               new Banda { Nombre = "Banda 5", GeneroMusical = "Folk", FechaFundacion = DateTime.Now.AddDays(-1), NumeroDiscosVendidos=500000 },
        };

        private ListaBandas _listaBandas;

        [TestInitialize]
        public void Setup()
        {
            _listaBandas = new ListaBandas(_bandas);
        }

        [TestMethod()]
        public void BandasDeGeneroMusicalTest()
        {
            Assert.AreEqual(_listaBandas.BandasDeGeneroMusical("Pop").Count, 2);
        }

        [TestMethod()]
        public void BandasDeUnAnyoTest()
        {
            Assert.AreEqual(_listaBandas.BandasDeUnAnyo(2021).Count, 2);
        }
        [TestMethod()]
        public void BandaMayorExitoTest()
        {
            Assert.AreEqual(_listaBandas.BandaMayorExito().Nombre, "Banda 4");
        }

        [TestMethod()]
        public void BandaMasRecienteTest()
        {
            Assert.AreEqual(_listaBandas.BandaMasReciente().Nombre, "Banda 5");
        }

        [TestMethod()]
        public void GananciasDeBandaTest()
        {
            Assert.AreEqual(_listaBandas.GananciasDeBanda("Banda 1", 10),1000000);
        }

        [TestMethod()]
        public void ContarBandasDeGeneroTest()
        {
            Assert.AreEqual(_listaBandas.ContarBandasDeGenero("Folk"), 1);
        }

        [TestMethod()]
        public void AlgunaBandaDeGeneroTest()
        {
            Assert.IsFalse(_listaBandas.AlgunaBandaDeGenero("Rap"));
        }

    }
}
