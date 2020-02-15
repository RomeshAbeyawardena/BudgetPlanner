using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace BudgetPlanner.Shared
{
    public static class UmbracoHelperExtensions
    {
        public static IPublishedContent FindByPath(this UmbracoHelper helper, char pathNodeSeparator, string path)
        {
            var paths = path.Split(pathNodeSeparator);

            IPublishedContent publishedContent = null;

            foreach(var nodePath in paths){
                publishedContent = GetContentAtNode(helper, nodePath, publishedContent);
                if (publishedContent == null)
                    return default;
            }

            return publishedContent;
        }

        public static IPublishedContent GetContentAtNode(UmbracoHelper helper, string node, IPublishedContent rootContent = null)
        {
            var root = rootContent == null ? helper.ContentAtRoot() : rootContent.Children;

            foreach(var content in root)
            {
                if(content.Name.Equals(node, StringComparison.InvariantCultureIgnoreCase))
                    return content;
            }

            return default;
        }
    }
}
