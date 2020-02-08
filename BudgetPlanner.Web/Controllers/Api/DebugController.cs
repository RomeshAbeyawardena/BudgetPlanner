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

        public async Task<ActionResult> GenerateToken()
        {
            var queryValues = Request.Query
                .ToDictionary(property => property.Key, property => property.Value.FirstOrDefault());


            return Ok();
        }
    }
}
