using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class ScoreManager
{
    public static void SaveScore(string playerName, int score, string word, bool won, int attemptsUsed, int attemptsLeft)
    {
        // Save individual game score
        var playerScore = new
        {
            playerName,
            score,
            word,
            won,
            attemptsUsed,
            attemptsLeft,
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
        highScores.Add(new HighScore { PlayerName = playerName, Score = score, Word = word, AttemptsUsed = attemptsUsed, AttemptsLeft = attemptsLeft, Date = DateTime.Now });
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score)); // Sort by score descending
        SaveToFile("gameHighScores.json", highScores);
    }

    public static void DisplayHighScores()
    {
        var highScores = LoadFromFile<List<HighScore>>("gameHighScores.json") ?? new List<HighScore>();
        Console.WriteLine("\nHigh Scores:");
        foreach (var highScore in highScores)
        {
            Console.WriteLine($"{highScore.PlayerName}: {highScore.Score} (Word: {highScore.Word}, Attempts Used: {highScore.AttemptsUsed}, Attempts Left: {highScore.AttemptsLeft}, Date: {highScore.Date})");
        }
    }

    private static void SaveToFile<T>(string fileName, T data)
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

    private static T? LoadFromFile<T>(string fileName)
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

    // Class to represent overall game stats
    public class OverallStats
    {
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public double WinLossRatio { get; set; }
        public int WordsGuessed { get; set; }
    }

    // Class to represent a high score
    public class HighScore
    {
        public string PlayerName { get; set; } = "Unknown"; // Initialize with default value
        public int Score { get; set; }
        public string Word { get; set; } = string.Empty; // Initialize with default value
        public int AttemptsUsed { get; set; }
        public int AttemptsLeft { get; set; }
        public DateTime Date { get; set; }
    }
}
