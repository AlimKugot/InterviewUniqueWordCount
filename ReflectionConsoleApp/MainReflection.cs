using ConsoleAppReflection;
using System.Reflection;

namespace InterviewUniqueWordCount
{
    public class MainReflection
    {

        private const string INPUT_FILE_PATH = "C:\\Users\\alimf\\source\\repos\\InterviewUniqueWordCount\\ReflectionConsoleApp\\wolxter-text_indiskoe_prikluchenie.fb2";
        private const string OUTPUT_FILE_PATH = "C:\\Users\\alimf\\source\\repos\\InterviewUniqueWordCount\\ReflectionConsoleApp\\output.txt";

        static void Main(string[] args)
        {
            var classType = Type.GetType("InterviewUniqueWordCount.MainBookParser, InterviewUniqueWordCount", true, true);
            ReflectionPrinter.PrintClass(classType);

            var obj = new MainBookParser();

            var parse = classType.GetMethod("Parse", BindingFlags.NonPublic | BindingFlags.Static);
            var print = classType.GetMethod("PrintToFile", BindingFlags.NonPublic | BindingFlags.Static);

            var resultMap = parse.Invoke(obj, new object[] { INPUT_FILE_PATH });
            print.Invoke(obj, new object[] {OUTPUT_FILE_PATH, resultMap});
        }

    }
}
