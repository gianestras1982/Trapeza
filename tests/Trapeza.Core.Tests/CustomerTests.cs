using Trapeza.Core.Config;
using Trapeza.Core.Config.Extensions;
using Trapeza.Core.Data;
using Trapeza.Core.Model.Types;
using Trapeza.Core.Services;
using Trapeza.Core.Services.Interfaces;
using Trapeza.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Xunit;

namespace Trapeza.Core.Tests
{
    public class CustomerTests : IClassFixture<TrapezaFixture>
    {
        private ICustomerService _customer;

        public CustomerTests(TrapezaFixture fixture)
        {
            _customer = fixture.Scope.ServiceProvider.GetRequiredService<ICustomerService>();
        }


        [Fact]
        public async void Test_RegisterCustomerAsync()
        {
            var options = new RegisterCustomerOptions()
            {
                Name = "Sofia",
                SurName = "Ourolidou",
                VatNumber = "065252238",
                CustType = CustomerType.Personal,
                Address = "Irakleitou 17"
            };

            var customer = await _customer.RegisterCustomerAsync(options);

            Assert.NotNull(customer);
        }

        [Fact]
        public async void Test_GetCustomerByIdAsync()
        {
            var customer = await _customer.GetCustomerByIdAsync(2);

            Assert.NotNull(customer);
        }

        [Fact]
        public async void Test_GetAllInOneCustAsync()
        {
            var customer = await _customer.GetAllInOneCustAsync(1);

            Assert.NotNull(customer);
        }

    }
}
