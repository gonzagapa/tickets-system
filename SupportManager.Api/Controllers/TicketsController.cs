using Microsoft.AspNetCore.Mvc;
using SupportManager.Api.Dtos;
using SupportManager.Api.Services;

namespace SupportManager.Api.Controllers; 

[Route("api/[controller]")]
[ApiController]
public class TicketController(ITicketService ticketService) : ControllerBase
{
    private readonly ITicketService _ticketService = ticketService;

    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
        var tickets = await _ticketService.GetAllTicketAsync();
        return Ok(tickets);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicket(int id)
    {
        var ticketDto = await _ticketService.GetTicketInfoAsync(id); 
        return Ok(ticketDto); 
    } 

    [HttpPost]
    public async Task<IActionResult> CreateTicket(TicketDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var ticketId = await _ticketService.CreateTicketAsync(dto);
        return CreatedAtAction(nameof(GetTicket), new {id = ticketId}, dto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var isDeleted = await _ticketService.DeleteTicketAsync(id);
        if(!isDeleted) return NotFound(new {Message = "Ticket not found"});

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateTicket(int id, string status)
    {
        var res = await _ticketService.UpdateTicketStatus(id, status);
        if(!res) return NotFound("Ticket not found");

        return NoContent();
    }
}