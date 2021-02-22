using System.Collections.Generic;
using System.Threading.Tasks;

using Trapeza.Core.Model;
using Trapeza.Core.Services.ManualClasses;
using Trapeza.Core.Services.Options;

namespace Trapeza.Core.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<Result<Transaction>> RegisterTranactionAsync(int accountId, RegisterTransactionOptions options);
        public Task<Result<Transaction>> GetTransactionByIdAsync(int transactionId);
        public Task<Result<List<Transaction>>> GetAllTransByCustmrIdAsync(int customerId);
        public Task<Result<List<CountTrnsofAcc>>> GetCountTrnsofAccbyCustmrIdAsync(int customerId);
    }
}