using System.Collections.Concurrent;
using System.Xml;

namespace InterviewUniqueWordCount
{
    public class FbUtility
    {
        private static readonly char[] separators = new char[] { ' ', '\r', '\n', '\t', ',', '.', ';', '!', '?', '-', '(', ')', '\"' };

        private FbUtility() { }

        public static List<string> ParseXmlString(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            return ExtractFromXmlWords(xmlDocument);
        }

        public static List<string> ParseXmlFile(string filePath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);
            return ExtractFromXmlWords(xmlDocument);
        }

        private static List<string> ExtractFromXmlWords(XmlDocument xmlDocument)
        {
            XmlNodeList pNodeList = xmlDocument.SelectNodes("" +
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

            List<string> words = new List<string>(pNodeList.Count);

            foreach (XmlNode p in pNodeList)
            {
                string line = p.InnerText.ToLower();
                string[] separatedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in separatedLine)
                {
                    words.Add(s);
                }
            }

            return words;
        }


        public static Dictionary<string, long> CountUniqueWordsParallel(ConcurrentBag<string> words)
        {
            ConcurrentDictionary<string, long> uniqueWordsWithCount = new ConcurrentDictionary<string, long>();

            Parallel.ForEach(words, word =>
            {
                uniqueWordsWithCount.AddOrUpdate(word, 1, (key, current) => current + 1);
            });

            Dictionary<string, long> dict= uniqueWordsWithCount.ToDictionary(e => e.Key, e => e.Value);
            Dictionary<string, long> ordered = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }


        public static Dictionary<string, long> CountUniqueWords(List<string> words)
        {
            Dictionary<string, long> uniqueWordsWithCount = new Dictionary<string, long>();

            foreach (string s in words)
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
            var ordered = uniqueWordsWithCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }
    }
}
