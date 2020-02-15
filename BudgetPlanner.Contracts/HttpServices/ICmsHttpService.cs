using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.HttpServices
{
    public interface ICmsHttpService : IHttpService
    {
        Task<IDictionary<string, string>> GetContent(string path);
    }
}
