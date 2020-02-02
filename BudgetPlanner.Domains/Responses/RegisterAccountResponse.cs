using BudgetPlanner.Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Responses
{
    public class RegisterAccountResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorKey { get; set; }
        public string ErrorMessage { get; set; }
        public  Exception Exception { get; set; }
        public Account SavedAccount { get; set; }
    }
}
