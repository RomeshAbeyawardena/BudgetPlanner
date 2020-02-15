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
            //var content = Umbraco.ContentSingleAtXPath($"//{contentPath}");
            
            IPublishedContent publishedContent = Umbraco.FindByPath('/', contentPath);
            
            if (publishedContent == null)
                return Json(new ContentResult { Succeeded = false });

            return Json(new ContentResult
            {
                Succeeded = true,
                Result = ToDictionary(publishedContent.Properties)
            });
        }
        

        private IDictionary<string, string> ToDictionary(IEnumerable<IPublishedProperty> publishedProperties)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var property in publishedProperties)
            {
                if (!property.HasValue())
                    continue;

                var propertyValue = property.Value();
                var propertyValueType = propertyValue.GetType();

                propertyValue = propertyValueType.IsArray
                    ? string.Join(",", (object[])propertyValue)
                    : property.Value<string>();

                dictionary.Add(property.Alias, propertyValue.ToString());
            }

            return dictionary;
        }
    }
}
