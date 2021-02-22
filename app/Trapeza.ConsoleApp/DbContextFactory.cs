using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Trapeza.Core.Data;
using Trapeza.Core.Config.Extensions;
using System.Reflection;

namespace Trapeza.ConsoleApp
{
    public class DbContextFactory : IDesignTimeDbContextFactory<TrapezaDBContext>
    {
        public TrapezaDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<TrapezaDBContext>();

            optionsBuilder.UseSqlServer(
                config.ConnString,
                options => {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });

            return new TrapezaDBContext(optionsBuilder.Options);
        }
    }
}
