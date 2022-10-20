using System;
using System.IO;

namespace WCFClient
{
    public class Program
    {
        private static string xmlString = "";
        private static string outputPath = "";
        private const string FILE_NAME = "tolstoi_anna_karenina.fb2";

        private static void InitPaths()
        {
            string root = Files.FileUtility.DirectoryLevelUp(3);
            string folder = typeof(Program).Namespace;
            string path = root + "\\" + folder + "\\" + FILE_NAME;

            outputPath = root + "\\" + folder + "\\" + "output.txt";
            xmlString = File.ReadAllText(path);
        }

        static void Main(string[] args)
        {
            InitPaths();

            var proxy = new WCFServiceRef.ServiceBookParserClient();
            var responseDictionary = proxy.CountUniqueWords(xmlString);
            Files.FileUtility.PrintToFile(outputPath, responseDictionary);
        } 
    }
}
