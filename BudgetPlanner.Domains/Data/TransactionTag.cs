using DNI.Shared.Contracts.Enumerations;
using DNI.Shared.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class TransactionTag
    {
        [Key]
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int TagId { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
