using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Trapeza.Core.Services.Extensions;

namespace Trapeza.Core.Tests
{
    public class TrapezaFixture : IDisposable
    {
        public IServiceScope Scope { get; private set; }

        public TrapezaFixture()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddAppServices(config);

            Scope = serviceCollection.BuildServiceProvider().CreateScope();
        }
        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}
