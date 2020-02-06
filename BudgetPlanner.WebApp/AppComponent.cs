using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Contracts;
using BudgetPlanner.Domains.State;
using Microsoft.AspNetCore.Components;

namespace BudgetPlanner.WebApp
{
    public class AppComponent : ComponentBase, IStateBase
    {
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
    }
}
