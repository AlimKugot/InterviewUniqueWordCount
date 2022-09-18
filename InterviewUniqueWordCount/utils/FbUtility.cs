using System.Xml;

namespace InterviewUniqueWordCount.utils
{
    public class FbUtility
    {
        private FbUtility() { }

        public static XmlNodeList ParseParagraphsToList(string filePath)
        {
            XmlDocument xmlDocument = new();
            xmlDocument.Load(filePath);

            XmlNodeList? pNodeList = xmlDocument.SelectNodes("" +
                "/*[local-name() = 'FictionBook']" +
                "/*[local-name() = 'body']" +
                "/*[local-name() = 'section']" +
                "/*[local-name() = 'p']");

            if (pNodeList is null || pNodeList.Count == 0)
            {
                throw new NullReferenceException(
                    "Cannot find <p>. " +
                    "Please check structure of your book"
                );
            }

            return pNodeList;
        }

        public static Dictionary<string, long> CountUniqueWords(XmlNodeList nodeList)
        {
            Dictionary<string, long> uniqueWordsWithCount = new Dictionary<string, long>();
            char[] separators = new char[] { ' ', '\r', '\n', '\t', ',', '.', ';', '!', '?', '-', '(', ')', '\"'};

            foreach (XmlNode p in nodeList)
            {
                string line = p.InnerText.ToLower();
                string[] separatedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in separatedLine)
                {
                    if (uniqueWordsWithCount.ContainsKey(s))
                    {
                        uniqueWordsWithCount[s]++;
                    }
                    else
                    {
                        uniqueWordsWithCount[s] = 1;
                    }
                }
            }

            return uniqueWordsWithCount;
        }
    }
}
