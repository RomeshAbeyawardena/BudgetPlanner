using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Providers
{
    public interface ICmsContentProvider
    {
        Task<T> PopulateContent<T>(string contentPath, T model, 
            IDictionary<string, string> placeholders = null, 
            string replaceParameterStart = default,
            string replaceParameterEnd = default);
    }
}
