using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionBatch2.Data
{

    public class DataLayer : IDataLayer
    {
        private readonly IConfiguration _configuration;

        public DataLayer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetSourceConnectionString()
        {
            return _configuration.GetConnectionString("SourceDatabase");
        }

        public string GetDestinationConnectionString()
        {
            return _configuration.GetConnectionString("DestinationDatabase");
        }

        public string GetQuery()
        {
            return _configuration["Settings:Query"];
        }

    }

}
