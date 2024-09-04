using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingASP.Listas
{
    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime FechaIngresoALaEmpresa { get; set; }
        public bool Soltero { get; set; }
    }
    public class ListaPersonas
    {
        private List<Persona> _personas { get; set; }
        public ListaPersonas(List<Persona> personas)
        {
            _personas = personas;
        }

        public List<Persona> Solteros()
        {
            return _personas.Where(x => x.Soltero).ToList();
        }

        public Persona PersonaMayor()
        {
            return _personas.MaxBy(x => x.Edad);
        }

        public bool TodosMayoresEdad()
        {
            return _personas.All(x => x.Edad >= 18);
        }

        public bool TodosMayoresOIgualesDeUnaEdad(int edad)
        {
            return _personas.All(x => x.Edad >= edad);
        }
    }


}
