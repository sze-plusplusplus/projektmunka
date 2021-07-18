using System.Collections.Generic;
using System.Linq;

namespace MeetHut.CommonTools.StringBuilder
{
    /// <summary>
    /// String builder for create formatted string for an object
    /// </summary>
    public static class StringBuilder
    {
        /// <summary>
        /// Convert values pairs to formatted string
        /// </summary>
        /// <param name="values">Value pairs</param>
        /// <returns>Formatted string</returns>
        public static string ToString(Dictionary<string, string> values)
        {
            return ToString(",", values);
        }
        
        /// <summary>
        /// Convert values pairs to formatted string
        /// </summary>
        /// <param name="separator">Pair separator</param>
        /// <param name="values">Value pairs</param>
        /// <returns>Formatted string</returns>
        public static string ToString(string separator, Dictionary<string, string> values)
        {
            return string.Join($"{separator}\n", values.Select(Convert).ToList());
        }

        private static string Convert(KeyValuePair<string, string> pair)
        {
            return $"{pair.Key}:{pair.Value}";
        }
    }
}