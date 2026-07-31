using System.Data.Common;
using SupportManager.Api.Dtos;
using SupportManager.Api.Mappers;
using SupportManager.Data.Entities.Ticket;
using SupportManager.Data.Repositories;

namespace SupportManager.Api.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDTO>> GetAllTicketAsync();
        Task<int> CreateTicketAsync(TicketDTO ticket);
        
        Task<TicketDocumentosDTO> GetTicketInfoAsync(int idTicket);

        Task<bool> DeleteTicketAsync(int idTicket);

        Task<bool> UpdateTicketStatus(int idTicket, string estatus);
    }

    public class TicketService(ITicketRepositories repository) : ITicketService
    {
        private readonly  ITicketRepositories _repository = repository;
        public async Task<int> CreateTicketAsync(TicketDTO ticket)
        {
            try
            {     
                var res =  await _repository.CrearTicketAsync(ticket.MaptoTicketLike());
                return res;
            }
            catch (DbException)
            {
                return 0;
            }
        }

        public async Task<bool> DeleteTicketAsync(int idTicket)
        {
            try
            {
                await  _repository.BorrarTicketAsync(idTicket);
                return true;
            }
            catch (DbException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TicketDTO>> GetAllTicketAsync()
        {
          var list = await _repository.ObtenerListaTicketsAsync(); 
          return list.Select(item => new TicketDTO(
                item.IdTicket, 
                item.Titulo,
                item.Descripccion,
                item.Estatus,
                item.Latitud,
                item.Longitud
            )); 
        }

        public async Task<TicketDocumentosDTO> GetTicketInfoAsync(int idTicket)
        {
            var (ticket,documentos) = await _repository.ObtenerTicketAsync(idTicket);

            var documentosDto = documentos.Select(item => new DocumentoAdjuntoDto(item.Id, item.Ruta,item.NombreOriginal, item.FechaCreacion, item.TicketId));

            return new TicketDocumentosDTO(
               ticket.IdTicket,
               ticket.Titulo,
               ticket.Descripccion,
               ticket.Estatus,
               ticket.Latitud,
               ticket.Longitud,
                documentosDto
            );
        }

        public async Task<bool> UpdateTicketStatus(int idTicket, string estatus)
        {
            try
            {
                var res = await _repository.ActualizarEstatusTicket(idTicket, estatus);
                return res;
            }
            catch (DbException)
            {
                return false;
            }
        }
    }

}