using backend.Models;
using backend.Models.Api;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private IAccountingService _accountingService;

        public TransactionsController(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        [HttpGet]
        public Task<IEnumerable<Transaction>> ListTransactions(CancellationToken ct)
        {
            return _accountingService.ListTransactions(ct);
        }

        [HttpPost]
        public Task<Transaction> CreateTransaction([FromBody] CreateTransactionRequestDto request, CancellationToken ct)
        {
            return _accountingService.CreateTransaction(request.AccountId, request.Amount, ct);
        }

        [HttpGet("{transactionId}")]
        public Task<Transaction> GetTransaction([FromRoute] Guid transactionId, CancellationToken ct)
        {
            return _accountingService.GetTransaction(transactionId, ct);
        }
    }
}
