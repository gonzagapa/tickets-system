using System.Security.Cryptography.X509Certificates;

namespace SupportManager.Api.Dtos
{
    public record DocumentoAdjuntoDto(
        Guid Id,
        string Ruta,
        string NombreOriginal,
        DateTime FechaCreacion,
        int TicketId
    );
}