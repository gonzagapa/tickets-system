namespace SupportManager.Data.Repositories;

public interface IDocumentosRepository
{
    Task GuardarDocumento(string ruta, string nombreOriginal, int ticketId); 
}