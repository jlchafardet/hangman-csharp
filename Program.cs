using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        // Step 1: Load words from the JSON file
        List<string> words = LoadWordsFromFile("wordList.json");

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
            // Display the current state of the guessed word
            Console.WriteLine("Word to guess: " + new string(guessedWord));
            Console.WriteLine("Attempts remaining: " + attemptsRemaining);
            Console.WriteLine("Guessed letters: " + string.Join(", ", guessedLetters));
            DisplayHangman(6 - attemptsRemaining); // Display the current stage of the hangman
            Console.Write("Enter a letter: ");

            // Get the player's guess
            char guess = Console.ReadLine().ToLower()[0];

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

        // Step 5: End game - check if the player has won or lost
        if (new string(guessedWord) == wordToGuess)
        {
            // Display the fully guessed word if the player wins
            Console.WriteLine("Congratulations! You guessed the word: " + wordToGuess);
        }
        else
        {
            // Display the word if the player loses
            Console.WriteLine("Game over! The word was: " + wordToGuess);
            DisplayHangman(6); // Display the final stage of the hangman
        }
    }

    // Function to load words from a JSON file
    static List<string> LoadWordsFromFile(string fileName)
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
            return new List<string> { "programming", "hangman", "challenge", "developer", "console" };
        }
    }

    // Function to display the current stage of the hangman
    static void DisplayHangman(int stage)
    {
        string[] hangmanStages = new string[]
        {
            @"
                -----
                |   |
                    |
                    |
                    |
                    |
              =========",
            @"
                -----
                |   |
                O   |
                    |
                    |
                    |
              =========",
            @"
                -----
                |   |
                O   |
                |   |
                    |
                    |
              =========",
            @"
                -----
                |   |
                O   |
               /|   |
                    |
                    |
              =========",
            @"
                -----
                |   |
                O   |
               /|\  |
                    |
                    |
              =========",
            @"
                -----
                |   |
                O   |
               /|\  |
               /    |
                    |
              =========",
            @"
                -----
                |   |
                O   |
               /|\  |
               / \  |
                    |
              ========="
        };

        Console.WriteLine(hangmanStages[stage]);
    }
}
