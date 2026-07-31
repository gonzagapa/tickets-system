using SupportManager.Api.Dtos;
using SupportManager.Data.Repositories;
using System.Data.Common;

namespace SupportManager.Api.Services;

public interface IDocumentosService
{
    Task<bool> GuardarDocumentoAsync(CargarDocumentoDto dto);
}

public class DocumentosService(IDocumentosRepository repository, ITicketRepositories ticketRepository) : IDocumentosService
{
    private readonly IDocumentosRepository _repository = repository;
    private readonly ITicketRepositories _ticketRepository = ticketRepository;

    public async Task<bool> GuardarDocumentoAsync(CargarDocumentoDto dto)
    {
        try
        {
            // 1. Validar que el ticket exista en la base de datos
            var (ticket, _) = await _ticketRepository.ObtenerTicketAsync(dto.TicketId);
            if (ticket == null)
            {
                throw new ArgumentException($"El ticket con ID {dto.TicketId} no existe.");
            }

            // =========================================================================
            // TODO: IMPLEMENTAR AQUÍ EL GUARDADO DEL ARCHIVO (LOCAL O EN CLOUDINARY)
            // =========================================================================
            // Deberás procesar 'dto.Archivo' (que es de tipo IFormFile) para guardarlo física 
            // o virtualmente y obtener la URL o ruta final.
            //
            // EJEMPLO LOCAL:
            // 1. Obtener la ruta de destino (p.ej. wwwroot/uploads).
            // 2. Generar un nombre único (p.ej. usando Guid.NewGuid() + extensión).
            // 3. Crear un FileStream y copiar el contenido con: await dto.Archivo.CopyToAsync(stream);
            // 4. Retornar la ruta relativa (p.ej. "/uploads/mi-archivo-unico.pdf").
            //
            // EJEMPLO CLOUDINARY:
            // 1. Configurar el SDK de Cloudinary.
            // 2. Subir el archivo usando Cloudinary Uploader (p.ej. cloudinary.UploadAsync(uploadParams)).
            // 3. Obtener el SecureUrl o Url devuelto.
            //
            // Por ahora, usaremos una ruta simulada para que puedas testear la base de datos.
            // Reemplaza esto con tu lógica de almacenamiento:
            string rutaDeAlmacenamiento = $"/uploads/{Guid.NewGuid()}_{dto.Archivo.FileName}";
            // =========================================================================

            // 3. Guardar el registro en la base de datos usando el procedimiento almacenado
            await _repository.GuardarDocumento(rutaDeAlmacenamiento, dto.Archivo.FileName, dto.TicketId);
            
            return true;
        }
        catch (DbException)
        {
            return false;
        }
    }
}
