using Hangman.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        int mistakes = 0;

        public Hangman_Game(User user1)
        {
            InitializeComponent();

            //initialise the current user
            (DataContext as HangmanVM).CurrentUser.Name = user1.Name;
            (DataContext as HangmanVM).CurrentUser.Avatar = user1.Avatar;

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

            TimeLabel.Content = "30";
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
            Random rnd = new Random();
            string selectedCategory = (sender as MenuItem).Header.ToString();

            if (selectedCategory == "All categories")
            {
                var randomKey = categories.ElementAt(rnd.Next(0, categories.Count));
                (DataContext as HangmanVM).Word.Word = randomKey.Value[rnd.Next(0, categories[randomKey.Key].Count)];
            }
            else
            {
                var wordToGuess = categories[selectedCategory][rnd.Next(0, categories[selectedCategory].Count)];
                (DataContext as HangmanVM).Word.Word = wordToGuess;
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
                (DataContext as HangmanVM).Word.GuessedWord = wordToGuess;
            }

            //start the timer
            deadline = DateTime.Now.AddSeconds(30);
            dispatcherTimer.Start();
        }

        private void LetterPressed(object sender, RoutedEventArgs e)
        {
            SecretWord secretWord = (DataContext as HangmanVM).Word;
            char[] pressedLetter = (sender as Button).Content.ToString().ToCharArray();
            var aux = secretWord.GuessedWord.ToCharArray();

            bool letterFound = false;

            for (int i = 0; i < secretWord.Word.Length; i++)
            {
                if (Char.ToUpper(secretWord.Word[i]) == pressedLetter[0])
                {
                    letterFound = true;
                    aux[i * 2] = secretWord.Word[i];
                }
            }

            bool gameLost = false;

            if (!letterFound)
            {
                mistakes++;
                if (mistakes == 1)
                {
                    MistakeBox1.Text = "X";
                    Gallow.Visibility = Visibility.Visible;
                }
                else if (mistakes == 2)
                {
                    MistakeBox2.Text = "X";
                    Noose.Visibility = Visibility.Visible;   
                }
                else if (mistakes == 3)
                {
                    MistakeBox3.Text = "X";
                    Face.Visibility = Visibility.Visible;
                }
                else if (mistakes == 4)
                {
                    MistakeBox4.Text = "X";
                    Body.Visibility = Visibility.Visible;
                }
                else if (mistakes == 5)
                {
                    MistakeBox5.Text = "X";
                    Gasoline.Visibility = Visibility.Visible;
                }
                else if (mistakes == 6)
                {
                    MistakeBox6.Text = "X";
                    Gasoline.Visibility = Visibility.Hidden;
                    Fire.Visibility = Visibility.Visible;
                    MessageBox.Show($"You lost! The word was: {secretWord.Word}");
                    gameLost = true;
                    dispatcherTimer.Stop();
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
                    MessageBox.Show("Congratulations! You won!");
                    dispatcherTimer.Stop();
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
                MessageBox.Show($"Time has expired! The word was: {(DataContext as HangmanVM).Word.Word}");
                TimeLabel.Content = "0";
            }
            else
            {
                TimeLabel.Content = secondsRemaining.ToString();
            }
        }

        //private void Make_Me(object sender, RoutedEventArgs e)
        //{
        //    Dictionary<string, List<string>> xd = new Dictionary<string, List<string>>();
        //    string categs = File.ReadAllText(".\\categories.json");
        //    xd = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(categs);
        //    //xd.Add("Rivers", new List<string>());
        //    //xd["Countries"].Add("Romania");

        //    //string[] countries = File.ReadAllLines(".\\Movies.txt");

        //    //foreach (string country in countries)
        //    //{
        //    //    xd["Rivers"].Add(country);
        //    //}

        //    //string JsonString = JsonSerializer.Serialize(xd);
        //    //File.WriteAllText(".\\categories.json", JsonString);
        //}
    }
}
