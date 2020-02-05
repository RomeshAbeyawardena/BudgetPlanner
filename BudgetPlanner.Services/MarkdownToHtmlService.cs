using BudgetPlanner.Contracts.Services;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class MarkdownToHtmlService : IMarkdownToHtmlService
    {
        public string ToHtml(string markdown, Action<Markdig.MarkdownPipelineBuilder> builder)
        {
            var pipelineBuilder = new Markdig.MarkdownPipelineBuilder();
            builder?.Invoke(pipelineBuilder);
            return Markdig.Markdown.ToHtml(markdown, pipelineBuilder.Build());
        }
    }
}
