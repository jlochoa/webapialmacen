using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingASP.Calculos
{
    public class ClaseCalculosActividad
    {
        public ClaseCalculosActividad() { }
        public bool EsPar(int numero)
        {
            return numero % 2 == 0;
        }
        public long Factorial(int numero)
        {
            if (numero < 0)
            {
                throw new Exception("Número erróneo");
            }
            else if (numero==0)
            {
                return 1;
            }
            else
            {
                int factorial = 1;
                for (int i = 1; i <= numero; i++)
                {
                    factorial *= i;
                }
                return factorial;
            }
        }

        public decimal AreaTriangulo(decimal baseTriangulo,decimal alturaTriangulo)
        {
            return baseTriangulo * alturaTriangulo / 2;
        }
    }
}
