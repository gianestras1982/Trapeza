using System.Collections.Generic;
using System.Threading.Tasks;

using Trapeza.Core.Model;
using Trapeza.Core.Model.Types;
using Trapeza.Core.Services.Options;

namespace Trapeza.Core.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<Result<Account>> RegisterAccountAsync(int customerId, RegisterAccountOptions options);
        public Task<Result<Account>> GetAccountByIdAsync(int accountId);
        public Task<Result<Account>> UpdateAccountStateTypeAsync(int accountId, AccountStateType options);
        public Task<Result<Account>> GetAccountByCustAndAccIdAsync(int customerId, int accountId);
    }
}