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
    public class SaveTransactionTags : IRequestHandler<SaveTransactionTagsRequest, SaveTransactionTagsResponse>
    {
        private readonly IBudgetPlannerCacheProvider _budgetPlannerCacheProvider;
        private readonly ITagService _tagService;

        public async Task<SaveTransactionTagsResponse> Handle(SaveTransactionTagsRequest request, CancellationToken cancellationToken)
        {
            var savedBudgetTags = new List<TransactionTag>();
            var tags = await _budgetPlannerCacheProvider.GetTags();
            var parsedTags = _tagService
                .ParseTags(tags, request.Tags.Split(','))
                .ToArray();

            var transactionTags = await _tagService.GetTransactionTags(request.TransactionId);

            for (var index=0; index<parsedTags.Length; index++)
            {
                var currentTag = parsedTags[index];

                if(currentTag.Id != default)
                    currentTag = await _tagService
                        .SaveTag(currentTag, false);

                if(transactionTags
                    .Any(budgetTag => budgetTag.TagId == currentTag.Id))
                    continue;

                var savedTransactionTag = await _tagService.SaveTransactionTag(new TransactionTag { 
                    Tag = currentTag, 
                    TransactionId = request.TransactionId 
                });


            }

            return Response.Success<SaveTransactionTagsResponse>(savedBudgetTags.ToArray());
        }
        
        public SaveTransactionTags(IBudgetPlannerCacheProvider budgetPlannerCacheProvider, ITagService tagService)
        {
            _budgetPlannerCacheProvider = budgetPlannerCacheProvider;
            _tagService =  tagService;
        }
    }
}
