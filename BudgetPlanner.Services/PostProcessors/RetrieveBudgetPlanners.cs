﻿using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.PostProcessors
{
    public class RetrieveBudgetPlanners : IRequestPostProcessor<RetrieveBudgetPlannersRequest, RetrieveBudgetPlannersResponse>
    {
        private readonly ITransactionService _transactionService;

        public async Task Process(RetrieveBudgetPlannersRequest request, RetrieveBudgetPlannersResponse response, CancellationToken cancellationToken)
        {
            foreach (var budget in response.BudgetPlanners)
                budget.Balance = await _transactionService.GetBalance(budget.Id);

        }

        public RetrieveBudgetPlanners(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
    }
}
