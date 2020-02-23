using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
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
    public class RetrieveTags : IRequestHandler<RetrieveTagsRequest, RetrieveTagsResponse>
    {
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;
        private readonly ITagService _tagService;

        public async Task<RetrieveTagsResponse> Handle(RetrieveTagsRequest request, CancellationToken cancellationToken)
        {
            var tags = await _budgetPlannerCacheProvider.GetTags();
            return Response.Success<RetrieveTagsResponse>(
                _tagService.SearchTags(tags, request.SearchTerm));
        }

        public RetrieveTags(ITagService tagService, IBudgetPlannerCacheProvider budgetPlannerCacheProvider)
        {
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;
            _tagService = tagService;
        }
    }
}
