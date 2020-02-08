using BudgetPlanner.Domains.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class DebugController : DefaultApiController
    {
        public DebugController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> GenerateToken()
        {
            #if !DEBUG
                return NotFound();
            #endif

            await Task.CompletedTask;
            var queryValues = Request.Query
                .ToDictionary(property => property.Key, property => property.Value.FirstOrDefault());

            return Ok(await GenerateToken(Request.Host.Value, queryValues));
        }
    }
}
