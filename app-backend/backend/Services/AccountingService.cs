using backend.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Account>> ListAccounts(CancellationToken ct)
        {
            return new List<Account>
            {
                new() {
                    AccountId = Guid.Parse("53ee34b0-d558-4fbf-ae78-6e85c9ded528"),
                    Balance = 1000
                }
            };
        }


        public async Task<Account> GetAccount(Guid accountId, CancellationToken ct)
        {
            return new()
            {
                AccountId = accountId,
                Balance = 1000
            };
        }

        public async Task<IEnumerable<Transaction>> ListTransactions(CancellationToken ct)
        {
            return new List<Transaction>
            {
                new() {
                    TransactionId = Guid.Parse("c7b5118b-5272-4d26-bdee-34a4a7b876e2"),
                    AccountId = Guid.Parse("53ee34b0-d558-4fbf-ae78-6e85c9ded528"),
                    Amount = 1000,
                    CreatedAt = DateTimeOffset.Parse("2024-02-04T15:54:15Z")
                }
            };
        }

        public async Task<Transaction> CreateTransaction(Guid accountId, int amount, CancellationToken ct)
        {
            return new()
            {
                TransactionId = Guid.Parse("c7b5118b-5272-4d26-bdee-34a4a7b876e2"),
                AccountId = accountId,
                Amount = amount,
                CreatedAt = DateTimeOffset.Parse("2024-02-04T15:54:15Z")
            };
        }

        [HttpGet("{transactionId}")]
        public async Task<Transaction> GetTransaction(Guid transactionId, CancellationToken ct)
        {
            return new()
            {
                TransactionId = transactionId,
                AccountId = Guid.Parse("53ee34b0-d558-4fbf-ae78-6e85c9ded528"),
                Amount = 1000,
                CreatedAt = DateTimeOffset.Parse("2024-02-04T15:54:15Z")
            };
        }

    }
}
