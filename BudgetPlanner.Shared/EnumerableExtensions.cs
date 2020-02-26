using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Shared
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> values, Action<T> factoryMethod)
        {
            foreach (var value in values)
            {
                factoryMethod(value);
            }

            return values;
        }
    }
}
