using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Domains.Dto;
using DNI.Shared.Services.Abstraction;
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
    }
}
