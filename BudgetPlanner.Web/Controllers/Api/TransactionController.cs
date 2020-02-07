using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class TransactionController : DefaultApiController
    {
        public async Task<ActionResult> GetTransactions([Bind(Prefix = "payload")]string token)
        {
            var claims = GetTokenClaims(token);
            return Ok();
        }
    }
}
