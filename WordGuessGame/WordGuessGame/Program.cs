﻿using System;

namespace WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        /// <summary>
        /// Displays the main menu and waits for user input.
        /// </summary>
        /// <returns>Returns the users choice from the menu options</returns>
        static string Menu()
        {
            string choice;
            while (true)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine();
                Console.WriteLine("1. Play Game");
                Console.WriteLine();
                Console.WriteLine("2. Options");
                Console.WriteLine();
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                choice = Console.ReadLine();
                if (choice.Equals("1") || choice.Equals("2"))
                {
                    return choice;
                }
                else if (choice.Equals("3"))
                {
                    Console.WriteLine();
                    Console.WriteLine("You've selected exit, are you sure you would like to exit? Y/N");
                    Console.WriteLine();
                    choice = Console.ReadLine();
                    if (choice.Equals("Y") || choice.Equals("y")) return "3";
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"{choice} is not a valid input, please select from the menu (ex: '1' to play)");
                    Console.WriteLine();
                }
            }
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

        /// <summary>
        /// Posts a word to the file storing all words for the game.
        /// </summary>
        /// <param name="path">Path of the file to write to.</param>
        /// <param name="word">Word to be added to the file.</param>
        static void PostWord(string path, string word)
        {
            try
            {
                string[] wordsToAdd = { word };
                System.IO.File.AppendAllLines(path, wordsToAdd);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Deletes a word from the file containing words for the game.
        /// </summary>
        /// <param name="path">Path of the file full of words.</param>
        /// <param name="word">The word to be deleted.</param>
        static void DeleteWord(string path, string word)
        {
            try
            {
                string[] words = System.IO.File.ReadAllLines(path);
                string[] newWords = new string[words.Length - 1];
                for (int i = 0, j = 0; i < newWords.Length; i++)
                {
                    if (!words[i].Equals(word))
                    {
                        newWords[j] = words[i];
                        j++;
                    }
                }
                System.IO.File.WriteAllLines(path, newWords);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

    }
}
