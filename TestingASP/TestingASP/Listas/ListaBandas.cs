using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingASP.Listas
{
    public class Banda
    {
        public string Nombre { get; set; }
        public string GeneroMusical { get; set; }
        public DateTime FechaFundacion { get; set; }
        public int NumeroDiscosVendidos { get; set; }
    }
    public class ListaBandas
    {
        private List<Banda> _bandas { get; set; }
        public ListaBandas(List<Banda> bandas)
        {
            _bandas = bandas;
        }

        public List<Banda> BandasDeGeneroMusical(string genero)
        {
            return _bandas.Where(x => x.GeneroMusical==genero).ToList();
        }

        public List<Banda> BandasDeUnAnyo(int year)
        {
            return _bandas.Where(x => x.FechaFundacion.Year == year).ToList();
        }

        public Banda BandaMayorExito()
        {
            return _bandas.MaxBy(x => x.NumeroDiscosVendidos);
        }

        public Banda BandaMasReciente()
        {
            return _bandas.MaxBy(x => x.FechaFundacion);
        }

        public decimal GananciasDeBanda(string nombre, decimal importe)
        {
            return _bandas.FirstOrDefault(x => x.Nombre==nombre).NumeroDiscosVendidos * importe;
        }

        public int ContarBandasDeGenero(string genero)
        {
            return _bandas.Count(x => x.GeneroMusical == genero);
        }

        public bool AlgunaBandaDeGenero(string genero)
        {
            return _bandas.Any(x => x.GeneroMusical == genero);
        }
    }
}
