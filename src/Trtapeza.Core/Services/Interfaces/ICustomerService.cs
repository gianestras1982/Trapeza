using System.Collections.Generic;
using System.Threading.Tasks;

using Trapeza.Core.Model;
using Trapeza.Core.Services.Options;

namespace Trapeza.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<Result<Customer>> RegisterCustomerAsync(RegisterCustomerOptions options);
        public Task<Result<Customer>> GetCustomerByIdAsync(int customerId);
        public Task<Result<Customer>> GetAllInOneCustAsync(int customerId);
    }
}