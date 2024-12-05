using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class QueryExecutorService : IQueryExecutorService
    {
        public async Task ExecuteTransferAsync(string sourceConnectionString, string destinationConnectionString, string query, DateTime? fechaDesdeParametro, DateTime? fechaHastaParametro)
        {
            using var sourceConnection = new SqlConnection(sourceConnectionString);
            using var destinationConnection = new SqlConnection(destinationConnectionString);

            DateTime fechaDesdeDefault =  DateTime.Now.AddDays(-1);
            DateTime fechaHastaDefault =  DateTime.Now;

            // Determina las fechas a usar
            DateTime? fechaDesde = fechaDesdeParametro != null
                ? fechaDesdeParametro
                : fechaDesdeDefault;

            DateTime? fechaHasta = fechaDesdeParametro != null
                ? fechaHastaParametro
                : fechaHastaDefault;

            Console.WriteLine($"Fecha Desde: {fechaDesde}");
            Console.WriteLine($"Fecha Hasta: {fechaHasta}");

            await sourceConnection.OpenAsync();
            await destinationConnection.OpenAsync();

            using var transaction = destinationConnection.BeginTransaction();


            try
            {
                using var command = new SqlCommand(query, sourceConnection);

                // Agregar los parámetros

                command.Parameters.AddWithValue("@FechaDesde", fechaDesde);
                command.Parameters.AddWithValue("@FechaHasta", fechaHasta);

                using var reader = await command.ExecuteReaderAsync();

                using var bulkCopy = new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.Default, transaction)
                {
                    DestinationTableName = "DestinationTable"
                };

                await bulkCopy.WriteToServerAsync(reader);
                await transaction.CommitAsync();

                Console.WriteLine("Transferencia completada con éxito.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }

}
