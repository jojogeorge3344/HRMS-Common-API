using System;
using System.Collections.Generic;
using System.Linq;

namespace Chef.Common.Test
{
    public static class Extensions
    {
        public static T GetRandom<T>(this IEnumerable<T> iEnumerable)
        {
            if(iEnumerable.Count() >0 )
                return iEnumerable.ElementAt(new Random().Next(0, iEnumerable.Count()));
            return default;
        }

        public static string InsertNTrimEnd(this string input, int insertPosition, string insertText, int noOfCharactersToTrim)
        {
            var text = input.Insert(insertPosition, insertText);
            var trimIndex = text.Length - noOfCharactersToTrim;
            trimIndex = (trimIndex < 0) ? 0 : trimIndex;
            trimIndex = (trimIndex > text.Length - 1) ? text.Length - 1 : trimIndex;
            return text.Remove(trimIndex);
        }
    }
}
