﻿using DNI.Core.Contracts.Enumerations;
using DNI.Core.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Data
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public bool Active { get; set; }
        public int BudgetId { get; set; }
        public string Description { get; set; }

        [Modifier(ModifierFlag.Created)]
        public DateTimeOffset Created { get; set; }
        
        public int TransactionTypeId { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Amount { get; set; }

        [NotMapped]
        public Enumerations.TransactionType Type => (Enumerations.TransactionType)TransactionTypeId;
        public virtual Budget Budget { get; set; }
        public virtual ICollection<TransactionLedger> TransactionLedgers {  get; set; }
    }
}
