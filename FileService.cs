using System.IO;
using System.Text;

namespace WordsBreaker
{
    internal static class FileService
    {
        public static string[] ReadFile(string inputPath) => 
            File.ReadAllLines(inputPath);

        public static void OutputFile() =>
            File.WriteAllLines("files/output.tsv", StringExtensions.WordsProcessing(), Encoding.UTF8);
    }
}