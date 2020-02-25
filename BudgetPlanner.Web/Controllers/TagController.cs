using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using BudgetPlanner.Domains.ViewModels;
using DNI.Shared.Contracts.Providers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseHelper = DNI.Shared.Domains.Response;

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
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = Map<SaveTagViewModel, SaveTagRequest>(model);

            var response = await MediatorService.Send(request);

            if(!ResponseHelper.IsSuccessful(response))
                return BadRequest();

            return Ok(new { response.Result, response.Created } );
        }


        public TagController(IClockProvider clockProvider)
        {
            _clockProvider = clockProvider;
        }
    }
}
