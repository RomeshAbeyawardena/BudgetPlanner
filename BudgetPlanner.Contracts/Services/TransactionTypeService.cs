using BudgetPlanner.Domains.Data;
using DNI.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly IRepository<TransactionType> _transactionTypeRepository;

        public async Task<IEnumerable<TransactionType>> GetTransactionTypes()
        {
            var transactionTypeQuery = from transactionType in _transactionTypeRepository.Query()
                                       select transactionType;

            return await transactionTypeQuery.ToArrayAsync();
        }

        public TransactionTypeService(IRepository<TransactionType> transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        }
    }
}
