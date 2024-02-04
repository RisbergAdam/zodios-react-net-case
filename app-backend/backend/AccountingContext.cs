using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class AccountingContext : DbContext
    {
        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } = null!;

        public DbSet<Transaction> Transactions { get; set; } = null!;

    }
}
