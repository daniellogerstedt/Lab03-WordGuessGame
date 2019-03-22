using System;

namespace WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "./words.txt";
            bool exit = false;
            while (!exit)
            {
                string choice = Menu();
                if (choice.Equals("1")) PlayGame(path);
                else if (choice.Equals("2")) OptionMenu(path);
                else
                {
                    Console.WriteLine("Thank you for playing, goodbye!");
                    exit = true;
                }
            }
            
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
        /// Handles logic for playing the game, including guessing letters and displaying the information for guesses and the word.
        /// </summary>
        /// <param name="path">Requires the path for the file to get words from.</param>
        static void PlayGame(string path)
        {
            string word = GetWord(path);
            char[] wordArray = word.ToCharArray();
            char[] guessArray = new char[wordArray.Length];
            bool endGame = false;
            string incorrect = "";
            string correct = "";
            for (int i = 0; i < wordArray.Length; i++)
            {
                guessArray[i] = '_';
            }

            while (!endGame)
            {
                Console.WriteLine($"The word is {wordArray.Length} letters long");
                Console.WriteLine($"You have incorrectly guessed: {incorrect}");
                Console.WriteLine($"And correctly guessed: {correct}");
                Console.WriteLine();
                Console.WriteLine($"The Word Is: {String.Join(" ", guessArray)}");
                Console.WriteLine();
                Console.WriteLine("What is your next guess?");
                Console.WriteLine();
                string guess = Console.ReadLine();
                char[] input = guess.ToCharArray();
                bool isLetter = false;
                if (input.Length == 1)
                {
                    endGame = true;
                    for (int i = 0; i < wordArray.Length; i++)
                    {
                        if (wordArray[i].Equals(input[0]))
                        {
                            guessArray[i] = input[0];
                            isLetter = true;
                        }
                        if (guessArray[i].Equals("_")) endGame = false;
                    }
                }
                if (isLetter)
                {
                    correct += $" {guess}";
                }
                else
                {
                    incorrect += $" {guess}";
                }
            }
        }

        /// <summary>
        /// This method handles the options menu, including use of the add and delete methods for words.
        /// </summary>
        /// <param name="path">This is the path to the file containing words.</param>
        static void OptionMenu(string path)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Please choose an option");
                Console.WriteLine();
                Console.WriteLine("1. View Words");
                Console.WriteLine();
                Console.WriteLine("2. Add Word");
                Console.WriteLine();
                Console.WriteLine("3. Delete Word");
                Console.WriteLine();
                Console.WriteLine("4. Done");
                Console.WriteLine();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewWords(path);
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.WriteLine("What word do you wish to add?");
                        Console.WriteLine();
                        string wordToAdd = Console.ReadLine();
                        PostWord(path, wordToAdd);
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.WriteLine("What word do you wish to delete?");
                        Console.WriteLine();
                        string wordToDelete = Console.ReadLine();
                        DeleteWord(path, wordToDelete);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine($"{choice} is not a valid option");
                        break;
                }
            }
        }


        /// <summary>
        /// This method reads the words file and displays all available words.
        /// </summary>
        /// <param name="path">The path to the file of words</param>
        static void ViewWords(string path)
        {
            string[] words = System.IO.File.ReadAllLines(path);
            Console.WriteLine();
            Console.WriteLine("The current words that are available are:");
            Console.WriteLine();
            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i]);
            }
            Console.WriteLine();
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
                int contains = 0;
                for (int h = 0; h < words.Length; h++) if (words[h].Equals(word)) contains++;
                if (contains == 0) return;
                string[] newWords = new string[words.Length - contains];
                for (int i = 0, j = 0; i < newWords.Length; i++)
                {
                    if (!words[i].Equals(word))
                    {
                        newWords[j] = words[i];
                        j++;
                    }
                }
                if (!words[newWords.Length].Equals(newWords[newWords.Length - 1]))
                System.IO.File.WriteAllLines(path, newWords);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

    }
}
