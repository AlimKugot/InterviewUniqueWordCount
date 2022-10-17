using System.Collections.Concurrent;
using System.Xml;

namespace InterviewUniqueWordCount.utils
{
    public class FbUtility
    {
        private static readonly char[] separators = new char[] { ' ', '\r', '\n', '\t', ',', '.', ';', '!', '?', '-', '(', ')', '\"' };

        private FbUtility() { }

        public static List<string> ParseParagraphsToList(string filePath)
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

            List<string> words = new(pNodeList.Count);
            
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


        public static ConcurrentDictionary<string, long> CountUniqueWordsParallel(ConcurrentBag<string> words)
        {
            ConcurrentDictionary<string, long> uniqueWordsWithCount = new();

            Parallel.ForEach(words, word =>
            {
                uniqueWordsWithCount.AddOrUpdate(word, 1, (key, current) => current + 1);
                //if (!uniqueWordsWithCount.TryAdd(word, 1))
                //{
                //    uniqueWordsWithCount[word]++;
                //}
            });

            return uniqueWordsWithCount;
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

            return uniqueWordsWithCount;
        }
    }
}
