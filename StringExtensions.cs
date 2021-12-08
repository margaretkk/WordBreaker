using System.Collections.Generic;
using System.Linq;

namespace WordsBreaker
{
    internal static class StringExtensions
    {
        public static void WordsProcessing()
        {
            string[] allInWords = FileService.ReadFile("files/de-test-words.tsv");

            string[] dictionaryWords = FileService.ReadFile("files/de-dictionary.tsv");

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
            FileService.OutputFile(output);
        }
        
        public static bool ContainsInnerWord(this string word, string innerWord) =>
            word.ToLower().Contains(innerWord.ToLower());
        
        public static string[] ConsistsOfWords(this string word, List<string> words)
        {
            string[] res = null;

            for (int wordsCount = 2; wordsCount <= words.Count && res == null; wordsCount++)
            {
                int[] wordsIndexes = new int[wordsCount];

                for (int temp = 0; temp < wordsCount; temp++)
                {
                    wordsIndexes[temp] = 0;
                }

                while (true)
                {
                    var currentWord = string.Empty;

                    for (int i = 0; i < wordsCount; i++)
                    {
                        currentWord += words[wordsIndexes[i]];
                    }

                    if (currentWord.ToLower() == word.ToLower())
                    {
                        res = new string[wordsCount];

                        for (int i = 0; i < wordsCount; i++)
                        {
                            res[i] = words[wordsIndexes[i]];
                        }

                        break;
                    }
                    else if (wordsIndexes.Sum(x => x) >= (words.Count - 1) * wordsCount)
                    {
                        break;
                    }
                    else
                    {
                        for (int i = wordsCount - 1; i >= 0; i--)
                        {
                            var currentIndex = wordsIndexes[i];

                            if (currentIndex == words.Count - 1)
                            {
                                wordsIndexes[i] = 0;
                            }
                            else
                            {
                                wordsIndexes[i] = currentIndex + 1;
                                break;
                            }
                        }
                    }
                }
            }
            return res;
        }
    }
}
