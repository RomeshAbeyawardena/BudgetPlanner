using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class DialogViewModel
    {
        public string HeaderContent { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
