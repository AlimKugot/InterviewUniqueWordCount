namespace Files
{
    public class FileUtility
    {
        public static string DirectoryLevelUp(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be > 0");
            }

            DirectoryInfo? currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            for (int i = count; i > 1 && currentDirectory != null; --i)
            {
                currentDirectory = Directory.GetParent(currentDirectory.FullName);
            }

            if (currentDirectory == null)
            {
                throw new ArgumentException("Count is too large");
            }

            return currentDirectory.FullName;
        }

        public static void PrintToFile(string outputFilePath, IDictionary<string, long> wordCount)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (KeyValuePair<string, long> kvp in wordCount)
                {
                    writer.WriteLine(kvp.Key + " " + kvp.Value);
                }
            }
        }
    }
}
