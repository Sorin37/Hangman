using Hangman.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as UsersList).HighlightedUser = (sender as ListBox).SelectedItem as User;
            //MainAvatar.Source = new BitmapImage(new Uri((DataContext as UsersList).HighlightedUser.Avatar));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUser au = new AddUser(DataContext);
            au.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            au.ShowDialog();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if ((DataContext as UsersList).Users.Count == 0)
            {
                MessageBox.Show("There are no users left!");
            }
            else
            {
                (DataContext as UsersList).Users.Remove((DataContext as UsersList).HighlightedUser);
                if ((DataContext as UsersList).Users.Count > 0)
                {
                    (DataContext as UsersList).HighlightedUser = (DataContext as UsersList).Users[0];
                }
            }
            //tre sa stergi si userul din statistics si din saved games
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            User highlightedUser = (DataContext as UsersList).HighlightedUser;
            if (highlightedUser == null)
            {
                MessageBox.Show("Please select a user!");
            }
            else
            {
                StateOfTheGame firstGame = new StateOfTheGame() { CurrentUser = highlightedUser};
                Hangman_Game hg = new Hangman_Game(firstGame);
                hg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                hg.Show();
                Close();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
