namespace SupportManager.Api.Dtos
{
    public record TicketDocumentosDTO(
        int Id,
        string Titulo, 
        string Descripccion,
        string Estatus,
        decimal? Latitud,
        decimal? Longitud,
        IEnumerable<DocumentoAdjuntoDto> Documentos
    );
}