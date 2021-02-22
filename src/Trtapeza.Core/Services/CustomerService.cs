using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Trapeza.Core.Consts;
using Trapeza.Core.Data;
using Trapeza.Core.Model;
using Trapeza.Core.Services.Interfaces;
using Trapeza.Core.Services.Options;
using System.Collections.Generic;
using System.IO;
using Npoi.Mapper;
using Trapeza.Core.Model.Types;

namespace Trapeza.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly TrapezaDBContext _dbContext;

        public CustomerService(TrapezaDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<Customer>> RegisterCustomerAsync(RegisterCustomerOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Name))
                return new Result<Customer>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Customer name is empty!"
                };

            if (string.IsNullOrWhiteSpace(options.SurName))
                return new Result<Customer>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Customer sure name is empty!"
                };

            if (options.VatNumber.Length != 9)
                return new Result<Customer>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"VAT Number length is invalid!"
                };

            long vatNumber = 0;
            if (!long.TryParse(options.VatNumber, out vatNumber))
                return new Result<Customer>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"VAT number must be numeric!"
                };

            var customer = new Customer()
            {
                Name = options.Name,
                SurName = options.SurName,
                VatNumber = options.VatNumber,
                CustType = options.CustType,
                Address = options.Address
            };

            await _dbContext.AddAsync<Customer>(customer);
            await _dbContext.SaveChangesAsync();

            return new Result<Customer>()
            {
                Code = ResultCodes.Success,
                Data = customer
            };
        }

        public async Task<Result<Customer>> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _dbContext.Set<Customer>()
                           .Where(c => c.CustomerId == customerId)
                           .Include(c => c.Accounts)
                           .SingleOrDefaultAsync();

            if (customer != null)
            {
                return new Result<Customer>()
                {
                    Code = ResultCodes.Success,
                    Data = customer
                };
            }
            else
            {
                return new Result<Customer>()
                {
                    Code = ResultCodes.NotFound,
                    Message = $"Den vrethike pelatis me kodiko {customerId}"
                };
            }
        }

        public async Task<Result<Customer>> GetAllInOneCustAsync(int customerId)
        {
            var customer = await _dbContext.Set<Customer>()
                           .Where(c => c.CustomerId == customerId)
                           .Include(c => c.Accounts)
                           .SingleOrDefaultAsync();
            

            if (customer != null)
            {
                foreach (Account acnt in customer.Accounts)
                {
                    var account = await _dbContext.Set<Account>()
                                   .Where(a => a.AccountId == acnt.AccountId)
                                   .Include(t => t.Transactions)
                                   .SingleOrDefaultAsync();
                }

                return new Result<Customer>()
                {
                    Code = ResultCodes.Success,
                    Data = customer
                };
            }
            else
            {
                return new Result<Customer>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Den iparxei o customer me kodiko {customerId}"
                };
            }

        }

    }

}