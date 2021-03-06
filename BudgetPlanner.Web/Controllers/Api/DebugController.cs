﻿using BudgetPlanner.Domains.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers.Api
{
    public class DebugController : DefaultApiController
    {
        public DebugController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> GenerateToken(CancellationToken cancellationToken)
        {
            #if !DEBUG
                return NotFound();
            #endif

            await Task.CompletedTask;
            var queryValues = Request.Query
                .ToDictionary(property => property.Key, property => property.Value.FirstOrDefault());

            var issuer = string.Format("{0}://{1}", Request.Scheme, Request.Host.Value);

            return Ok(await GenerateToken(issuer, issuer, queryValues, cancellationToken));
        }
    }
}
