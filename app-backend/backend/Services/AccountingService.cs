﻿using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .OrderByDescending(tx => tx.CreatedAt)
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
                    Balance = 0,
                    Transactions = new List<Transaction>()
                };

                await _dbContext.Accounts.AddAsync(account, ct);
            }

            var newTransaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Account = account,
                Amount = amount,
                CreatedAt = DateTimeOffset.Now
            };

            account.Balance += newTransaction.Amount;

            await _dbContext.Transactions.AddAsync(newTransaction, ct);
            await _dbContext.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            return newTransaction;
        }

        public async Task<Transaction> GetTransaction(Guid transactionId, CancellationToken ct)
        {
            return await _dbContext.Transactions
                .Include(tx => tx.Account)
                .FirstOrDefaultAsync(tx => tx.Id == transactionId, ct);
        }

    }
}
