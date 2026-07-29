namespace SupportManager.Data.Entities.Ticket
{
    public class Ticket
    {
        public int IdTicket {get; set;}
        public string Titulo { get; set; } = string.Empty; 

        public string Descripccion { get; set; }   = string.Empty; 

        public string Estatus { get; set;} = string.Empty; 

        public DateTime FechaCreacion {get; set;} 

        public decimal? Latitud {get; set;}

        public decimal? Longitud {get; set;}
    }
}