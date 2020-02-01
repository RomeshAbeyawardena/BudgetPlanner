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
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public byte[] EmailAddress { get; set; }
        public byte[] Password { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        
        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        
        [Modifier(ModifierFlag.Modified)]
        public DateTimeOffset? Modified { get; set; }
    }
}
