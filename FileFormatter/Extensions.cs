using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration
{
    public static class Extensions
    {
        public static int IndexOf(this IEnumerable<string> strings, string exactMatch)
        {
            return strings.IndexOf(str => str == exactMatch);
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Predicate<T> condition)
        {            
            int index = -1;
            int result = items.Any(item =>
            {
                index++;                
                return condition(item);
            }) ? index : -1;            

            return result;
        }
    }
}
