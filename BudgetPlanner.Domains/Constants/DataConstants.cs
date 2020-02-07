using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Constants
{
    public static class DataConstants
    {
        public const string AccountSessionCookie = "accSession";
        public const string DefaultConnectionStringKey = "Default";
        public const string AccountIdClaim = "AccountId";
        public const string BudgetPlannerIdClaim = "BudgetPlannerId";
        public const string BudgetPlannerReferenceClaim = "BudgetPlannerReference";
        public const string PageSizeClaim = "PageSize";
        public const string PageNumberClaim = "PageNumber";
        public const string AccountItem = nameof(AccountItem);
    }
}
