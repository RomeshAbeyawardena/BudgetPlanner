using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Core.Contracts;
using DNI.Core.Contracts.Enumerations;
using DNI.Core.Contracts.Services;
using DNI.Core.Domains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.RequestHandlers
{
    public class CreateBudgetTransaction : IRequestHandler<CreateTransactionRequest, CreateTransactionResponse>
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionProvider _transactionProvider;
        private readonly ITransactionLedgerService _transactionLedgerService;
        private readonly IBudgetPlannerService _budgetPlannerService;
        private readonly IMapperProvider _mapperProvider;

        public async Task<CreateTransactionResponse> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = _mapperProvider.Map<CreateTransactionRequest, Transaction>(request);

            var budgetPlanner = await _budgetPlannerService.GetBudgetPlanner(transaction.BudgetId);
            
            budgetPlanner.LastUpdated = SqlDateTime.MinValue.Value;
            
            var previousBalance = await _transactionProvider.GetBalance(transaction.BudgetId, true);

            if(transaction.Id != default)
            {
                transaction = await _transactionService.SaveTransaction(transaction);
                return Response.Success<CreateTransactionResponse>(transaction);
            }

            transaction = await _transactionService.SaveTransaction(transaction, false);

            var transactionLedger = new TransactionLedger
            {
                Transaction = transaction,
                Amount = transaction.Type == Domains.Enumerations.TransactionType.Expense 
                    ? -transaction.Amount
                    : transaction.Amount,
                PreviousBalance = previousBalance,
                NewBalance = transaction.Type == Domains.Enumerations.TransactionType.Expense 
                    ? previousBalance - transaction.Amount
                    : previousBalance + transaction.Amount
            };

            await _transactionLedgerService
                .SaveTransactionLedger(transactionLedger, true);

            return Response.Success<CreateTransactionResponse>(transaction);
        }

        public CreateBudgetTransaction(IBudgetPlannerService budgetPlannerService, 
            IMapperProvider mapperProvider,
            ITransactionService transactionService, 
            ITransactionProvider transactionProvider,
            ITransactionLedgerService transactionLedgerService)
        {
            _budgetPlannerService = budgetPlannerService;
            _mapperProvider = mapperProvider;
            _transactionService = transactionService;
            _transactionProvider = transactionProvider;
            _transactionLedgerService = transactionLedgerService;
        }
    }
}
