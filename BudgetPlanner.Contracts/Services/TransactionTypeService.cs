using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly IRepository<TransactionType> _transactionTypeRepository;

        public async Task<IEnumerable<TransactionType>> GetTransactionTypes(CancellationToken cancellationToken)
        {
            var transactionTypeQuery = from transactionType in _transactionTypeRepository.Query()
                                       select transactionType;

            return await _transactionTypeRepository
                .For(transactionTypeQuery)
                .ToArrayAsync(cancellationToken);
        }

        public TransactionTypeService(IRepository<TransactionType> transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        }
    }
}
