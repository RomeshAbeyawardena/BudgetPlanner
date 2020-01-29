using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Domains.Constants
{
    public static class FormatConstants
    {
        public const string CurrencyFormat = "C2";
        public const string ShortDateFormat = "dd MMM yyyy";
        public const string LongDateFormat = "dd MMMM yyyy";
        public static string SetFormat(string format, int index = 0)
        {
            var formatBuilder = new StringBuilder("{");
            formatBuilder.Append(index);
            formatBuilder.Append(":");
            formatBuilder.Append(format);
            formatBuilder.Append("}");
            return formatBuilder.ToString();
        }
    }
}
