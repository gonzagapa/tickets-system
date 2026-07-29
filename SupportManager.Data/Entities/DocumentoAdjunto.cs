namespace SupportManager.Data.Entities
{
    public class DocumentoAdjunto
    {
       public Guid Id {get; set;}

       public string Ruta {get; set;} = string.Empty;  

       public string NombreOriginal { get; set; } = string.Empty;

       public DateTime FechaCreacion {get; set;} 

       public int TicketId {get; set;}
    }
}