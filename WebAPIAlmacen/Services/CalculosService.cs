namespace WebAPIAlmacen.Services
{
    public class CalculosService
    {
        public decimal CalculoIVA(decimal importe, bool reducido)
        {
            if (reducido)
            {
                return importe * 0.10m;
            }
            else
            {
                return importe * 0.21m; ;
            }
        }
    }
}
