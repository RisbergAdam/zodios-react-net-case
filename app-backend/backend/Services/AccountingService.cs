using backend.Models;
using backend.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface IAccountingService
    {
        public Task<IEnumerable<Account>> ListAccounts(CancellationToken ct);

        public Task<Account> GetAccount(Guid accountId, CancellationToken ct);

        public Task<IEnumerable<Transaction>> ListTransactions(CancellationToken ct);

        public Task<Transaction> CreateTransaction(Guid accountId, int amount, CancellationToken ct);

        public Task<Transaction> GetTransaction(Guid transactionId, CancellationToken ct);
    }

    public class AccountingService : IAccountingService
    {

        private AccountingContext _dbContext;

        public AccountingService(AccountingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Account>> ListAccounts(CancellationToken ct)
        {
            return await _dbContext.Accounts.ToListAsync(ct);
        }


        public async Task<Account> GetAccount(Guid accountId, CancellationToken ct)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == accountId, ct);
        }

        public async Task<IEnumerable<Transaction>> ListTransactions(CancellationToken ct)
        {
            return await _dbContext.Transactions
                .Include(tx => tx.Account)
                .ToListAsync(ct);
        }

        public async Task<Transaction> CreateTransaction(Guid accountId, int amount, CancellationToken ct)
        {
            // Use transaction to prevent concurrent updates to account balance.
            using var tx = await _dbContext.Database.BeginTransactionAsync(ct);
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == accountId, ct);

            if (account == null)
            {
                account = new Account
                {
                    Id = accountId,
                    Balance = amount,
                    Transactions = new List<Transaction>()
                };

                await _dbContext.Accounts.AddAsync(account, ct);
            }

            var newTransaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                CreatedAt = DateTimeOffset.Parse("2024-02-04T15:54:15Z")
            };

            account.Balance += newTransaction.Amount;
            account.Transactions.Add(newTransaction);

            await _dbContext.Transactions.AddAsync(newTransaction, ct);
            await _dbContext.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            return newTransaction;
        }

        [HttpGet("{transactionId}")]
        public async Task<Transaction> GetTransaction(Guid transactionId, CancellationToken ct)
        {
            return await _dbContext.Transactions
                .Include(tx => tx.Account)
                .FirstOrDefaultAsync(tx => tx.Id == transactionId, ct);
        }

    }
}
