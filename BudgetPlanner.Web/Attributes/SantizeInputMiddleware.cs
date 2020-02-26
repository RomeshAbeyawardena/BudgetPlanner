using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetPlanner.Shared;
using System.Text.Encodings.Web;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace BudgetPlanner.Web.Attributes
{
    public class SantizeInputMiddleware
    {
        private readonly RequestDelegate _next;

        public SantizeInputMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.HasFormContentType)
            { 
                await _next(context);
                return;
            }

            var sanitizedFormDictionary = new Dictionary<string, StringValues>();

            foreach (var formField in context.Request.Form)
               sanitizedFormDictionary.Add(formField.Key, WebUtility.HtmlDecode(formField.Value));

            context.Request.Form = new FormCollection(sanitizedFormDictionary);
            await _next(context);
        }
    }
}
