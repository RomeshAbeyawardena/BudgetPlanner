using DNI.Shared.Contracts.Enumerations;
using DNI.Shared.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class TransactionLedger
    {
        [Key]
        public int Id { get; set; }
        public int? TransactionId { get; set; }
        
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PreviousBalance { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal NewBalance { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
    }
}
