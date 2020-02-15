using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Attributes
{
    public sealed class ContentAttribute : Attribute
    {
        public ContentAttribute(string contentName = default)
        {
            ContentName = contentName;
        }

        public string ContentName { get; }
    }
}
