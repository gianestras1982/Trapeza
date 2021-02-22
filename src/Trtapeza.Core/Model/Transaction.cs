using System;

using Trapeza.Core.Model.Types;

namespace Trapeza.Core.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime Created { get; private set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }

        public Transaction()
        {
            Created = DateTime.Now;
        }
    }
}
