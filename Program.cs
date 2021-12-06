using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordsBreaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] allInWords = File.ReadAllLines("files/de-test-words.tsv");

            string[] dictionaryWords = File.ReadAllLines("files/de-dictionary.tsv");

            List<string> smallWordsList = new List<string>();

            string[] output = new string[allInWords.Length];
            
            int counter = 0;

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
                    
                    
                    if (res != null)
                    {
                        output[counter] = $"(in) {word} -> (out) {string.Join(", ", res)}";
                    }
                    else
                    {
                        output[counter] = $"(in) {word} -> (out) {word}";
                    }

                    counter++;
                }
            }

            File.WriteAllLines("files/output.tsv", output, Encoding.UTF8);
        }
    }
}
