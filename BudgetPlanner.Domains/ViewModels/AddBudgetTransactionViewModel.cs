using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Attributes;

namespace BudgetPlanner.Domains.ViewModels
{
    public class AddBudgetTransactionViewModel : BaseViewModel
    {
        public SelectList TransactionTypes { get; set; }

        [Content(ContentConstants.TransactionTypeLabel)]
        public string TransactionTypeLabel { get; set; }

        [Content(ContentConstants.DescriptionLabel)]
        public string DescriptionLabel { get; set; }

        [Content(ContentConstants.AmountLabel)]
        public string AmountLabel { get; set; }

        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Enabled")]
        public bool Active { get; set; }

        [Required, MaxLength(320)]
        public string Description { get; set; }

        [HiddenInput]
        public int BudgetId { get; set; }

        [HiddenInput]
        public DateTimeOffset Created { get; set; }
        
        [Required]
        public int TransactionTypeId { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
