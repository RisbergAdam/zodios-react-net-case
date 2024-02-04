using backend.Models;
using backend.Models.Api;
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
        [HttpGet]
        public Task<IEnumerable<Transaction>> ListTransactions(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult<Transaction>> CreateTransaction([FromBody] CreateTransactionRequestDto request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{transactionId}")]
        public Task<Transaction> GetTransaction([FromRoute] Guid transactionId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
