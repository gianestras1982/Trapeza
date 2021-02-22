using Microsoft.EntityFrameworkCore;

using Trapeza.Core.Model;

namespace Trapeza.Core.Data
{
    public class TrapezaDBContext : DbContext
    {

        public TrapezaDBContext(DbContextOptions<TrapezaDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Customer>().HasIndex(c => c.VatNumber).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(c => c.CustBankID).IsUnique();


            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().HasIndex(a => a.AccountNumber).IsUnique();


            modelBuilder.Entity<Transaction>().ToTable("Transaction");

        }
    }
}
