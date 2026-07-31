using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using SupportManager.Api.Validations;

namespace SupportManager.Api.Dtos;

public record CargarDocumentoDto(
    [Required(ErrorMessage = "El identificador del ticket es requerido.")]
    int TicketId,

    [Required(ErrorMessage = "Debe adjuntar un archivo.")]
    [MaxFileSize(5 * 1024 * 1024)] // Límite de 5 MB
    [AllowedExtensions([".jpg", ".jpeg", ".png"])]
    IFormFile Archivo
);
