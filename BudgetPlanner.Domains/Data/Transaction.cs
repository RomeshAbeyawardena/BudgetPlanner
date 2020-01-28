using DNI.Shared.Domains.Enumerations;
using DNI.Shared.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public bool Active { get; set; }
        public int BudgetId { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        
        public int TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
    }
}
