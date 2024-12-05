using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionBatch2.Business
{
    public interface IDataTransferService
    {
        Task TransferDataAsync(DateTime? fechaDesdeParametro, DateTime? fechaHastaParametro);
    }
}
