using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SupportManager.Data.Entities.Ticket;

namespace SupportManager.Data.Repositories;

public class TicketRepository : ITicketRepositories
{
    private readonly string _connectionString;

    public TicketRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException("La cadena de conexión no existe.");
    }

    public Task BorrarTicketAsync(int idTicket)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CrearTicketAsync(TicketLike ticket)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@titulo", ticket.Titulo);
        parameters.Add("@descripcion", ticket.Descripccion);
        parameters.Add("@estatus", ticket.Estatus);
        parameters.Add("@latitud", ticket.Latitud);
        parameters.Add("@longitud", ticket.Longitud);

        return await db.QuerySingleAsync<int>("p_CrearTicket",parameters, commandType: CommandType.StoredProcedure);
  
    }

    public async Task<IEnumerable<Ticket>> ObtenerListaTicketsAsync()
    {
        //con using aseguramos cerrar la conexion cuando no se ocupe.
        using IDbConnection db = new SqlConnection(_connectionString);
        return await db.QueryAsync<Ticket>(
            "p_ObtenerListaTickets", 
            commandType:CommandType.StoredProcedure
        );

    }

    public Task<Ticket> ObtenerTicketAsync(int idTicket)
    {
        throw new NotImplementedException();
    }
}
