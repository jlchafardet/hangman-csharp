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
        int score = 100; // Initialize score

        // Pause to see any initial errors
        Console.WriteLine("Press any key to start the game...");
        Console.ReadKey();

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

            // Display current score
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Current score: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(score);
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
                    // Deduct points for incorrect guess
                    score -= 10;
                    attemptsRemaining--;
                }
            }
        }

        // Display the final result
        if (new string(guessedWord) == wordToGuess)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations! You guessed the word: " + wordToGuess);
            Console.ResetColor();

            // Ask for player's name
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine() ?? "Unknown";

            // Save the score
            SaveScore(playerName, score, wordToGuess, true);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over! The word was: " + wordToGuess);
            Console.ResetColor();

            // Save the score
            SaveScore("Unknown", score, wordToGuess, false);
        }

        // Display high scores
        DisplayHighScores();

        // Prompt to exit
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    // Function to load words from a JSON file
    static List<string>? LoadWordsFromFile(string fileName)
    {
        try
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<List<string>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading words from file: " + ex.Message);
            return null;
        }
    }

    // Function to clear the screen
    static void ClearScreen()
    {
        Console.Clear();
    }

    // Function to display the hangman
    static void DisplayHangman(int stage)
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

        // Display the appropriate stage
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(hangmanStages[stage]);
        Console.ResetColor();
    }

    // Function to save the score
    static void SaveScore(string playerName, int score, string word, bool won)
    {
        // Save individual game score
        var playerScore = new
        {
            playerName,
            score,
            word,
            won,
            date = DateTime.Now
        };
        SaveToFile("playerScores.json", playerScore);

        // Update overall game stats
        var overallStats = LoadFromFile<OverallStats>("gameOverallStats.json") ?? new OverallStats();
        if (won)
        {
            overallStats.GamesWon++;
            overallStats.WordsGuessed++;
        }
        else
        {
            overallStats.GamesLost++;
        }
        overallStats.WinLossRatio = overallStats.GamesWon / (double)(overallStats.GamesWon + overallStats.GamesLost);
        SaveToFile("gameOverallStats.json", overallStats);

        // Update high scores
        var highScores = LoadFromFile<List<HighScore>>("gameHighScores.json") ?? new List<HighScore>();
        highScores.Add(new HighScore { PlayerName = playerName, Score = score, Date = DateTime.Now });
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score)); // Sort by score descending
        SaveToFile("gameHighScores.json", highScores);
    }

    // Function to save data to a JSON file
    static void SaveToFile<T>(string fileName, T data)
    {
        try
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving data to file: " + ex.Message);
        }
    }

    // Function to load data from a JSON file
    static T? LoadFromFile<T>(string fileName)
    {
        try
        {
            string json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch
        {
            return default;
        }
    }

    // Function to display high scores
    static void DisplayHighScores()
    {
        var highScores = LoadFromFile<List<HighScore>>("gameHighScores.json") ?? new List<HighScore>();
        Console.WriteLine("\nHigh Scores:");
        foreach (var highScore in highScores)
        {
            Console.WriteLine($"{highScore.PlayerName}: {highScore.Score} (Date: {highScore.Date})");
        }
    }

    // Class to represent overall game stats
    class OverallStats
    {
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public double WinLossRatio { get; set; }
        public int WordsGuessed { get; set; }
    }

    // Class to represent a high score
    class HighScore
    {
        public string PlayerName { get; set; } = "Unknown"; // Initialize with default value
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}