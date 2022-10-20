using System.Collections.Concurrent;

namespace InterviewUniqueWordCount
{
    public class MainBookParser
    {

        private static readonly string PROJECT_ROOT_PATH = Files.FileUtility.DirectoryLevelUp(3);

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
                    Files.FileUtility.PrintToFile(OUTPUT_FILE_PATH, result);
                    break;
                case 1:
                    Console.WriteLine("Starting parsing: " + args[0]);
                    Dictionary<string, long> resultWithArg = Parse(args[0]);
                    Files.FileUtility.PrintToFile(OUTPUT_FILE_PATH, resultWithArg);
                    break;
                default:
                    Console.WriteLine("Error: too much arguments. Please enter file path to your txt file");
                    break;
            }
        }


        private static Dictionary<string, long> Parse(string filePath)
        {
            List<string> words = FbUtility.ParseXmlFile(filePath);
            Dictionary<string, long> res = FbUtility.CountUniqueWords(words);
            return res;
        }

        public static Dictionary<string, long> ParseParallel(string filePath)
        {
            List<string> words = FbUtility.ParseXmlFile(filePath);
            ConcurrentBag<string> wordsConcurrent = new ConcurrentBag<string>(words);

            return FbUtility.CountUniqueWordsParallel(wordsConcurrent);
        }
    }
}
