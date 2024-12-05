using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public interface IQueryExecutorService
    {
        Task ExecuteTransferAsync(string sourceConnectionString, string destinationConnectionString, string query, DateTime? fechaDesdeParametro, DateTime? fechaHastaParametro);
    }
}
