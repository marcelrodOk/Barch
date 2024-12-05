using CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionBatch2.Data;

namespace TransactionBatch2.Business
{
    public class DataTransferService : IDataTransferService
    {
        private readonly IDataLayer _dataLayer;
        private readonly IQueryExecutorService _queryExecutorService;

        public DataTransferService(IDataLayer dataLayer, IQueryExecutorService queryExecutorService)
        {
            _dataLayer = dataLayer;
            _queryExecutorService = queryExecutorService;
        }

        public async Task TransferDataAsync(DateTime? fechaDesdeParametro, DateTime? fechaHastaParametro)
        {
            var sourceConnectionString = _dataLayer.GetSourceConnectionString();
            var destinationConnectionString = _dataLayer.GetDestinationConnectionString();
            var sqlQuery = _dataLayer.GetQuery();
            await _queryExecutorService.ExecuteTransferAsync(sourceConnectionString, destinationConnectionString, sqlQuery, fechaDesdeParametro, fechaHastaParametro);
        }
    }
}
