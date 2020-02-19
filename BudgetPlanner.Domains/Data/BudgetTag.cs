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
    public class BudgetTag
    {
        [Key]
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public int TagId { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
