using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingASP.Calculos
{
    public class ClaseCalculos
    {
        public ClaseCalculos() { }
        public char LetraDNI(int dni)
        {
            int resto = dni % 23;
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            var letra = letras[resto];
            return letra;
        }

        public decimal ImporteNetoFactura(decimal importe)
        {
            if (importe < 0)
            {
                throw new Exception("Importe erróneo");
            }
            else if (importe > 1000)
            {
                return importe - (importe * 10 / 100);
            }
            else
            {
                return importe;
            }
        }
    }
}
