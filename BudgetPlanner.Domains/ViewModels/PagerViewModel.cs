using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class PagerViewModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public string PreviousPageUrl { get; set;}
        public string NextPageUrl { get; set; }
        public string SelectPageUrl { get; set; }
    }
}
