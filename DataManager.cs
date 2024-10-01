using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class DataManager
{
    public static List<string>? LoadWordsFromFile(string fileName)
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

    public static void SaveScore(string playerName, int score, string word, bool won)
    {
        var playerScore = new
        {
            playerName,
            score,
            word,
            won,
            date = DateTime.Now
        };
        SaveToFile("playerScores.json", playerScore);

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

        var highScores = LoadFromFile<List<HighScore>>("gameHighScores.json") ?? new List<HighScore>();
        highScores.Add(new HighScore { PlayerName = playerName, Score = score, Date = DateTime.Now });
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
        SaveToFile("gameHighScores.json", highScores);
    }

    public static void SaveToFile<T>(string fileName, T data)
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

    public static T? LoadFromFile<T>(string fileName)
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

    public static void DisplayHighScores()
    {
        var highScores = LoadFromFile<List<HighScore>>("gameHighScores.json") ?? new List<HighScore>();
        Console.WriteLine("\nHigh Scores:");
        foreach (var highScore in highScores)
        {
            Console.WriteLine($"{highScore.PlayerName}: {highScore.Score} (Date: {highScore.Date})");
        }
    }

    public class OverallStats
    {
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public double WinLossRatio { get; set; }
        public int WordsGuessed { get; set; }
    }

    public class HighScore
    {
        public string PlayerName { get; set; } = "Unknown";
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}
