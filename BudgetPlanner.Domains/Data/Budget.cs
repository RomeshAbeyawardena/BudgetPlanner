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
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public bool Active { get; set; }
        
        [Modifier(ModifierFlag.Created|ModifierFlag.Modified)]
        public DateTimeOffset? LastUpdated { get; set; }
        
        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
    }
}
