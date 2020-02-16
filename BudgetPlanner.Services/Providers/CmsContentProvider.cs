using BudgetPlanner.Contracts.HttpServices;
using BudgetPlanner.Contracts.Providers;
using BudgetPlanner.Domains.Attributes;
using BudgetPlanner.Domains.Constants;
using BudgetPlanner.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Providers
{
    public class CmsContentProvider : ICmsContentProvider
    {
        private readonly ICmsHttpService _cmsHttpService;

        public async Task<T> PopulateContent<T>(string contentPath, T model, 
            IDictionary<string, string> placeholders = null, string replaceParameterStart = default,
            string replaceParameterEnd = default)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var content = await _cmsHttpService.GetContent(contentPath);

            foreach (var property in properties)
            {
                var contentAttribute = property.GetCustomAttribute<ContentAttribute>();
                if(contentAttribute == null)
                    continue;

                var contentName = (string.IsNullOrEmpty(contentAttribute.ContentName)) 
                    ? property.Name 
                    : contentAttribute.ContentName;

                if(!content.TryGetValue(contentName, out var contentValue) 
                    || string.IsNullOrEmpty(contentValue))
                    continue;

                if(replaceParameterStart == default)
                    replaceParameterStart = ContentConstants.ReplaceParameterStart;

                if(replaceParameterEnd == default)
                    replaceParameterEnd = ContentConstants.ReplaceParameterEnd;

                if(placeholders != null)
                    contentValue = contentValue.
                        ReplaceByKey(replaceParameterStart, replaceParameterEnd, placeholders);

                property.SetValue(model, contentValue);
            }

            return model;
        }

        public CmsContentProvider(ICmsHttpService cmsHttpService)
        {
            _cmsHttpService = cmsHttpService;
        }
    }
}
