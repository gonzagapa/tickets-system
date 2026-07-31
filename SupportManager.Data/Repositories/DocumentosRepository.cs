using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SupportManager.Data.Repositories;

public class DocumentosRepository(IConfiguration configuration) : IDocumentosRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("La cadena de conexión no existe.");
    public async Task GuardarDocumento(string ruta, string nombreOriginal, int ticketId)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@ruta", ruta, dbType: DbType.String);
        parameters.Add("@nombreOriginal", nombreOriginal, dbType: DbType.String);
        parameters.Add("@ticketId", ticketId, dbType: DbType.Int32);

        await db.ExecuteAsync("p_GuardarDocumento", parameters, commandType: CommandType.StoredProcedure);

    }
}