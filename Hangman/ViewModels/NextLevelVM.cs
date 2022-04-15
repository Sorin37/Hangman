using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hangman.ViewModels
{
    internal class NextLevelVM
    {
        public StateOfTheGame Game { get; set; }
        //private NextLevelCommand _NextLevel;
        //public ICommand NextLevel { get => _NextLevel; }
        public NextLevelVM()
        {
            Game = new StateOfTheGame();
            Game.CurrentUser = new User();
            Game.SecretWord = new SecretWord();
            //_NextLevel = new NextLevelCommand(this);
        }
    }
}