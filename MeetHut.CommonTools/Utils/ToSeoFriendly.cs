
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MeetHut.CommonTools.Utils
{
    /// <summary>
    /// ToSeoFriendly converter
    /// </summary>
    public static class ToSeoFriendly
    {
        /// <summary>
        /// String -> Seo-friendly string convert
        /// </summary>
        /// <param name="str"></param>
        public static string String(string str)
        {
            str = new string(str.ToLowerInvariant().Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray()).Normalize(NormalizationForm.FormC);

            var match = Regex.Match(str, "[\\w]+");
            var result = new System.Text.StringBuilder("");
            bool maxLengthHit = false;
            while (match.Success && !maxLengthHit)
            {
                result.Append(match.Value + "-");
                match = match.NextMatch();
            }

            return result.ToString().TrimEnd('-');
        }

    }
}