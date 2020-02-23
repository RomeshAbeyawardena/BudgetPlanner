using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class TagController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetTags(string searchTerm)
        {
            var response = await MediatorService.Send(new RetrieveTagsRequest { SearchTerm = searchTerm });
            if(response.IsSuccessful)
                return Json(response.Result);

            return BadRequest();
        }
    }
}
