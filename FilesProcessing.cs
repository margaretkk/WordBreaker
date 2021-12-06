using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordsBreaker
{
    internal static class FilesProcessing
    {

        public static string[] GetPartsOfWords()
        {
            var allInWords = FilesProcessing.ReadFile("files/de-test-words.tsv");

            var dictionaryWords = FilesProcessing.ReadFile("files/de-dictionary.tsv");

            var smallWordsList = new List<string>();

            string[] output = null;

            foreach (var word in allInWords)
            {
                smallWordsList.Clear();

                foreach (var dictionaryWord in dictionaryWords)
                {
                    if (word.Length > dictionaryWord.Length)
                    {
                        if (word.ContainsInnerWord(dictionaryWord))
                        {
                            smallWordsList.Add(dictionaryWord);
                        }
                    }
                }

                if (smallWordsList.Any())
                {
                    for (int i = 0; i < smallWordsList.Count; i++)
                    {
                        var currentWord = smallWordsList[i];

                        for (int j = 0; j < smallWordsList.Count; j++)
                        {
                            var tempWord = smallWordsList[j];

                            if (tempWord.Length < currentWord.Length &&
                                currentWord.ContainsInnerWord(tempWord))
                            {
                                smallWordsList.Remove(tempWord);

                                if (i > -1)
                                {
                                    i--;
                                }
                                j--;
                            }
                        }
                    }

                    var res = word.ConsistsOfWords(smallWordsList);


                    for (int i = 0; i < allInWords.Length; i++)
                    {
                        if (res != null)
                        {
                            output[i] = $"(in) {word} -> (out) {string.Join(", ", res)}";
                        }
                        else
                        {
                            output[i] = $"(in) {word} -> (out) {word}";
                        }
                    }

                }
            }

            return output;
        }
        public static string[] ReadFile (string path) => File.ReadAllLines (path);
        public static void WriteToFile()
        {
            File.WriteAllLines("files/output.tsv", contents: GetPartsOfWords(), Encoding.UTF8);
        }
    }
}
