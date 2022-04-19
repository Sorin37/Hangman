using Hangman.Models;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for Hangman_Game.xaml
    /// </summary>
    public partial class Hangman_Game : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private DateTime deadline;
        private Dictionary<string, List<string>> categories = new Dictionary<string, List<string>>();

        public Hangman_Game(StateOfTheGame previousGame)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            //initialize the current game
            StateOfTheGame currentGame = (DataContext as HangmanVM).Game;
            currentGame.CurrentUser.Id = previousGame.CurrentUser.Id;
            currentGame.CurrentUser.Name = previousGame.CurrentUser.Name;
            currentGame.CurrentUser.Avatar = previousGame.CurrentUser.Avatar;
            if (previousGame.Category != null)
            {
                currentGame.Category = previousGame.Category;
                currentGame.Level = previousGame.Level;
            }

            //read the categories from json
            string categs = File.ReadAllText(".\\categories.json");
            categories = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(categs);

            //populate the categories menu
            foreach (string key in categories.Keys)
            {
                MenuItem mi = new MenuItem();
                mi.Header = key;
                mi.IsCheckable = true;
                mi.Click += MenuItemCategories_Click;
                CategoriesMenuItem.Items.Add(mi);
            }

            if (previousGame.SecretWord == null)
            {
                if (currentGame.Level > 1)
                {
                    foreach (MenuItem menuItem in CategoriesMenuItem.Items)
                    {
                        menuItem.IsEnabled = false;
                    }
                    initializeGame(currentGame.Category);
                }

            }
            else
            {
                //when saved game is opened
                currentGame.SecretWord = previousGame.SecretWord;
                currentGame.RemainingTime = previousGame.RemainingTime;
                deadline = DateTime.Now.AddSeconds(currentGame.RemainingTime);
                dispatcherTimer.Start();
                currentGame.Mistakes = previousGame.Mistakes;
                if (currentGame.Mistakes == 1)
                {
                    MistakeBox1.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/1.png";
                }
                else if (currentGame.Mistakes == 2)
                {
                    MistakeBox1.Text = "X";
                    MistakeBox2.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/2.png";
                }
                else if (currentGame.Mistakes == 3)
                {
                    MistakeBox1.Text = "X";
                    MistakeBox2.Text = "X";
                    MistakeBox3.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/3.png";
                }
                else if (currentGame.Mistakes == 4)
                {
                    MistakeBox1.Text = "X";
                    MistakeBox2.Text = "X";
                    MistakeBox3.Text = "X";
                    MistakeBox4.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/4.png";
                }
                else if (currentGame.Mistakes == 5)
                {
                    MistakeBox1.Text = "X";
                    MistakeBox2.Text = "X";
                    MistakeBox3.Text = "X";
                    MistakeBox4.Text = "X";
                    MistakeBox5.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/5.png";
                }
            }

            (DataContext as HangmanVM).Game.RemainingTime = 30;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            about.Show();
        }

        private void MenuItemCategories_Click(object sender, RoutedEventArgs e)
        {
            foreach (MenuItem menuItem in CategoriesMenuItem.Items)
            {
                //if(menuItem.Header != (sender as MenuItem).Header)
                //{
                menuItem.IsEnabled = false;
                //}
            }
            string selectedCategory = (sender as MenuItem).Header.ToString();

            initializeGame(selectedCategory);
        }

        private void initializeGame(string category)
        {
            Random rnd = new Random();
            if (category == "All categories")
            {
                var randomKey = categories.ElementAt(rnd.Next(0, categories.Count));
                (DataContext as HangmanVM).Game.Category = "All categories";
                (DataContext as HangmanVM).Game.SecretWord.Word = randomKey.Value[rnd.Next(0, categories[randomKey.Key].Count)];

                var charWord = (DataContext as HangmanVM).Game.SecretWord.Word.ToCharArray();
                char[] maskedWord = new char[charWord.Length * 2];

                for (int i = 0; i < charWord.Length - 1; i++)
                {
                    if (Char.IsLetter(charWord[i]))
                    {
                        maskedWord[2 * i] = '_';
                    }
                    else
                    {
                        maskedWord[2 * i] = charWord[i];
                    }
                    maskedWord[2 * i + 1] = ' ';
                }

                if (Char.IsLetter(charWord[charWord.Length - 1]))
                {
                    maskedWord[2 * charWord.Length - 2] = '_';
                }
                else
                {
                    maskedWord[2 * charWord.Length - 2] = charWord[charWord.Length - 1];
                }

                (DataContext as HangmanVM).Game.SecretWord.GuessedWord = new string(maskedWord);
            }
            else
            {
                (DataContext as HangmanVM).Game.Category = category;
                var wordToGuess = categories[category][rnd.Next(0, categories[category].Count)];
                (DataContext as HangmanVM).Game.SecretWord.Word = wordToGuess;
                var charWord = wordToGuess.ToCharArray();
                char[] maskedWord = new char[charWord.Length * 2];

                for (int i = 0; i < charWord.Length - 1; i++)
                {
                    if (Char.IsLetter(charWord[i]))
                    {
                        maskedWord[2 * i] = '_';
                    }
                    else
                    {
                        maskedWord[2 * i] = charWord[i];
                    }
                    maskedWord[2 * i + 1] = ' ';
                }

                if (Char.IsLetter(charWord[charWord.Length - 1]))
                {
                    maskedWord[2 * charWord.Length - 2] = '_';
                }
                else
                {
                    maskedWord[2 * charWord.Length - 2] = charWord[charWord.Length - 1];
                }

                wordToGuess = new string(maskedWord);
                (DataContext as HangmanVM).Game.SecretWord.GuessedWord = wordToGuess;
            }

            //start the timer
            deadline = DateTime.Now.AddSeconds(30);
            dispatcherTimer.Start();
        }

        private void LetterPressed(object sender, RoutedEventArgs e)
        {
            SecretWord secretWord = (DataContext as HangmanVM).Game.SecretWord;
            char[] pressedLetter = (sender as Button).Content.ToString().ToCharArray();
            var aux = secretWord.GuessedWord.ToCharArray();

            (sender as Button).IsEnabled = false;

            bool gameLost = false;

            bool letterFound = false;

            for (int i = 0; i < secretWord.Word.Length; i++)
            {
                if (Char.ToUpper(secretWord.Word[i]) == pressedLetter[0])
                {
                    letterFound = true;
                    aux[i * 2] = secretWord.Word[i];
                }
            }

            if (!letterFound)
            {
                int mistakes = ++(DataContext as HangmanVM).Game.Mistakes;
                if (mistakes == 1)
                {
                    MistakeBox1.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/1.png";
                }
                else if (mistakes == 2)
                {
                    MistakeBox2.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/2.png";
                }
                else if (mistakes == 3)
                {
                    MistakeBox3.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/3.png";
                }
                else if (mistakes == 4)
                {
                    MistakeBox4.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/4.png";
                }
                else if (mistakes == 5)
                {
                    MistakeBox5.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/5.png";
                }
                else if (mistakes == 6)
                {
                    dispatcherTimer.Stop();
                    MistakeBox6.Text = "X";
                    (DataContext as HangmanVM).Image = "../Gallow/6.png";
                    MessageBox.Show($"You lost! The word was: {secretWord.Word}");
                    gameLost = true;
                    (DataContext as HangmanVM).Game.RemainingTime = (deadline - DateTime.Now).Seconds + 1;

                    ObservableCollection<Statistic> AllStats;
                    string stats;
                    stats = File.ReadAllText(".\\userInfo.json");
                    AllStats = new ObservableCollection<Statistic>();
                    AllStats = JsonSerializer.Deserialize<ObservableCollection<Statistic>>(stats);
                    AllStats.First(x => x.User.Id == (DataContext as HangmanVM).Game.CurrentUser.Id).NrOfGames++;
                    string newStats = JsonSerializer.Serialize(AllStats);
                    File.WriteAllText(".\\userInfo.json", newStats);

                    Close();

                }
            }

            if (!gameLost)
            {
                bool gameWon = true;

                foreach (char letter in aux)
                {
                    if (letter == '_')
                    {
                        gameWon = false;
                    }
                }

                secretWord.GuessedWord = new string(aux);


                if (gameWon)
                {
                    dispatcherTimer.Stop();
                    if ((DataContext as HangmanVM).Game.Level < 5)
                    {
                        (DataContext as HangmanVM).Game.Level++;
                        (DataContext as HangmanVM).Game.SecretWord = null;
                        Hangman_Game ws = new Hangman_Game((DataContext as HangmanVM).Game);
                        ws.Show();
                    }
                    else
                    {
                        ObservableCollection<Statistic> AllStats;
                        string stats;
                        stats = File.ReadAllText(".\\userInfo.json");
                        AllStats = new ObservableCollection<Statistic>();
                        AllStats = JsonSerializer.Deserialize<ObservableCollection<Statistic>>(stats);
                        AllStats.First(x => x.User.Id == (DataContext as HangmanVM).Game.CurrentUser.Id).NrOfGames++;
                        AllStats.First(x => x.User.Id == (DataContext as HangmanVM).Game.CurrentUser.Id).NrOfWins++;
                        string newStats = JsonSerializer.Serialize(AllStats);
                        File.WriteAllText(".\\userInfo.json", newStats);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    Close();
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int secondsRemaining = (deadline - DateTime.Now).Seconds;
            if (secondsRemaining == 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.IsEnabled = false;
                MessageBox.Show($"Time has expired! The word was: {(DataContext as HangmanVM).Game.SecretWord.Word}");
                //TimeLabel.Content = "0";
                (DataContext as HangmanVM).Game.RemainingTime = 0;
            }
            else
            {
                //TimeLabel.Content = secondsRemaining.ToString();
                (DataContext as HangmanVM).Game.RemainingTime = secondsRemaining;
            }
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            (DataContext as HangmanVM).Game.Category = null;
            Hangman_Game newGame = new Hangman_Game((DataContext as HangmanVM).Game);
            newGame.Show();
            Close();
        }

        private void SaveGame(object sender, RoutedEventArgs e)
        {
            ObservableCollection<StateOfTheGame> games = new ObservableCollection<StateOfTheGame>();
            string json = File.ReadAllText(".\\savedGames.json");
            games = JsonSerializer.Deserialize<ObservableCollection<StateOfTheGame>>(json);
            StateOfTheGame lastSavedGame = new StateOfTheGame();
            lastSavedGame = games.FirstOrDefault(x => x.CurrentUser.Id == (DataContext as HangmanVM).Game.CurrentUser.Id);
            if (lastSavedGame == null)
            {
                games.Add((DataContext as HangmanVM).Game);
            }
            else
            {
                int index = games.IndexOf(games.First(x => x.CurrentUser.Id == (DataContext as HangmanVM).Game.CurrentUser.Id));
                games[index] = (DataContext as HangmanVM).Game;
            }
            json = JsonSerializer.Serialize(games);
            File.WriteAllText(".\\savedGames.json", json);
            Close();
        }
        private void OpenGame(object sender, RoutedEventArgs e)
        {
            ObservableCollection<StateOfTheGame> games = new ObservableCollection<StateOfTheGame>();
            string json = File.ReadAllText(".\\savedGames.json");
            games = JsonSerializer.Deserialize<ObservableCollection<StateOfTheGame>>(json);
            StateOfTheGame lastSavedGame = new StateOfTheGame();
            lastSavedGame = games.FirstOrDefault(x => x.CurrentUser.Id == (DataContext as HangmanVM).Game.CurrentUser.Id);
            if (lastSavedGame == null)
            {
                MessageBox.Show("No saved game for this user");
            }
            else
            {
                games.Remove(lastSavedGame);
                json = JsonSerializer.Serialize(games);
                File.WriteAllText(".\\savedGames.json", json);
                Hangman_Game hg = new Hangman_Game(lastSavedGame);
                hg.Show();
                Close();
            }
        }

        private void Statistics(object sender, RoutedEventArgs e)
        {
            StatisticsView stats = new StatisticsView();
            stats.Show();
        }
    }
}
