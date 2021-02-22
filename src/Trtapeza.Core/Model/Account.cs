using System;
using System.Collections.Generic;
using Trapeza.Core.Model.Types;

namespace Trapeza.Core.Model
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime Created { get; private set; }
        public AccountStateType State { get; set; } = AccountStateType.Inactive;

        public List<Transaction> Transactions { get; set; }

        public Account()
        {
            Created = DateTime.Now;
            Transactions = new List<Transaction>();
        }
    }
}
