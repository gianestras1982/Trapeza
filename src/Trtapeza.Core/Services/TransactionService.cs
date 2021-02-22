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
using Trapeza.Core.Services.ManualClasses;

namespace Trapeza.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TrapezaDBContext _dBContext;
        private IAccountService _account;
        private ICustomerService _customer;

        public TransactionService(TrapezaDBContext dBContext, IAccountService account, ICustomerService customer)
        {
            _dBContext = dBContext;
            _account = account;
            _customer = customer;
        }

        public async Task<Result<Transaction>> RegisterTranactionAsync(int accountId, RegisterTransactionOptions options)
        {
            if (options.Amount < 0)
                return new Result<Transaction>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"To poso pou edoses einai < 0."
                };

            var account = (await _account.GetAccountByIdAsync(accountId)).Data;

            if (options.Type == TransactionType.Debit)
            { 
                if (options.Amount > account.Balance)
                {
                    return new Result<Transaction>()
                    {
                        Code = ResultCodes.BadRequest,
                        Message = $"Thes na xreoseis {options.Amount} evro eno o logariamos exei {account.Balance}. Akiro."
                    };
                }
            }

            if (account != null)
            {
                var transaction = new Transaction()
                {
                    Type = options.Type,
                    Amount = options.Amount
                };



                account.Transactions.Add(transaction);

                account.Balance = account.Balance + ((int) options.Type) * options.Amount;

                _dBContext.Update<Account>(account);
                await _dBContext.SaveChangesAsync();

                return new Result<Transaction>()
                {
                    Code = ResultCodes.Success,
                    Data = transaction
                };
            }
            else
            {
                return new Result<Transaction>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den vrethike account me kodiko {accountId}"
                };
            }

        }

        public async Task<Result<Transaction>> GetTransactionByIdAsync(int transactionId)
        {
            var transaction = await _dBContext.Set<Transaction>()
                              .Where(t => t.TransactionId == transactionId)
                              .SingleOrDefaultAsync();

            if (transaction != null)
            {
                return new Result<Transaction>()
                {
                    Code = ResultCodes.Success,
                    Data = transaction
                };
            }
            else
            {
                return new Result<Transaction>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei transactionId me kodiko {transactionId}."
                };

            }
        }

        public async Task<Result<List<Transaction>>> GetAllTransByCustmrIdAsync(int customerId)
        {
            var customer = await _customer.GetCustomerByIdAsync(customerId);

            if (customer.Data != null)
            {
                List<Transaction> listTrans = new List<Transaction>();

                foreach (Account a in customer.Data.Accounts)
                {
                    var transactions = (await _account.GetAccountByIdAsync(a.AccountId)).Data.Transactions;

                    listTrans.AddRange(transactions);
                }

                return new Result<List<Transaction>>()
                {
                    Code = ResultCodes.Success,
                    Data = listTrans
                };                
            }
            else
            {
                return new Result<List<Transaction>>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei customer me kodiko {customerId}"                    
                };
            }
        }

        public async Task<Result<List<CountTrnsofAcc>>> GetCountTrnsofAccbyCustmrIdAsync(int customerId)
        {
            var customer = (await _customer.GetCustomerByIdAsync(customerId)).Data;

            if (customer != null)
            {
                List<CountTrnsofAcc> listCnt = new List<CountTrnsofAcc>();
                foreach (Account a in customer.Accounts)
                {
                    var cnt = new CountTrnsofAcc();
                    cnt.AccountNumber = a.AccountNumber;
                    cnt.Counter = (await _account.GetAccountByIdAsync(a.AccountId)).Data.Transactions.Count;

                    listCnt.Add(cnt);
                }

                return new Result<List<CountTrnsofAcc>>()
                {
                    Code = ResultCodes.Success,
                    Data = listCnt
                };
            }
            else
            {
                return new Result<List<CountTrnsofAcc>>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei customer me kodiko {customerId}"
                };
            }

        }
    }


}