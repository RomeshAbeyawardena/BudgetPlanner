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
    public class TransactionType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset Modified { get; set; }
    }
}
