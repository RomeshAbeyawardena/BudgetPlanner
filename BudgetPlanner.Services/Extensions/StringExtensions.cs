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
        public static Func<string, bool> ContainsKeys(string replaceParameterStart,
            string replaceParameterEnd, IDictionary<string, string> dictionary) => (word =>
                IsKey(word, replaceParameterStart, replaceParameterEnd)
                        && dictionary.ContainsKey(word
                            .GetKey(replaceParameterStart, replaceParameterEnd)));

        public static string ReplaceByKey(this string value, string replaceParameterStart,
            string replaceParameterEnd, IDictionary<string, string> dictionary)
        {
            var words = value
                .GetKeyWords(replaceParameterStart, replaceParameterEnd, dictionary);

            if (!words.Any())
                return value;

            foreach (var word in words)
            {
                if (!dictionary.TryGetValue(word
                        .GetKey(replaceParameterStart, replaceParameterEnd),
                    out var replacement))
                    continue;

                var replacementValue = replacement.GetKeyWords(replaceParameterStart,
                        replaceParameterEnd, dictionary).Any()
                    ? ReplaceByKey(replacement, replaceParameterStart, replaceParameterEnd, dictionary)
                    : replacement;

                value = value.Replace(word, replacementValue);
            }

            return value;
        }

        private static IEnumerable<string> GetKeyWords(this string value, string replaceParameterStart,
            string replaceParameterEnd, IDictionary<string, string> dictionary)
        {
            var regexOptions = RegexOptions.ECMAScript | RegexOptions.Multiline;
            //remove html 
            var wordSource = Regex
                .Replace(value, "([<][/]{0,1}[a-z]{0,}[>])", string.Empty, regexOptions);
            //remove punctuation
            wordSource = Regex.Replace(wordSource, "[\\@\\%\\£\\$\\.\\,\\:\\;]{0,}", string.Empty, regexOptions);

            return wordSource.Split(' ')
                .Where(ContainsKeys(replaceParameterStart,
                    replaceParameterEnd, dictionary));
        }

        private static bool IsKey(this string value, string replaceParameterStart,
            string replaceParameterEnd)
        {
            return value
                    .StartsWith(replaceParameterStart)
                        && value.EndsWith(replaceParameterEnd);
        }

        private static string GetKey(this string value, string replaceParameterStart, string replaceParameterEnd)
        {
            return value.Replace(replaceParameterStart, string.Empty)
                            .Replace(replaceParameterEnd, string.Empty);
        }
    }
}
