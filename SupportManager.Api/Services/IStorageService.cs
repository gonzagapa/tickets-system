using Microsoft.AspNetCore.Http;

namespace SupportManager.Api.Services;

public interface IStorageService
{
    Task<string> GuardarArchivoAsync(IFormFile archivo, string carpeta);
    Task<bool> EliminarArchivoAsync(string rutaCompleta);
}
