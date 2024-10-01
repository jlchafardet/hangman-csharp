using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Step 1: Define the list of words for the game
        List<string> words = new List<string> { "programming", "hangman", "challenge", "developer", "console" };

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
            Console.WriteLine("Congratulations! You guessed the word: " + wordToGuess);
        }
        else
        {
            Console.WriteLine("Game over! The word was: " + wordToGuess);
        }
    }
}
