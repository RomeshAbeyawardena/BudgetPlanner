using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BudgetPlanner.Services.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceByKey(this string value, string replaceParameterStart,
            string replaceParameterEnd, IDictionary<string, string> dictionary)
        {
            //remove html 
            var wordSource = Regex.Replace(value, "([<][/]{0,1}[a-z]{0,}[>])", string.Empty, RegexOptions.ECMAScript | RegexOptions.Multiline);

            Func<string, bool> ContainsKeys = (word => word
                    .StartsWith(replaceParameterStart)
                        && word.EndsWith(replaceParameterEnd)
                        && dictionary.ContainsKey(word
                            .GetKey(replaceParameterStart, replaceParameterEnd)));

            var words = wordSource.Split(' ')
                .Where(ContainsKeys);

            if (!words.Any())
                return value;

            foreach (var word in words)
            {
                if (!dictionary.TryGetValue(word
                        .GetKey(replaceParameterStart, replaceParameterEnd),
                    out var replacement))
                    continue;

                var replacementValue = replacement.Split(' ').Any(ContainsKeys)
                    ? ReplaceByKey(replacement, replaceParameterStart, replaceParameterEnd, dictionary)
                    : replacement;

                value = value.Replace(word, replacementValue);
            }

            return value;
        }

        private static string GetKey(this string value, string replaceParameterStart, string replaceParameterEnd)
        {
            return value.Replace(replaceParameterStart, string.Empty)
                            .Replace(replaceParameterEnd, string.Empty);
        }
    }
}
