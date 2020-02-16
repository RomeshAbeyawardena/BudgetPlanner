using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using BudgetPlanner.Shared;
namespace BudgetPlanner.Cms.Controllers
{

    public class ContentResult
    {
        public bool Succeeded { get; set; }
        public IDictionary<string, string> Result { get; set; }
    }

    public class ContentController : UmbracoApiController
    {
        [HttpGet]
        public JsonResult<ContentResult> Get(string contentPath)
        {
            
            IPublishedContent publishedContent = Umbraco.FindByPath('/', contentPath);
            
            if (publishedContent == null)
                return Json(new ContentResult { Succeeded = false });
            
            return Json(new ContentResult
            {
                Succeeded = true,
                Result = publishedContent.Properties.ToDictionary()
            });
        }
    }
}
