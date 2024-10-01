using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Step 1: Load words from the JSON file
        List<string>? words = DataManager.LoadWordsFromFile("wordList.json");

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
        int score = 100; // Initialize score

        // Step 4: Main game loop
        while (attemptsRemaining > 0 && new string(guessedWord) != wordToGuess)
        {
            // Clear the screen
            UIHandler.ClearScreen();

            // Display the current stage of the hangman
            UIHandler.DisplayHangman(6 - attemptsRemaining);

            // Display the current state of the guessed word
            UIHandler.DisplayGuessedWord(guessedWord);

            // Display attempts remaining
            UIHandler.DisplayAttemptsRemaining(attemptsRemaining);

            // Display guessed letters
            UIHandler.DisplayGuessedLetters(guessedLetters, wordToGuess);

            // Display current score
            UIHandler.DisplayScore(score);

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
                    // Deduct points for incorrect guess
                    score -= 10;
                    attemptsRemaining--;
                }
            }
        }

        // Display the final result
        bool won = new string(guessedWord) == wordToGuess;
        UIHandler.DisplayFinalResult(won, wordToGuess);

        // Ask for player's name
        string playerName = UIHandler.GetPlayerName();

        // Save the score
        ScoreManager.SaveScore(playerName, score, wordToGuess, won);

        // Display high scores
        ScoreManager.DisplayHighScores();

        // Prompt to exit
        UIHandler.PromptToExit();
    }
}