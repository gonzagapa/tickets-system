using System.ComponentModel.DataAnnotations;
using SupportManager.Data.Constants;
using SupportManager.Data.Entities.Ticket;

namespace SupportManager.Api.Validations
{
    public class TicketStatusAtributte():ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validation)
        {
            if(value is string)
            {
                if(TicketStatus.Status.Contains(value)) return ValidationResult.Success;
            }
            return new ValidationResult("Este no es un Estatus valido");
        }  
    }
}