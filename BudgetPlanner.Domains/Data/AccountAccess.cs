using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class AccountAccess
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int AccessTypeId { get; set; }
        public bool Succeeded { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        public virtual AccessType AccessType { get; set; }
        public bool Active { get; set; }
    }
}
