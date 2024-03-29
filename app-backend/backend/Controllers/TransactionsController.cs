using backend.Models.Api;
using backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<TransactionDto>> ListTransactions(CancellationToken ct)
        {
            var transactions = await _accountingService.ListTransactions(ct);
            return transactions.Select(tx => new TransactionDto
            {
                TransactionId = tx.Id,
                AccountId = tx.Account.Id,
                Amount = tx.Amount,
                CreatedAt = tx.CreatedAt
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequestDto request, CancellationToken ct)
        {
            var tx = await _accountingService.CreateTransaction(request.AccountId, request.Amount, ct);
            return Created(tx.Id.ToString(), new TransactionDto
            {
                TransactionId = tx.Id,
                AccountId = tx.Account.Id,
                Amount = tx.Amount,
                CreatedAt = tx.CreatedAt
            });
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransaction([FromRoute] Guid transactionId, CancellationToken ct)
        {
            var tx = await _accountingService.GetTransaction(transactionId, ct);
            if (tx == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new TransactionDto
                {
                    TransactionId = tx.Id,
                    AccountId = tx.Account.Id,
                    Amount = tx.Amount,
                    CreatedAt = tx.CreatedAt
                });
            }
        }
    }
}
