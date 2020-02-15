using BudgetPlanner.Domains.Attributes;
using BudgetPlanner.Domains.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class BaseViewModel
    {
        public bool IsModal { get; set; }
        
        [Content(ContentConstants.Title)]
        public string Title { get; set; }

        [Content(ContentConstants.Content)]
        public string Content { get; set; }

        [Content(ContentConstants.MetaDescription)]
        public string MetaDescription { get; set; }

        [Content(ContentConstants.MetaTags)]
        public string MetaTags { get; set; }

    }
}
