using WebAPIAlmacen.Validators;

namespace WebAPIAlmacen.DTOs
{
    public class DTOFamilia
    {
        public int Id { get; set; }
        [PrimeraMayusculaAttribute]
        public string Nombre { get; set; }
    }
}
