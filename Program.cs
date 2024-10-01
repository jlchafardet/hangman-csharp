using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Ensure this is included

class Program
{
    static void Main()
    {
        // Step 1: Load words from the JSON file
        List<string>? words = LoadWordsFromFile("wordList.json");

        // Check if words is null
        if (words == null || words.Count == 0)
        {
            Console.WriteLine("No words available to play the game.");
            return;
        }

        // Step 2: Select a random word from the list
        Random random = new Random();
        string wordToGuess = words[random.Next(words.Count)];
        char[] guessedWord = new string('_', wordToGuess.Length).ToCharArray();

        // Step 3: Initialize game variables
        int attemptsRemaining = 6; // Number of attempts the player has
        List<char> guessedLetters = new List<char>(); // List to store guessed letters

        // Step 4: Main game loop
        while (attemptsRemaining > 0 && new string(guessedWord) != wordToGuess)
        {
            // Clear the screen
            ClearScreen();

            // Display the current stage of the hangman
            DisplayHangman(6 - attemptsRemaining);

            // Display the current state of the guessed word
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Word to guess: ");
            Console.ResetColor();
            foreach (char c in guessedWord)
            {
                Console.ForegroundColor = c == '_' ? ConsoleColor.White : ConsoleColor.Green;
                Console.Write(c + " ");
            }
            Console.WriteLine();
            Console.ResetColor();

            // Display attempts remaining
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Attempts remaining: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(attemptsRemaining);
            Console.ResetColor();

            // Display guessed letters
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Guessed letters: ");
            foreach (char c in guessedLetters)
            {
                if (wordToGuess.Contains(c))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(c + " ");
            }
            Console.WriteLine();
            Console.ResetColor();

            // Prompt for user input
            Console.Write("\x1b[1mEnter a letter: \x1b[0m");

            // Get the player's guess
            string? input = Console.ReadLine();
            if (input != null && input.Length > 0)
            {
                char guess = input.ToLower()[0];

                // Check if the letter has already been guessed
                if (guessedLetters.Contains(guess))
                {
                    Console.WriteLine("You already guessed that letter. Try again.");
                    continue;
                }

                // Add the guessed letter to the list of guessed letters
                guessedLetters.Add(guess);

                // Check if the guessed letter is in the word
                if (wordToGuess.Contains(guess))
                {
                    // Update the guessed word with the correct letter
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i] == guess)
                        {
                            guessedWord[i] = guess;
                        }
                    }
                }
                else
                {
                    // Decrease the number of attempts remaining
                    attemptsRemaining--;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a letter.");
            }
        }

        // Step 5: End game - check if the player has won or lost
        ClearScreen();
        DisplayHangman(6 - attemptsRemaining); // Display the final stage of the hangman

        if (new string(guessedWord) == wordToGuess)
        {
            // Display the fully guessed word if the player wins
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Congratulations! You guessed the word: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(wordToGuess);
        }
        else
        {
            // Display the word if the player loses
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Game over! The word was: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(wordToGuess);
        }
        Console.ResetColor();

        // Show the final state and wait for the player to press any key
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    // Function to load words from a JSON file
    static List<string>? LoadWordsFromFile(string fileName)
    {
        try
        {
            // Read the JSON file content
            string jsonContent = File.ReadAllText(fileName);
            // Deserialize the JSON content to a list of words
            return JsonConvert.DeserializeObject<List<string>>(jsonContent);
        }
        catch (Exception ex)
        {
            // If there is an error (e.g., file not found, invalid JSON), use a default list of words
            Console.WriteLine("Error loading words from file: " + ex.Message);
            return null; // This is now valid
        }
    }

    // Function to display the current stage of the hangman
    static void DisplayHangman(int stage)
    {
        string[] hangmanStages = new string[]
        {
            @"
                ----- // Brown color
                |   |
                    |
                    |
                    |
                    |
              =========",
            @"
                ----- // Brown color
                |   |
                O   |
                    |
                    |
                    |
              =========",
            @"
                ----- // Brown color
                |   |
                O   |
                |   |
                    |
                    |
              =========",
            @"
                ----- // Brown color
                |   |
                O   |
               /|   |
                    |
                    |
              =========",
            @"
                ----- // Brown color
                |   |
                O   |
               /|\  |
                    |
                    |
              =========",
            @"
                ----- // Brown color
                |   |
                O   |
               /|\  |
               /    |
                    |
              =========",
            @"
                ----- // Brown color
                |   |
                O   |
               /|\  |
               / \  |
                    |
              ========="
        };

        // Set the color for the hangman stand
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(hangmanStages[stage]);
        Console.ResetColor();
    }

    // Function to clear the console screen
    static void ClearScreen()
    {
        Console.Clear();
    }
}