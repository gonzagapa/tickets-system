using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SupportManager.Data.Entities;
using SupportManager.Data.Entities.Ticket;

namespace SupportManager.Data.Repositories;

public class TicketRepository(IConfiguration configuration) : ITicketRepositories
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("La cadena de conexión no existe.");

    public async Task<bool> ActualizarEstatusTicket(int idTicket, string estatus)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@idTicket", idTicket, dbType: DbType.Int32); 
        parameters.Add("@estatus", estatus, dbType: DbType.String);
 
        await db.ExecuteAsync("p_ActualizarEstatusTicket", parameters, commandType: CommandType.StoredProcedure );

        return true;
    }

    public async Task BorrarTicketAsync(int idTicket)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@idTicket", idTicket, dbType: DbType.Int32); 

        await db.ExecuteAsync("p_EliminarTicket", parameters,commandType: CommandType.StoredProcedure);
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

    public async Task<(Ticket Ticket, IEnumerable<DocumentoAdjunto> DocumentoAdjunto)> ObtenerTicketAsync(int idTicket)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var parameters = new DynamicParameters(); 
        parameters.Add("@IdTicket", idTicket, dbType: DbType.Int32);

        using( var res =  await db.QueryMultipleAsync(
        "p_ObtenerTicket"
        ,parameters, 
        commandType: CommandType.StoredProcedure))
        {
            //Los colocas de acuerdo al orden de select del procedimiento almancenado
            var ticket = await res.ReadFirstOrDefaultAsync<Ticket>();

            var documentos = await res.ReadAsync<DocumentoAdjunto>();

            return (ticket!, documentos);
        }


    }
}
