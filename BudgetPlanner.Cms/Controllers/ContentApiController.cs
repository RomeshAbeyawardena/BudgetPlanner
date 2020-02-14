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

namespace BudgetPlanner.Cms.Controllers
{
    [Route("api/content/{controller}")]
    public class ContentApiController : UmbracoApiController
    {
        [HttpGet]
        public JsonResult<IDictionary<string, string>> Get(string contentPath)
        {
            var content = Umbraco.ContentSingleAtXPath($"//{contentPath}");
            
            return Json(ToDictionary(content.Properties));
        }

        private IDictionary<string, string> ToDictionary(IEnumerable<IPublishedProperty> publishedProperties)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var property in publishedProperties)
            {
                if(!property.HasValue())
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
