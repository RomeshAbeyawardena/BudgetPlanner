using BudgetPlanner.Domains.Attributes;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class TransactionListViewModel : PagerViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }

        [Content(ContentConstants.Description)]
        public string DescriptionHeading { get; set; }

        [Content(ContentConstants.Created)]
        public string CreatedHeading { get; set; }

        [Content(ContentConstants.Amount)]
        public string AmountHeading { get; set; }

        [Content(ContentConstants.OldBalance)]
        public string OldBalanceHeading { get; set; }

        [Content(ContentConstants.NewBalance)]
        public string NewBalanceHeading { get; set; }
    }
}
