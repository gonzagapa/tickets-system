using SupportManager.Api.Dtos;
using SupportManager.Data.Entities.Ticket;

namespace SupportManager.Api.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDTO>> GetAllTicketAsync();
        Task<int> CreateTicketAsync(TicketDTO ticket);
        
        Task<TicketDocumentosDTO> GetTicketInfoAsync();

        Task DeleteTicketAsync(int idTicket);
    }

    public class TicketService : ITicketService
    {
        public Task<int> CreateTicketAsync(TicketDTO ticket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTicketAsync(int idTicket)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketDTO>> GetAllTicketAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TicketDocumentosDTO> GetTicketInfoAsync()
        {
            throw new NotImplementedException();
        }
    }

}