using System.ComponentModel.DataAnnotations;

namespace WebAPIAlmacen.Validators
{
    public class PrimeraMayusculaAttribute : ValidationAttribute
    {
        // value sería el valor a validar
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("El valor no puede ser nulo");
            }
            var primeraLetra = value.ToString()[0].ToString();

            if (primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayúscula");
            }

            return ValidationResult.Success;
        }
    }

}
