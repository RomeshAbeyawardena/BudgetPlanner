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
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
        public bool Active { get; set; }
    }
}
