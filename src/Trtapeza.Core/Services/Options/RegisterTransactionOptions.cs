using Trapeza.Core.Model.Types;

namespace Trapeza.Core.Services.Options
{
    public class RegisterTransactionOptions
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
