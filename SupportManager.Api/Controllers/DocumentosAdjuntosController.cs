using Microsoft.AspNetCore.Mvc;
using SupportManager.Api.Dtos;
using SupportManager.Api.Services;

namespace SupportManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentosAdjuntosController(IDocumentosService documentosService) : ControllerBase
{
    private readonly IDocumentosService _documentosService = documentosService;

    [HttpPost]
    public async Task<IActionResult> CargarDocumento([FromForm] CargarDocumentoDto dto)
    {
        // ASP.NET Core valida el DTO utilizando los atributos definidos en Validations.
        // Si no cumple con el tamaño máximo o la extensión, ModelState.IsValid será false.
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _documentosService.GuardarDocumentoAsync(dto);
        if (!result)
        {
            return StatusCode(500, new { Message = "Hubo un error al intentar guardar el documento en el servidor." });
        }

        return Ok(new { Message = "Documento procesado y registrado con éxito." });
    }
}
