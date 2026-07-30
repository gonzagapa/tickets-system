namespace SupportManager.Api.Dtos{
    public record TicketDTO(
        int? Id,
        string Titulo, 
        string Descripccion,
        string Estatus,
        decimal? Latitud,
        decimal? Longitud
    );
}