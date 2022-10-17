using System.Diagnostics;
using System.Reflection;

namespace InterviewUniqueWordCount
{
    public class MainParallel
    {
        private static string ROOT = "";

        public static void Main(string[] args)
        {
            InitRootPath();

            string inputFilePath = ROOT + "\\" + "tolstoi_anna_karenina.fb2";
            string outputFilePath = ROOT + "\\" + "output.txt";
            string outputParallelFilePath = ROOT + "\\" + "output_parallel.txt";

            Stopwatch stopwatch = new Stopwatch();

            Console.Write("Concurrent mode: ");
            stopwatch.Start();
            ParseFromDll(inputFilePath, outputFilePath);
            stopwatch.Stop();
            Console.WriteLine("{0} ms", stopwatch.Elapsed.TotalMilliseconds);

            Console.Write("Parallel mode: ");
            stopwatch.Start();
            ParseFromDll(inputFilePath, outputParallelFilePath, true);
            stopwatch.Stop();
            Console.WriteLine("{0} ms", stopwatch.Elapsed.TotalMilliseconds);
        }

        private static void ParseFromDll(string inputFilePath, string outputFilePath, bool isParallel = false)
        {
            var classType = Type.GetType("InterviewUniqueWordCount.MainBookParser, InterviewUniqueWordCount", true, true);
            var obj = new MainBookParser();

            MethodInfo? parse;
            if (isParallel)
            {
                parse = classType.GetMethod("ParseParallel", BindingFlags.Public | BindingFlags.Static);
            }
            else
            {
                parse = classType.GetMethod("Parse", BindingFlags.NonPublic | BindingFlags.Static);
            }

            var print = classType.GetMethod("PrintToFile", BindingFlags.NonPublic | BindingFlags.Static);
            var resultMap = parse.Invoke(obj, new object[] { inputFilePath });
            print.Invoke(obj, new object[] { outputFilePath, resultMap });
        }



        private static void InitRootPath()
        {
            var classType = Type.GetType("InterviewUniqueWordCount.utils.FileUtility, InterviewUniqueWordCount", true, true);
            var directoryLevelUp = classType.GetMethod("DirectoryLevelUp", BindingFlags.Public | BindingFlags.Static);

            var obj = new MainBookParser();

            ROOT = (string)directoryLevelUp?.Invoke(obj, new object[] { 3 });
        }
    }
}
