using System;
using System.IO;
using System.Text;

namespace WordsBreaker
{
    internal static class FileService
    {
         private static FileService instance;

        public static FileService Instance => instance ??= new FileService();

        public Result<string[]> ReadFile(string path) 
        {
            var res = new Result<string[]>();

            try
            {
                var readFile = File.ReadAllLines(path);

                res.SetSuccess(readFile);
            }
            catch (Exception ex)
            {
                res.SetFailure(ex);
            }

            return res;
        }

        public Result WriteFile(string[] result)
        {
            var res = new Result();

            try
            {
                File.WriteAllLines("files/output.tsv", result, Encoding.UTF8);

                res.SetSuccess();
            }
            catch (Exception ex)
            {
                res.SetFailure(ex);
            }

            return res;
        }
    }
}
