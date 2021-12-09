using System;
using System.Collections.Generic;
using System.Linq;

namespace WordsBreaker
{
    internal class WordsBreaker
    {
        private static WordsBreaker instance;
        
        public static WordsBreaker Instance => 
            instance ??= new WordsBreaker();

        private string pathToDictionary = "files/de-dictionary.tsv";
        private string pathToTaskWords = "files/de-test-words.tsv";

        public void Run()
        {
            var dictionary = 
                FileService.Instance.ReadFile(pathToDictionary);
            var task = 
                FileService.Instance.ReadFile(pathToTaskWords);
            
            List<string> smallWordsList = new List<string>();

            string[] output = new string[task.Complete.Length];

            smallWordsList.Clear();

            int counter = 0;

            foreach (var word in task.Complete)
            {
                foreach (var dictionaryWord in dictionary.Complete)
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

                    var result = word.ConsistsOfWords(smallWordsList);


                    if (result != null)
                    {
                        output[counter] = $"(in) {word} -> (out) {string.Join(", ", result)}";
                    }
                    else
                    {
                        output[counter] = $"(in) {word} -> (out) {word}";
                    }

                    counter++;
                }

            }

            FileService.Instance.WriteFile(output);
        }
    }
}
