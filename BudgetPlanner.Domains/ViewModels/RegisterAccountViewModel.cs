using BudgetPlanner.Domains.Attributes;
using BudgetPlanner.Domains.Constants;
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

        [Content(ContentConstants.EmailAddressLabel)]
        public string EmailAddressLabel { get; set; }

        [Required, MinLength(6), MaxLength(32)]
        public string Password { get; set; }

        [Content(ContentConstants.PasswordLabel)]
        public string PasswordLabel { get; set; }

        [Required, 
         MinLength(6), 
         MaxLength(32)]
        [MustMatch(nameof(Password))]
        public string Confirm { get; set; }

        [Content(ContentConstants.ConfirmPasswordLabel)]
        public string ConfirmPasswordLabel { get; set; }

        [Required, MinLength(3), MaxLength(32)]
        public string FirstName { get; set; }

        [Content(ContentConstants.FirstNameLabel)]
        public string FirstNameLabel { get; set; }
        [Required, MinLength(2), MaxLength(900)]
        public string LastName { get; set; }

        [Content(ContentConstants.LastNameLabel)]
        public string LastNameLabel { get; set; }
        
        public bool Active { get; set; }

        [Content(ContentConstants.AcceptTermsOfUseLabel)]
        public string AcceptTermsOfUseLabel { get; set; }

        [Content(ContentConstants.Content)]
        public string Content { get; set; }

        public DateTimeOffset Created { get; set; }
        
        public DateTimeOffset? Modified { get; set; }
    }
}
