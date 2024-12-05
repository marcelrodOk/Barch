using Data;
using Microsoft.Extensions.Configuration;

namespace DataLayer
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
