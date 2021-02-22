using Microsoft.Extensions.Configuration;

namespace Trapeza.Core.Config.Extensions
{
    public static class ConfigurationExtentions
    {
        public static AppConfig ReadAppConfiguration(this IConfiguration @this)
        {
            var connString = @this.GetSection("ConnectionStrings").GetSection("TrapezaDatabase").Value;
            var environment = @this.GetSection("Environment").Value;

            return new AppConfig()
            {
                ConnString = connString,
                Environment = environment
            };
        }
    }
}
