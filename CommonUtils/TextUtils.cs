using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonUtils
{
    public class TextUtils
    {

        //https://stackoverflow.com/a/2334958
        public static string StripTags(string htmlString)
        {
            string pattern = @"<(.|\n)*?>"; //OPTIMIZE make lazy static, compiled

            return Regex.Replace(htmlString, pattern, string.Empty);
        }
    }
}
