using backend.Models;
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
    public class AccountsController : Controller
    {

        private IAccountingService _accountingService;

        public AccountsController(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        [HttpGet]
        public Task<IEnumerable<Account>> ListAaccounts(CancellationToken ct)
        {
            return _accountingService.ListAccounts(ct);
        }

        [HttpGet("{accountId}")]
        public Task<Account> GetAccount([FromRoute] Guid accountId, CancellationToken ct)
        {
            return _accountingService.GetAccount(accountId, ct);
        }
    }
}
