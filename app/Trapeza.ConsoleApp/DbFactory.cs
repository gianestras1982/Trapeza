using Trapeza.Core.Config;
using Trapeza.Core.Config.Extensions;
using Trapeza.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Trapeza.ConsoleApp
{
    public class DbFactory
    {
        public DbContextOptionsBuilder<TrapezaDBContext> GetCon()
        {
            ConfigurationBuilder confBldr = new ConfigurationBuilder();
            IConfigurationBuilder _iconfBldr = confBldr.SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}");
            _iconfBldr = _iconfBldr.AddJsonFile("appsettings.json", false);
            IConfigurationRoot _iconfRoot = _iconfBldr.Build();

            AppConfig appConfg = _iconfRoot.ReadAppConfiguration();

            DbContextOptionsBuilder<TrapezaDBContext> dbCntxtOptnsBldr = new DbContextOptionsBuilder<TrapezaDBContext>();

            dbCntxtOptnsBldr.UseSqlServer(appConfg.ConnString,
                options => {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                });

            return dbCntxtOptnsBldr;
        }
    }
}
