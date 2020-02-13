using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class DisallowUntrustedReferrersAttribute : Attribute, IActionFilter
    {
        public DisallowUntrustedReferrersAttribute(string trustedAction, string trustedController)
        {
            Action = trustedAction;
            Controller = trustedController;
        }

        public string Action { get; }
        public string Controller { get; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            ControllerBase controller = (ControllerBase)context.Controller;

            var referer = context.HttpContext.Request.Headers["Referer"].ToString();

            if(!referer.Contains(controller.Url.Action(Action, Controller)))
                context.Result = new BadRequestResult();

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
