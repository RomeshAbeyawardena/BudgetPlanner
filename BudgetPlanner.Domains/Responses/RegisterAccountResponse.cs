using BudgetPlanner.Domains.Dto;
using DNI.Shared.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class RegisterAccountResponse : ResponseBase
    {
        public  Exception Exception { get; set; }
        public Account SavedAccount { get; set; }
    }
}
