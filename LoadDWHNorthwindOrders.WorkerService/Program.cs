using LoadDWHNorthwindOrders.Data.Context;
using LoadDWHNorthwindOrders.Data.Interfaces;
using LoadDWHNorthwindOrders.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace LoadDWHNorthwindOrders.WorkerService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContextPool<NorthwindContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DbNorthwind")));

                    services.AddDbContextPool<DWHNorthwindOrdersContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DbDWHNorthwindOrders")));

                    services.AddScoped<IDataServiceDWHNorthwind, DataServiceDWHNorthwind>();

                    services.AddHostedService<Worker>();
                });
    }
}