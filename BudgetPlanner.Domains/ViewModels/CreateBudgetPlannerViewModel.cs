using BudgetPlanner.Domains.Attributes;
using BudgetPlanner.Domains.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class CreateBudgetPlannerViewModel : BaseViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required, MinLength(5), MaxLength(200)]
        public string Reference { get; set; }

        [Content(ContentConstants.ReferenceLabel)]
        public string ReferenceLabel { get; set; }

        [Required, MinLength(3), MaxLength(200)]
        public string Name { get; set; }

        [Content(ContentConstants.NameLabel)]
        public string NameLabel { get; set; }

        [Required]
        public bool Active { get; set; }

        [Content(ContentConstants.ActiveLabel)]
        public string ActiveLabel { get; set; }

        [HiddenInput]
        public DateTimeOffset? LastUpdated { get; set; }

        [HiddenInput]
        public DateTimeOffset Created { get; set; }
        public int AccountId { get; set; }
    }
}
