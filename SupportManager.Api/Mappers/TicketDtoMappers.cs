using SupportManager.Api.Dtos;
using SupportManager.Data.Entities.Ticket;
using SupportManager.Data.Repositories;

namespace SupportManager.Api.Mappers; 

public static class TicketDtoMappers
{
    public static Ticket MappToTicket(this TicketDTO dto)
    {
        return new()
        {
            IdTicket = dto.Id ?? 0,
            Descripccion = dto.Descripccion,
            Estatus = dto.Estatus,
            Titulo = dto.Titulo,
            Latitud = dto.Latitud,
            Longitud = dto.Longitud
        }; 
    }

    public static TicketLike MaptoTicketLike(this TicketDTO dto)
    {
        return new()
        {
            Titulo = dto.Titulo,
            Descripccion = dto.Descripccion,
            Estatus = dto.Estatus,
            Latitud = dto.Latitud,
            Longitud = dto.Longitud
        };
    }
} 