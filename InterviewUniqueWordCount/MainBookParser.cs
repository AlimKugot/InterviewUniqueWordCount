using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Xml;
using System.Xml.Linq;
using InterviewUniqueWordCount.utils;

namespace InterviewUniqueWordCount
{
    public class MainBookParser
    {

        private static readonly string PROJECT_ROOT_PATH = FileUtility.DirectoryLevelUp(3);

        private static readonly string DEFAULT_INPUT_FILE_NAME = "gjugo_1861_miserables.fb2";
        private static readonly string DEFAULT_INPUT_FILE_PATH = PROJECT_ROOT_PATH + "\\" + DEFAULT_INPUT_FILE_NAME;

        private static readonly string OUTPUT_FILE_NAME = "output.txt";
        private static readonly string OUTPUT_FILE_PATH = OUTPUT_FILE_NAME;


        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("Starting parsing default file: " + DEFAULT_INPUT_FILE_PATH);
                    Dictionary<string, long> result = Parse(DEFAULT_INPUT_FILE_PATH);
                    PrintToFile(OUTPUT_FILE_PATH, result);
                    break;
                case 1:
                    Console.WriteLine("Starting parsing: " + args[0]);
                    Dictionary<string, long> resultWithArg = Parse(args[0]);
                    PrintToFile(OUTPUT_FILE_PATH, resultWithArg);
                    break;
                default:
                    Console.WriteLine("Error: too much arguments. Please enter file path to your txt file");
                    break;
            }
        }


        private static Dictionary<string, long> Parse(string filePath)
        {
            XmlNodeList paragraphList = FbUtility.ParseParagraphsToList(filePath);
            Dictionary<string, long> res = FbUtility.CountUniqueWords(paragraphList);
            Dictionary<string, long> ordered = res.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }

        public static Dictionary<string, long> ParseParallel(string filePath)
        {
            XmlNodeList paragraphList = FbUtility.ParseParagraphsToList(filePath);
            var res = FbUtility.CountUniqueWordsParallel(paragraphList);
            var ordered = res.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }

        private static void PrintToFile(string outputFilePath, Dictionary<string, long> wordCount)
        {
            using (StreamWriter writer = new(outputFilePath))
            {
                foreach (KeyValuePair<string, long> kvp in wordCount)
                {
                    writer.WriteLine(kvp.Key + " " + kvp.Value);
                }
            }
        }
    }
}
