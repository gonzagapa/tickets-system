using Microsoft.AspNetCore.Mvc;

namespace SupportManager.Api.Controllers; 

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    [HttpGet]
    [Route("hello")]
    public IActionResult GetEmployees()
    {
        return Ok(new {Mesagge = "Hello world"});
    }
}