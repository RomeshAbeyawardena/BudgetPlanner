using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class TransactionLedgerService : ITransactionLedgerService
    {
        private readonly IRepository<TransactionLedger> _transactionLedgerRepository;

        public async Task<TransactionLedger> SaveTransactionLedger(TransactionLedger transactionLedger, bool saveChanges = true)
        {
            return await _transactionLedgerRepository.SaveChanges(transactionLedger, saveChanges);
        }

        public async Task<TransactionLedger> GetTransactionLedger(int transactionId)
        {
            var transactionLedgerQuery = from transactionLedger in _transactionLedgerRepository.Query(enableTracking: false)
                                         where transactionLedger.TransactionId == transactionId
                                         select transactionLedger;

            return await transactionLedgerQuery.SingleOrDefaultAsync();
        }

        public TransactionLedgerService(IRepository<TransactionLedger> transactionLedgerRepository)
        {
            _transactionLedgerRepository = transactionLedgerRepository;
        }
    }
}
