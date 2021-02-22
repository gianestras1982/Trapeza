using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Trapeza.Core.Config;
using Trapeza.Core.Config.Extensions;
using Trapeza.Core.Data;
using Trapeza.Core.Services.Interfaces;

namespace Trapeza.Core.Services.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddAppServices(this IServiceCollection @this, IConfiguration config)
        {
            //Gemizoume to ServiceCollection pou kalese tin AddAppServices
            //me ta periexomena tou json (opoia theloume emeis),
            //me to connection string me tin DbContext tis efarmogis
            //kathos kai me ta Services pou ftiaksame.

            @this.AddSingleton<AppConfig>(config.ReadAppConfiguration());

            @this.AddDbContext<TrapezaDBContext>(
                (serviceProvider, optionsBuilder) =>
                {
                    var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                    optionsBuilder.UseSqlServer(appConfig.ConnString);
                });


            @this.AddScoped<ICustomerService, CustomerService>();
            @this.AddScoped<IAccountService, AccountService>();
            @this.AddScoped<ITransactionService, TransactionService>();


        }
    }
}
