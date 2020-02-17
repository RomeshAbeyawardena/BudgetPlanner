using DNI.Shared.Contracts.Enumerations;
using DNI.Shared.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class BudgetLimit
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool Active { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }

        public Budget Budget { get; set; }
    }
}
