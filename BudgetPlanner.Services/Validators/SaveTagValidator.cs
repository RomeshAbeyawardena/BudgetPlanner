using BudgetPlanner.Contracts.Services;
using BudgetPlanner.Domains.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Validators
{
    public class SaveTagValidator : ValidatorBase<SaveTagRequest>
    {
        public SaveTagValidator(IAccountService accountService) : base(accountService)
        {
        }
    }
}
