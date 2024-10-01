# hangman-csharp

A simple Hangman game written in C#

## Hangman Game

### Features

- A simple console-based Hangman game.
- Randomly selects a word from a predefined list or a JSON file.
- Allows the player to guess letters until they either guess the word or run out of attempts.
- Displays the fully guessed word if the player wins.
- **Graphical Hangman**: Displays a graphical representation of the hangman as the player makes incorrect guesses.
- **Score Tracking**: Tracks and displays the player's score based on their performance, saves scores to a JSON file, and maintains high scores.

### How to Play

1. Run the game using the command:
   ```sh
   dotnet run
   ```
2. The game will display the word to guess with underscores representing each letter.
3. Enter a letter to guess.
4. The game will update the display with the guessed letters, the number of attempts remaining, and the current score.
5. Continue guessing letters until you either guess the word or run out of attempts.
6. If you guess the word correctly, the game will display the fully guessed word and prompt for your name to save the score.
7. The game will display the high scores at the end.

### Word Import

- The game can load words from a JSON file named `wordList.json`.
- If the file is not found or contains invalid data, a default list of words will be used.

### Example JSON File (`wordList.json`)

```json
[
  "example",
  "words",
  "for",
  "hangman",
  "game"
]
```

## Implemented Features Subset

- **Simple Console-Based Hangman Game**: The game is a console-based Hangman game.
- **Random Word Selection**: The game loads words from a JSON file (`wordList.json`). If the file is not found or contains invalid data, it uses a default list of words.
- **Letter Guessing with Attempts Tracking**: The game allows the player to guess letters and tracks the number of attempts remaining.
- **Display Fully Guessed Word**: The game displays the fully guessed word in green if the player wins.
- **Graphical Hangman**: The game displays a graphical representation of the hangman as the player makes incorrect guesses.
- **Enhanced User Interface**: The game uses colors and text formatting to enhance the display. Specific elements like "Word to guess:", "Attempts remaining:", and guessed letters are color-coded.
- **Game Over and Win Messages**: The game displays a congratulatory message in green if the player wins and a game over message in red if the player loses.
- **Score Tracking**: Tracks and displays the player's score based on their performance, saves scores to a JSON file, and maintains high scores.

## Future Feature Implementations

### Word Categories

- **Description**: Allow players to choose from different categories of words (e.g., animals, technology, sports).
- **Implementation Steps**:
  1. Create multiple lists of words, each representing a different category.
  2. Add a menu for players to select a category before starting the game.
  3. Randomly select a word from the chosen category.

### Difficulty Levels

- **Description**: Introduce different difficulty levels (e.g., easy, medium, hard) that affect the number of attempts or the length of the words.
- **Implementation Steps**:
  1. Define difficulty levels with corresponding word lists and attempt limits.
  2. Add a menu for players to select a difficulty level.
  3. Adjust the game settings based on the selected difficulty.

### Hint System

- **Description**: Provide hints to players, such as revealing a letter or giving a clue about the word.
- **Implementation Steps**:
  1. Add a hint option that players can use during the game.
  2. Implement logic to reveal a random letter or provide a textual hint.
  3. Limit the number of hints available to the player.

### Score Tracking

- **Description**: Track and display the player's score based on their performance.
- **Implementation Steps**:
  1. Define a scoring system (e.g., points for correct guesses, penalties for incorrect guesses).
  2. Display the score during and after the game.
  3. Optionally, save high scores to a file or database.

### ~~Graphical Hangman~~

- ~~**Description**: Display a graphical representation of the hangman as the player makes incorrect guesses.~~
- ~~**Implementation Steps**:~~
  ~~1. Create ASCII art for each stage of the hangman.~~
  ~~2. Update the display to show the appropriate stage based on the number of incorrect guesses.~~

### ~~Word Import~~

- ~~**Description**: Allow players to import their own list of words from a file.~~
- ~~**Implementation Steps**:~~
  ~~1. Add an option to load words from a text file.~~
  ~~2. Implement file reading logic to import words into the game.~~
  ~~3. Validate the imported words to ensure they are suitable for the game.~~

### Timed Mode

- **Description**: Introduce a timed mode where players must guess the word within a certain time limit.
- **Implementation Steps**:
  1. Add a timer that counts down during the game.
  2. Display the remaining time to the player.
  3. End the game if the timer reaches zero.

### Customizable Settings

- **Description**: Allow players to customize game settings such as the number of attempts, word length, and hint availability.
- **Implementation Steps**:
  1. Create a settings menu where players can adjust game parameters.
  2. Save and apply the customized settings during the game.

### Sound Effects

- **Description**: Add sound effects for correct guesses, incorrect guesses, and game events.
- **Implementation Steps**:
  1. Integrate a sound library to play audio files.
  2. Add sound effects for different game actions.
  3. Provide an option to enable or disable sound effects.

### ~~Leaderboard~~

- ~~**Description**: Maintain a leaderboard to display the top scores.~~
- ~~**Implementation Steps**:~~
  ~~1. Create a data structure to store high scores.~~
  ~~2. Update the leaderboard with new high scores.~~
  ~~3. Display the leaderboard at the end of the game or on demand.~~

### Save and Load Game

- **Description**: Allow players to save their game progress and load it later.
- **Implementation Steps**:
  1. Implement logic to save the current game state to a file.
  2. Add an option to load a saved game state.
  3. Ensure the game state is correctly restored when loaded.

### ~~Enhanced User Interface~~

- ~~**Description**: Improve the console interface with better formatting and user prompts.~~
- ~~**Implementation Steps**:~~
  ~~1. Use colors and text formatting to enhance the display.~~
  ~~2. Provide clear and user-friendly prompts and messages.~~
  ~~3. Ensure the interface is accessible and easy to navigate.~~

### Word Definition

- **Description**: Provide the definition of the word after the game ends.
- **Implementation Steps**:
  1. Integrate a dictionary API to fetch word definitions.
  2. Display the definition of the word at the end of the game.

### Achievements

- **Description**: Introduce achievements for players to unlock based on their performance.
- **Implementation Steps**:
  1. Define a set of achievements (e.g., winning without incorrect guesses, guessing a word in one attempt).
  2. Track player progress towards achievements.
  3. Display unlocked achievements to the player.