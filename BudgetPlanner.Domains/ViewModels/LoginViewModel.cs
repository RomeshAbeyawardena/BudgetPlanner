using BudgetPlanner.Domains.Attributes;
using BudgetPlanner.Domains.Constants;
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
        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Content(ContentConstants.EmailAddressLabel)]
        public string EmailAddressLabel { get; set; }

        public string Password { get; set; }

        [Content(ContentConstants.PasswordLabel)]
        public string PasswordLabel { get; set; }

        public bool RememberMe { get; set; }

        [Content(ContentConstants.RememberMeLabel)]
        public string RememberMeLabel { get; set; }
    }
}
