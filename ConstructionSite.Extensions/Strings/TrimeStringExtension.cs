using System.Text.RegularExpressions;

namespace ConstructionSite.Extensions.Strings
{
    public static class TrimeStringExtension
    {
        public static string GetSRC(this string str)
        {
            var data = Regex.Match(str, "<iframe.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
            return data;
        }
    }
}