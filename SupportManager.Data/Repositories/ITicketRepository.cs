using SupportManager.Data.Entities;
using SupportManager.Data.Entities.Ticket;

namespace SupportManager.Data.Repositories
{
    public class TicketLike
    {
        public string Titulo { get; set; } = string.Empty; 

        public string Descripccion { get; set; }   = string.Empty; 

        public string Estatus { get; set;} = string.Empty;

        public decimal? Latitud {get; set;}

        public decimal? Longitud {get; set;}
    }
    public interface ITicketRepositories
    {
        Task<(Ticket Ticket, IEnumerable<DocumentoAdjunto> DocumentoAdjunto)> ObtenerTicketAsync(int idTicket);

        Task<IEnumerable<Ticket>> ObtenerListaTicketsAsync();

        Task<int> CrearTicketAsync(TicketLike ticket); 

        Task BorrarTicketAsync(int idTicket); 
    }
}