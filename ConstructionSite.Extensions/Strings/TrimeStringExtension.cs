using HtmlAgilityPack;
using iTextSharp.text.html;
using System;
using System.Collections.Generic;
using System.Text;
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
