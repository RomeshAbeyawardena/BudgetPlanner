using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.HttpServices
{
    public interface IHttpService
    {
        Task<string> GetRequestToken();
    }
}
