using Trapeza.Core.Model;
using Trapeza.Core.Model.Types;
using Trapeza.Core.Services.Interfaces;
using Trapeza.Core.Services.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Trapeza.Core.Tests
{
    public class TransactionTests : IClassFixture<TrapezaFixture>
    {
        private ITransactionService _transaction;

        public TransactionTests(TrapezaFixture fixture)
        {
            _transaction = fixture.Scope.ServiceProvider.GetRequiredService<ITransactionService>();
        }


        [Fact]
        public async void Test_RegisterTranactionAsync()
        {
            var options = new RegisterTransactionOptions()
            {
                Type = TransactionType.Credit,
                Amount = 2600
            };

            var transaction = await _transaction.RegisterTranactionAsync(3, options);

            Assert.NotNull(transaction);
                
        }


        [Fact]
        public async void Test_GetTransactionByIdAsync()
        {
            var transaction = await _transaction.GetTransactionByIdAsync(9);

            Assert.NotNull(transaction);
        }


        [Fact]
        public async void Test_GetAllTransByCustmrIdAsync()
        {
            var transactions = await _transaction.GetAllTransByCustmrIdAsync(1);

            Assert.NotNull(transactions);
        }

        [Fact]
        public async void Test_GetCountTrnsofAccbyCustmrIdAsync()
        {
            var cntTrnsAcc = await _transaction.GetCountTrnsofAccbyCustmrIdAsync(3);

            Assert.NotNull(cntTrnsAcc);
        }
    }

}