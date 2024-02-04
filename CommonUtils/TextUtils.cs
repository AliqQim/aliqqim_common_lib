using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonUtils
{
    public class TextUtils
    {
        //based on https://stackoverflow.com/a/2334958
        private static readonly Lazy<Regex> _stripTagsRegex = new Lazy<Regex>(() =>
            new Regex(@"<(\??)(\s*/|)([a-zA-Z]+)(.|\n)*?>", RegexOptions.Compiled));

        public static string StripTags(string htmlString, bool insertLineBreaks = true)
        {
            return _stripTagsRegex.Value.Replace(htmlString, m =>
            {
                string replacer = "";

                if (insertLineBreaks && !IsInitialXmlTag())
                {
                    if (IsClosingTag() 
                        && GetTag().Equals("p", StringComparison.OrdinalIgnoreCase))
                    {
                        replacer = "\n";
                    }
                    else if (!IsClosingTag())
                    {
                        var lineBreakingTags = new[] { "br", "p" };
                        var tag = GetTag();
                        if (lineBreakingTags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                        {
                            replacer = "\n";
                        }
                    }
                }

                return replacer;

                bool IsClosingTag()
                {
                    return m.Groups[2].Value.Length > 0;
                }

                bool IsInitialXmlTag()
                {
                    return m.Groups[1].Value.Length > 0;
                }

                string GetTag()
                {
                    return m.Groups[3].Value;
                }
            });

        }
    }
}
