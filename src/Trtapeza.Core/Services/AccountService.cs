using Trapeza.Core.Consts;
using Trapeza.Core.Data;
using Trapeza.Core.Model;
using Trapeza.Core.Model.Types;
using Trapeza.Core.Services.Interfaces;
using Trapeza.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapeza.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly TrapezaDBContext _dBContext;
        private ICustomerService _customer;
        private ITransactionService _transaction;

        public AccountService(TrapezaDBContext dBContext, ICustomerService customer)
        {
            _dBContext = dBContext;
            _customer = customer;

        }

        public async Task<Result<Account>> RegisterAccountAsync(int customerId, RegisterAccountOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Currency))
                return new Result<Account>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Currency is empty!"
                };

            if (string.IsNullOrWhiteSpace(options.Description))
                return new Result<Account>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Einai ipoxreotiko na doseis perigrafi"
                };


            var customer = (await _customer.GetCustomerByIdAsync(customerId)).Data;

            if (customer != null)
            {
                var account = new Account()
                {
                    Currency = options.Currency,
                    Description = options.Description
                    
                };

                customer.Accounts.Add(account);

                _dBContext.Update<Customer>(customer);
                await _dBContext.SaveChangesAsync();

                return new Result<Account>()
                {
                    Code = ResultCodes.Success,
                    Data = account
                };
            }
            else
            {
                return new Result<Account>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei o pelatis me kodiko {customerId}."
                };
            }
                           

        }

        public async Task<Result<Account>> GetAccountByIdAsync(int accountId)
        {
            var account = await _dBContext.Set<Account>()
                          .Where(a => a.AccountId == accountId)
                          .Include(a => a.Transactions)
                          .SingleOrDefaultAsync();

            if (account != null)
            {
                return new Result<Account>()
                {
                    Code = ResultCodes.Success,
                    Data = account
                };
            }
            else
            {
                return new Result<Account>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den ipaxei account me kodiko {accountId}."
                };
            }

        }

        public async Task<Result<Account>> UpdateAccountStateTypeAsync(int accountId, AccountStateType options)
        {
            var account = (await GetAccountByIdAsync(accountId)).Data;

            if (account != null)
            {
                account.State = options;

                _dBContext.Update<Account>(account);
                await _dBContext.SaveChangesAsync();

                return new Result<Account>()
                {
                    Code = ResultCodes.Success,
                    Data = account
                };
            }
            else
            {
                return new Result<Account>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei accountId me kodiko {accountId}"
                };
            }
       
        }

        public async Task<Result<Account>> GetAccountByCustAndAccIdAsync(int customerId, int accountId)
        {
            var customer = (await _customer.GetCustomerByIdAsync(customerId)).Data;

            if (customer != null)
            {
                //var account = (await GetAccountByIdAsync(accountId)).Data;
                var account = customer.Accounts
                              .Where(a => a.AccountId == accountId)
                              .SingleOrDefault();

                if (account != null)
                {
                    return new Result<Account>()
                    {
                        Code = ResultCodes.Success,
                        Data = account
                    };
                }
                else
                {
                    return new Result<Account>()
                    {
                        Code = ResultCodes.BadRequest,
                        Message = $"Den iparxei account me kodiko {accountId}."
                    };
                }
            }
            else
            {
                return new Result<Account>
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei customer me kodiko {customerId}"
                };
            }           
        }
    }

}