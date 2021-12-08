using System.IO;
using System.Text;

namespace WordsBreaker
{
    internal static class FileService
    {
        public static string[] ReadFile(string inputPath) => 
            File.ReadAllLines(inputPath);

        public static void OutputFile(string[] output) =>
            File.WriteAllLines("files/output.tsv", output, Encoding.UTF8);
    }
}
