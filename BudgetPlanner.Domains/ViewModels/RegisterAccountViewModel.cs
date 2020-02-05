using DNI.Shared.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class RegisterAccountViewModel : BaseViewModel
    {
        public int Id { get; set; }
        
        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required, MinLength(6), MaxLength(32)]
        public string Password { get; set; }

        [Required, 
         MinLength(6), 
         MaxLength(32)]
        [MustMatch(nameof(Password))]
        public string Confirm { get; set; }

        [Required, MinLength(3), MaxLength(32)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(900)]
        public string LastName { get; set; }
        
        [Display(Name = "I accept terms of use")]
        public bool Active { get; set; }

        public DateTimeOffset Created { get; set; }
        
        public DateTimeOffset? Modified { get; set; }
    }
}
