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
    public class AccountTests : IClassFixture<TrapezaFixture>
    {
        private IAccountService _account;

        public AccountTests(TrapezaFixture fixture)
        {
            _account = fixture.Scope.ServiceProvider.GetRequiredService<IAccountService>();
        }


        [Fact]
        public async void Test_RegisterAccountAsync()
        {
            var options = new RegisterAccountOptions()
            {
                Currency = "EUR",
                Description = "Logariasmos gia to paidi"
            };

            var account = await _account.RegisterAccountAsync(1, options);

            Assert.NotNull(account);
        }

        
        [Fact]
        public async void Test_GetAccountByIdAsync()
        {
            var account = await _account.GetAccountByIdAsync(1);

            Assert.NotNull(account);
        }


        [Fact]
        public async void Test_UpdateAccountStateTypeAsync()
        {
            var account = await _account.UpdateAccountStateTypeAsync(3, AccountStateType.Active);

            Assert.NotNull(account);
        }

        [Fact]
        public async void Test_GetAccountByCustAndAccIdAsync()
        {
            var account = await _account.GetAccountByCustAndAccIdAsync(1, 11);

            Assert.NotNull(account);
        }
    }
}
