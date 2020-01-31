using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Attributes
{
    public class HeaderValueAttribute : Attribute, IActionFilter
    {
        public HeaderValueAttribute(string name, string value = default)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext
                .Response.Headers
                .Add(Name, Value);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
