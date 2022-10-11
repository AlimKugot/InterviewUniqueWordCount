using ConsoleAppReflection;
using System.Reflection;

namespace InterviewUniqueWordCount
{
    public class MainReflection
    {
        private const string INPUT_FILE_NAME = "wolxter-text_indiskoe_prikluchenie.fb2";
        private const string OUTPUT_FILE_NAME = "output.txt";

        private static string InputFilePath = "";
        private static string OutputFilePath = "";
  

        static void Main(string[] args)
        {
            InitPaths();

            var classType = Type.GetType("InterviewUniqueWordCount.MainBookParser, InterviewUniqueWordCount", true, true);
            ReflectionPrinter.PrintClass(classType);

            var obj = new MainBookParser();

            var parse = classType.GetMethod("Parse", BindingFlags.NonPublic | BindingFlags.Static);
            var print = classType.GetMethod("PrintToFile", BindingFlags.NonPublic | BindingFlags.Static);

            var resultMap = parse.Invoke(obj, new object[] { InputFilePath });
            print.Invoke(obj, new object[] {OutputFilePath, resultMap});
        }


        private static void InitPaths()
        {
            var classType = Type.GetType("InterviewUniqueWordCount.utils.FileUtility, InterviewUniqueWordCount", true, true);
            var directoryLevelUp = classType.GetMethod("DirectoryLevelUp", BindingFlags.Public | BindingFlags.Static);
            
            var obj = new MainBookParser();

            var root = directoryLevelUp?.Invoke(obj, new object[] {3});
            if (root is null) throw new NullReferenceException("root directory is null");

            InputFilePath = root + "\\" + INPUT_FILE_NAME;
            OutputFilePath = root + "\\" + OUTPUT_FILE_NAME;
        }
    }
}
