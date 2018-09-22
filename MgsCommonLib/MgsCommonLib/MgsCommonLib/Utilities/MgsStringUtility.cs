using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MgsCommonLib.MgsCommonLib.Utilities
{
    public static class MgsStringUtility
    {
        public static string ToFristLetterUpperCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
