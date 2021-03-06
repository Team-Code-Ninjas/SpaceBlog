﻿namespace SpaceBlog.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Utils
    {
        private static List<string> newRowTags = new List<string>() { "</p>", "</li>", "<br>" };

        /// <summary>
        /// This method will be used to display only the first 
        /// 100 characters of each content in article
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns>text</returns>
        /// <returns>shortText</returns>
        public static string CutText(string text, int maxLength)
        {
            if (text == null || text.Length <= maxLength)
            {
                return text;
            }

            var shortText = text.Substring(0, maxLength) + "...";
            return shortText;
        }

        public static string CutHtmlText(string text, int maxLength)
        {
            // var shortText = CutText(text, maxLength);
            var shortText = text;
            int additionalCuts = 0;
            foreach (var tag in newRowTags)
            {
                string[] localSeparators = { tag };

                if (shortText.Contains(tag))
                {
                    shortText = shortText.Split(localSeparators, StringSplitOptions.RemoveEmptyEntries)[0] + tag;
                    additionalCuts = additionalCuts + CalculateHtmlTagCuts(tag);
                }
            }

            return CutText(text, maxLength + additionalCuts);
        }

        private static int CalculateHtmlTagCuts(string htmlTag)
        {
            if (htmlTag == "<br>")
            {
                return htmlTag.Length;
            }
            else
            {
                return htmlTag.Length * 2;
            }
        }

        public static string CutTags(string content)
        {
            var newText = Regex.Replace(content, "<img.*>", "(image) ", RegexOptions.Singleline);
            newText = Regex.Replace(newText, @"<iframe.*>.*<\/iframe>", "(video) ", RegexOptions.Singleline);
            //newText = Regex.Replace(newText, "<table.*?>.*?</table>", "(table) ", RegexOptions.Singleline);
            //newText = Regex.Replace(newText, "<br />", " ", RegexOptions.Singleline);
            //newText = Regex.Replace(newText, "<div.*?>.*?</div>", "", RegexOptions.Singleline);
            //newText = Regex.Replace(newText, "<p.*?>", "", RegexOptions.Singleline);
            //newText = Regex.Replace(newText, "</p>", "", RegexOptions.Singleline);
            //newText = Regex.Replace(newText, "<ul.*?>.*?</ul>", "", RegexOptions.Singleline);
            //newText = Regex.Replace(newText, "<ol.*?>.*?</ol>", "", RegexOptions.Singleline);

            return newText;
        }
    }
}