using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using backend.Models;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        [HttpGet]
        public Task<IEnumerable<Account>> ListAaccounts(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{accountId}")]
        public Task<Account> GetAccount([FromRoute] Guid accountId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
