using System.ComponentModel.DataAnnotations;
using SupportManager.Api.Validations;

namespace SupportManager.Api.Dtos{
    public record TicketDTO(
        int? Id,
        [Required(ErrorMessage = "{Titulo is required}")]
        [MinLength(3, ErrorMessage = "Titulo debe ser mayor a 3 caracteres")]
        string Titulo,
        [Required(ErrorMessage = "{Descripccion is required}")]
        [MinLength(3, ErrorMessage = "Descripccion debe ser mayor a 3 caracteres")]
        string Descripccion,
        [Required(ErrorMessage = "{Descripccion is required}")]
        [TicketStatusAtributte]
        string Estatus,
        decimal? Latitud,
        decimal? Longitud
    );
}