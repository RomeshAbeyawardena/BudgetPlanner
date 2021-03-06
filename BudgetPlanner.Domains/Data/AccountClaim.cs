﻿using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class AccountClaim
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ClaimId { get; set; }
        public string Value { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }

        public virtual Account Account { get; set; }
        public virtual Claim Claim { get; set; }
        public bool Active { get; set; }
    }
}
