using System.ComponentModel.DataAnnotations;
using Zamowienia.Data;
namespace Zamowienia.Models

{
    public class UniqueProductAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Pole nie może być puste");
            }
            var _context = (ApplicationDbContext?)validationContext.GetService(typeof(ApplicationDbContext));
            if (_context.Przedmioty == null)
            {
                return new ValidationResult("Brak kontekstu");
            }
            if(!_context.Przedmioty.Any(p => p.NazwaProduktu == value.ToString()))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Taki produkt już istnieje");
        }
    }
}
