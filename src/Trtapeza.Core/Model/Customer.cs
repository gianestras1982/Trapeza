using System;
using System.Collections.Generic;

using Trapeza.Core.Model.Types;

namespace Trapeza.Core.Model
{

    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustBankID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string SurName { get; set; }
        public string VatNumber { get; set; }
        public bool Active { get; set; } = true;
        public string Address { get; set; }
        public CustomerType CustType { get; set; }
        public DateTime Created { get; private set; }

        public List<Account> Accounts { get; set; }

        public Customer()
        {
            Created = DateTime.Now;
            Accounts = new List<Account>();
        }
    }
}
