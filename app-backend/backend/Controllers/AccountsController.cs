using backend.Models.Api;
using backend.Services;
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
    public class AccountsController : Controller
    {

        private IAccountingService _accountingService;

        public AccountsController(IAccountingService accountingService)
        {
            _accountingService = accountingService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountDto>> ListAaccounts(CancellationToken ct)
        {
            var accounts = await _accountingService.ListAccounts(ct);
            return accounts.Select(acc => new AccountDto
            {
                AccountId = acc.Id,
                Balance = acc.Balance
            });
        }

        [HttpGet("{accountId}")]
        public async Task<AccountDto> GetAccount([FromRoute] Guid accountId, CancellationToken ct)
        {
            var acc = await _accountingService.GetAccount(accountId, ct);
            return acc == null ? null : new AccountDto
            {
                AccountId = acc.Id,
                Balance = acc.Balance
            };
        }
    }
}
