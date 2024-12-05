using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionBatch2.Data
{
    public interface IDataLayer
    {
        string GetSourceConnectionString();
        string GetDestinationConnectionString();
        string GetQuery();

    }
}
