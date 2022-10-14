using System.Collections.Concurrent;
using System.Xml;

namespace InterviewUniqueWordCount.utils
{
    public class FbUtility
    {
        private static char[] separators = new char[] { ' ', '\r', '\n', '\t', ',', '.', ';', '!', '?', '-', '(', ')', '\"' };


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

        public static ConcurrentDictionary<string, long> CountUniqueWordsParallel(XmlNodeList nodeList)
        {
            var uniqueWordsWithCount = new ConcurrentDictionary<string, long>();
            
            Parallel.ForEach(nodeList.Cast<XmlNode>(), p =>
            {   
                string line = p.InnerText.ToLower();
                string[] separatedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                Parallel.ForEach(separatedLine, s =>
                {
                    if (uniqueWordsWithCount.ContainsKey(s))
                    {
                        uniqueWordsWithCount[s]++;
                    }
                    else
                    {
                        uniqueWordsWithCount[s] = 1;
                    }
                });
            });
            return uniqueWordsWithCount;
        }

        public static Dictionary<string, long> CountUniqueWords(XmlNodeList nodeList)
        {
            Dictionary<string, long> uniqueWordsWithCount = new Dictionary<string, long>();

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
