using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Domains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class SaveTag : IRequestHandler<SaveTagRequest, SaveTagResponse>
    {
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;
        private readonly ITagService _tagService;

        public async Task<SaveTagResponse> Handle(SaveTagRequest request, CancellationToken cancellationToken)
        {
            var foundTag = _tagService.GetTag(await _budgetPlannerCacheProvider.GetTags(), request.Name);

            if(foundTag != null)
                return Response.Success<SaveTagResponse>(foundTag);

            foundTag = await _tagService
                .SaveTag(new Tag { Id = request.Id, Name = request.Name }, true);

            return Response.Success<SaveTagResponse>(foundTag, config => config.Created = true);
        }

        public SaveTag(IBudgetPlannerCacheProvider budgetPlannerCacheProvider, ITagService tagService)
        {
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;
            _tagService = tagService;
        }
    }
}
