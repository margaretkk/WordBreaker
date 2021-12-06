using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordsBreaker
{
    internal static class StringExtensions
    {
        public static bool ContainsInnerWord(this string word, string innerWord)
        {
            Regex regex = new Regex($@"{innerWord}(\w*)", RegexOptions.IgnoreCase);
            return regex.Match(word).Success;
        }
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
