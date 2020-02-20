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
    public class RetrieveTransactionTags : IRequestHandler<RetrieveTransactionTagsRequest, RetrieveTransactionTagsResponse>
    {
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;
        private readonly ITagService _tagService;

        public async Task<RetrieveTransactionTagsResponse> Handle(RetrieveTransactionTagsRequest request, CancellationToken cancellationToken)
        {
            var budgetTags = await _tagService.GetTransactionTags(request.TransactionId);
            var tags = await _budgetPlannerCacheProvider.GetTags();
            return Response.Success<RetrieveTransactionTagsResponse>(budgetTags
                .Select(budgetTag => _tagService.GetTag(tags, budgetTag.TagId)));
        }

        public RetrieveTransactionTags(IBudgetPlannerCacheProvider budgetPlannerCacheProvider, ITagService tagService)
        {
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;
            _tagService =  tagService;
        }
    }
}
