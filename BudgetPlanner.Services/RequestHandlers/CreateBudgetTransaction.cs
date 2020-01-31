using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts;
using DNI.Shared.Contracts.Enumerations;
using DNI.Shared.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
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
        private readonly IMapperProvider _mapperProvider;
        private readonly IModifierFlagPropertyService _modifierFlagPropertyService;

        public async Task<CreateTransactionResponse> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = _mapperProvider.Map<CreateTransactionRequest, Transaction>(request);

            var lastTransaction = await _transactionService.GetLastTransaction(transaction.BudgetId);

            var previousBalance = await _transactionProvider.GetBalance(transaction.BudgetId, true);

            var transactionLedger = new TransactionLedger
            {
                TransactionId = lastTransaction?.Id,
                Amount = transaction.Type == Domains.Enumerations.TransactionType.Expense 
                    ? -transaction.Amount
                    : transaction.Amount,
                PreviousBalance = previousBalance,
                NewBalance = transaction.Type == Domains.Enumerations.TransactionType.Expense 
                ? previousBalance - transaction.Amount
                : previousBalance + transaction.Amount
            };

            //_modifierFlagPropertyService.SetModifierFlagValues(transactionLedger, ModifierFlag.Created);

            transactionLedger = await _transactionLedgerService
                .SaveTransactionLedger(transactionLedger, false);

            if(transaction.TransactionLedgers == null)
                transaction.TransactionLedgers = new List<TransactionLedger>();

            transaction.TransactionLedgers.Add(transactionLedger);

            transaction = await _transactionService.SaveTransaction(transaction);

            return new CreateTransactionResponse { IsSuccessful = true, Transaction = transaction };
        }

        public CreateBudgetTransaction(IMapperProvider mapperProvider,
            IModifierFlagPropertyService modifierFlagPropertyService,
            ITransactionService transactionService, 
            ITransactionProvider transactionProvider,
            ITransactionLedgerService transactionLedgerService)
        {
            _mapperProvider = mapperProvider;
            _modifierFlagPropertyService = modifierFlagPropertyService;
            _transactionService = transactionService;
            _transactionProvider = transactionProvider;
            _transactionLedgerService = transactionLedgerService;
        }
    }
}
