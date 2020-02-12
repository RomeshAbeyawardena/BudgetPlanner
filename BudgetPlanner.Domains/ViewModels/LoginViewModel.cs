using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        [Display(Name ="Email Address")]
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
