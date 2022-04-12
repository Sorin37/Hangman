using Hangman.Models;
using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hangman.Views
{
    /// <summary>
    /// Interaction logic for WinScreen.xaml
    /// </summary>
    public partial class WinScreen : Window
    {
        public WinScreen(StateOfTheGame previousGame)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            StateOfTheGame game = (DataContext as NextLevelVM).Game;
            game.CurrentUser= new User();
            game.CurrentUser.Id = previousGame.CurrentUser.Id;
            game.CurrentUser.Name = previousGame.CurrentUser.Name;
            game.CurrentUser.Avatar = previousGame.CurrentUser.Avatar;
            game.Category = previousGame.Category;
            game.Level = previousGame.Level + 1;
            MessageBox.Show((DataContext as NextLevelVM).Game.Category);
        }
    }
}
