using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains
{
    public class ApiSetting
    {
        public Uri BaseUri { get; set; }
        public string ServicePath { get; set; }
        public Uri Uri
        {
            get
            {
                if (Uri.TryCreate(BaseUri, ServicePath, out var uri))
                    return uri;

                return default;
            }
        }
    }
}
