using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data;
using Business;
using CommonServices;

class Program
{

    static async Task Main(string[] args)
    {

        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        var dataTransferService = serviceProvider.GetRequiredService<IDataTransferService>();

        DateTime? fechaDesde = args.Length > 0 && DateTime.TryParse(args[0], out var desde) ? desde : null;
        DateTime? fechaHasta = args.Length > 1 && DateTime.TryParse(args[1], out var hasta) ? hasta : null;


        await dataTransferService.TransferDataAsync(fechaDesde, fechaHasta);

    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddScoped<IDataLayer, DataLayer.DataLayer>();
        services.AddScoped<IQueryExecutorService, QueryExecutorService>();
        services.AddScoped<IDataTransferService, DataTransferService>();
    }
}
