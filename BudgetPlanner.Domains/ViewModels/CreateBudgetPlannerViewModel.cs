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

        [Required, Display(Name = "Unique Reference"), MinLength(5), MaxLength(200)]
        public string Reference { get; set; }

        [Required, MinLength(3), MaxLength(200)]
        public string Name { get; set; }

        [Required, Display(Name = "Enabled")]
        public bool Active { get; set; }

        [HiddenInput]
        public DateTimeOffset? LastUpdated { get; set; }

        [HiddenInput]
        public DateTimeOffset Created { get; set; }
        public int AccountId { get; set; }
    }
}
