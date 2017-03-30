namespace SpaceBlog.Utilities
{
    public class Utils
    {
        /// <summary>
        /// This method will be used to display only the first 
        /// 100 characters of each content in article
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns>text</returns>
        /// <returns>shortText</returns>
        public static string CutText(string text, int maxLength = 100)
        {
            if (text == null || text.Length <= maxLength)
            {
                return text;
            }

            var shortText = text.Substring(0, maxLength) + "...";

            return shortText;
        }
    }
}