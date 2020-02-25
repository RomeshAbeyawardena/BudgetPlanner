using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts.Providers;
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
        private readonly IClockProvider _clockProvider;

        [HttpGet]
        public async Task<ActionResult> GetTags(string searchTerm)
        {
            var response = await MediatorService.Send(new RetrieveTagsRequest { SearchTerm = searchTerm });
            if(response.IsSuccessful)
                return Json( new { response.Result, requested = _clockProvider.DateTimeOffset  });

            return BadRequest();
        }

        
        [HttpPost]
        public async Task<ActionResult> SaveTag([FromForm]SaveTagViewModel model)
        {

        }


        public TagController(IClockProvider clockProvider)
        {
            _clockProvider = clockProvider;
        }
    }
}
