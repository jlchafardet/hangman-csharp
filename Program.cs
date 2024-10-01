using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Pause at the beginning of the game
        Console.WriteLine("Press any key to start the game...");
        Console.ReadKey();

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
        int attemptsUsed = 0; // Initialize attempts used

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
            if (!string.IsNullOrEmpty(input))
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
                attemptsUsed++; // Increment attempts used

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

        if (won)
        {
            // Fetch and display the word definition
            string definition = await FetchWordDefinition(wordToGuess);
            UIHandler.DisplayWordDefinition(definition);
        }

        // Ask for player's name
        string playerName = UIHandler.GetPlayerName();

        // Save the score
        ScoreManager.SaveScore(playerName, score, wordToGuess, won, attemptsUsed, attemptsRemaining);

        // Display high scores
        ScoreManager.DisplayHighScores();

        // Prompt to exit
        UIHandler.PromptToExit();
    }

    // This function fetches the definition of a word from an online dictionary API
    static async Task<string> FetchWordDefinition(string word)
    {
        // Create an instance of HttpClient to send HTTP requests
        using HttpClient client = new HttpClient();

        // Construct the URL for the API request using the provided word
        string apiUrl = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";

        // Send a GET request to the API and wait for the response
        HttpResponseMessage response = await client.GetAsync(apiUrl);

        // Ensure the response indicates success (status code 200-299)
        response.EnsureSuccessStatusCode();

        // Read the response body as a string
        string responseBody = await response.Content.ReadAsStringAsync();

        // Deserialize the JSON response into a dynamic object
        dynamic? jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);

        // Check if the JSON response is not null and contains at least one entry
        if (jsonResponse != null && jsonResponse.Count > 0)
        {
            // Get the meanings array from the first entry in the JSON response
            var meanings = jsonResponse[0]?.meanings as IEnumerable<dynamic>;

            // Check if the meanings array is not null
            if (meanings != null)
            {
                // Iterate through each meaning in the meanings array
                foreach (var meaning in meanings)
                {
                    // Get the definitions array from the current meaning
                    var definitions = meaning?.definitions as IEnumerable<dynamic>;

                    // Check if the definitions array is not null
                    if (definitions != null)
                    {
                        // Iterate through each definition in the definitions array
                        foreach (var definition in definitions)
                        {
                            // Get the definition text from the current definition
                            string? def = definition?.definition;

                            // Check if the definition text is not null or empty
                            if (!string.IsNullOrEmpty(def))
                            {
                                // Return the first non-empty definition found
                                return def;
                            }
                        }
                    }
                }
            }
        }

        // Return a default message if no definition is found
        return "Definition not found.";
    }
}