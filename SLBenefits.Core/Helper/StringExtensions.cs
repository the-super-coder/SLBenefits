using System;
using System.Threading;

namespace SLBenefits.Core.Helper
{
    public static class StringExtensions
    {
        public static string ToProperCase(this string value)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static bool Contains(this string sourceString, string stringToCheck, StringComparison comparison)
        {
            return sourceString.IndexOf(stringToCheck, comparison) >= 0;
        }

        public static bool IsEmpty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        public static bool IsNotEmpty(this string stringValue)
        {
            return !string.IsNullOrEmpty(stringValue);
        }

        public static bool ToBool(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue)) return false;

            return bool.Parse(stringValue);
        }
        
    }
}
