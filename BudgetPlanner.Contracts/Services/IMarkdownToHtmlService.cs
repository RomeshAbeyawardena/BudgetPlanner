using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Contracts.Services
{
    public interface IMarkdownToHtmlService
    {
        string ToHtml(string markdown, Action<Markdig.MarkdownPipelineBuilder> builder = null);
    }
}
