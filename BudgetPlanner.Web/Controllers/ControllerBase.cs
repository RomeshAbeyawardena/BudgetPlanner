using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using DNI.Shared.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Controllers
{
    public class ControllerBase : DefaultControllerBase
    {
        protected Account CurrentAccount => HttpContext.Items.TryGetValue(DataConstants.AccountItem, out var value) 
            && value is Account account ? account : null; 

        protected void AddModelStateErrors(IEnumerable<IdentityError> identityErrors)
        {
            foreach (var item in identityErrors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
        }
    }
}
