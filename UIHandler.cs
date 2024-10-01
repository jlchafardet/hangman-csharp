using System;
using System.Collections.Generic;

public static class UIHandler
{
    // Clears the console screen
    public static void ClearScreen()
    {
        Console.Clear();
    }

    // Displays the hangman graphic based on the number of incorrect guesses
    public static void DisplayHangman(int stage)
    {
        string[] hangmanStages = new string[]
        {
            @"
  +---+
  |   |
      |
      |
      |
      |
=========",
            @"
  +---+
  |   |
  O   |
      |
      |
      |
=========",
            @"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========",
            @"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========",
            @"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========",
            @"
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========",
            @"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
========="
        };

        Console.WriteLine(hangmanStages[stage]);
    }

    // Displays the current state of the guessed word
    public static void DisplayGuessedWord(char[] guessedWord)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Word to guess: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(new string(guessedWord));
        Console.ResetColor();
    }

    // Displays the number of attempts remaining
    public static void DisplayAttemptsRemaining(int attemptsRemaining)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Attempts remaining: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(attemptsRemaining);
        Console.ResetColor();
    }

    // Displays the letters that have been guessed so far
    public static void DisplayGuessedLetters(List<char> guessedLetters, string wordToGuess)
    {
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
    }

    // Displays the current score
    public static void DisplayScore(int score)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Current score: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(score);
        Console.ResetColor();
    }

    // Displays the final result of the game (win or lose)
    public static void DisplayFinalResult(bool won, string wordToGuess)
    {
        if (won)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations! You guessed the word: " + wordToGuess);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over! The word was: " + wordToGuess);
        }
        Console.ResetColor();
    }

    // Displays the definition of the word in a blue ASCII box
    public static void DisplayWordDefinition(string definition)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                      Word Definition                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(definition);
        Console.WriteLine();
    }

    // Prompts the player to enter their name
    public static string GetPlayerName()
    {
        Console.Write("Enter your name: ");
        return Console.ReadLine() ?? "Unknown";
    }

    // Prompts the player to exit the game
    public static void PromptToExit()
    {
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}