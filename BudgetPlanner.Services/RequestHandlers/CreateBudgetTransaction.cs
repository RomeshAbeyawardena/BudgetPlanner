using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using BudgetPlanner.Domains.Requests;
using BudgetPlanner.Domains.Responses;
using DNI.Shared.Contracts;
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
        private readonly ITransactionLedgerService _transactionLedgerService;
        private readonly IMapperProvider _mapperProvider;
        public async Task<CreateTransactionResponse> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var transaction = _mapperProvider.Map<CreateTransactionRequest, Transaction>(request);

            var lastTransaction = await _transactionService.GetLastTransaction(transaction.BudgetId);

            var transactionLedger = await _transactionLedgerService.SaveTransactionLedger(new TransactionLedger { 
                TransactionId = lastTransaction?.Id, 
                OldAmount = lastTransaction?.Amount ?? 0,
                NewAmount = transaction.Amount
            }, false);

            if(transaction.TransactionLedgers == null)
                transaction.TransactionLedgers = new List<TransactionLedger>();

            transaction.TransactionLedgers.Add(transactionLedger);

            transaction = await _transactionService.SaveTransaction(transaction);

            return new CreateTransactionResponse { IsSuccessful = true, Transaction = transaction };
        }

        public CreateBudgetTransaction(IMapperProvider mapperProvider, ITransactionService transactionService, ITransactionLedgerService transactionLedgerService)
        {
            _mapperProvider = mapperProvider;
            _transactionService = transactionService;
            _transactionLedgerService = transactionLedgerService;
        }
    }
}
