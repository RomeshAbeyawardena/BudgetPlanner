﻿using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class RetrieveBudgetPlannerResponse
    {
        public bool IsSuccessful { get; set; }
        public Budget BudgetPlanner { get; set; }
        public decimal OldAmount { get; set; }
        public decimal Amount { get; set; }
    }
}
