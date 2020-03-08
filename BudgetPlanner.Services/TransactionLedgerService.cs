using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Core.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class TransactionLedgerService : ITransactionLedgerService
    {
        private readonly IRepository<TransactionLedger> _transactionLedgerRepository;

        public async Task<TransactionLedger> SaveTransactionLedger(TransactionLedger transactionLedger, bool saveChanges, CancellationToken cancellationToken)
        {
            return await _transactionLedgerRepository.SaveChanges(transactionLedger, saveChanges);
        }

        public async Task<TransactionLedger> GetTransactionLedger(int transactionId, CancellationToken cancellationToken)
        {
            var transactionLedgerQuery = from transactionLedger in _transactionLedgerRepository.Query(enableTracking: false)
                                         where transactionLedger.TransactionId == transactionId
                                         select transactionLedger;

            return await _transactionLedgerRepository.For(transactionLedgerQuery).ToSingleOrDefaultAsync(cancellationToken);
        }

        public TransactionLedgerService(IRepository<TransactionLedger> transactionLedgerRepository)
        {
            _transactionLedgerRepository = transactionLedgerRepository;
        }
    }
}
