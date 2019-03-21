using System;

namespace WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }


        /// <summary>
        /// Reads in the file of words and gets a single word returning it. If the file fails to read it writes the error to the console and just returns the word "Surprise"
        /// </summary>
        /// <param name="path">The path of the file to be read.</param>
        /// <returns>Returns the word to be used by the game.</returns>
        static string GetWord(string path)
        {
            try
            {
            string[] words = System.IO.File.ReadAllLines(path);
            Random random = new Random();
            int wordNumber = random.Next(0, words.Length);
            return words[wordNumber];
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return "Surprise";
            }
        }
    }
}
