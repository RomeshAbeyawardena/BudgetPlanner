﻿using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class BudgetPanelDashboardListViewModel
    {
        [JsonIgnore]
        public int AccountId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
