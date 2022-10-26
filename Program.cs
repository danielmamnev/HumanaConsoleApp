using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Humana.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PromptUser(true);
        }

        private static void PromptUser(bool initialLoad = false)
        {
            if (initialLoad)
                Console.WriteLine("Hello! Please enter a paragraph or type exit to exit");
            else
                Console.WriteLine(Environment.NewLine + "Please enter a paragraph or type exit to exit");

            string paragraph = Console.ReadLine();
            paragraph = paragraph.ToLower();
            if (paragraph == "exit")
            {
                return;
            }
            List<string> sentenceList = paragraph.Split('.').ToList();
            paragraph = Regex.Replace(paragraph, @"[^\w\s]", string.Empty);
            List<string> wordsList = paragraph.Split(' ').ToList();
            List<string> distinctWordsList = wordsList.Distinct().ToList();

            int palindromeWords = wordsList.Where(x => new string(x.ToCharArray().Reverse().ToArray()) == new string(x.ToCharArray())).Count();
            int palindromeSentences = 0;

            foreach (string sentence in sentenceList)
            {
                string[] wordsArr = sentence.Split(' ');
                string[] reversed = wordsArr.Reverse().ToArray();
                if (wordsArr.SequenceEqual(reversed))
                    palindromeSentences++;
            }
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("The number of palindrome words: " + palindromeWords + Environment.NewLine);
            Console.WriteLine("The number of palindrome sentences: " + palindromeSentences + Environment.NewLine);
            if (distinctWordsList.Count() > 0)
            {
                Console.WriteLine("There are " + distinctWordsList.Count() + " unique words in your paragraph. They are listed below: " + Environment.NewLine);
                foreach (var w in distinctWordsList)
                {
                    Console.WriteLine(w + Environment.NewLine);
                }
            }

            void PromptForKey()
            {
                Console.WriteLine("Please Enter a character to retrieve words containing the characater: ");
                ConsoleKeyInfo key = Console.ReadKey();

                List<string> filteredWordsList = wordsList.Where(x => x.Contains(key.KeyChar)).ToList();
                Console.WriteLine(Environment.NewLine);
                foreach (var w in filteredWordsList)
                {
                    Console.WriteLine(w);
                }
                Console.WriteLine("Would you like to enter another character? Press Y otherwise press any key");
                ConsoleKeyInfo response = Console.ReadKey();

                if (response.KeyChar.ToString().ToLower() == "y")
                {
                    Console.WriteLine(Environment.NewLine);
                    PromptForKey();
                }
            }
            PromptForKey();
            PromptUser();
        }
    }
}