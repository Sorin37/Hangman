using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hangman
{
    internal class HangmanVM
    {
        public StateOfTheGame Game { get; set; }

        private GuessLetter _pressLetter;
        public ICommand pressLetter { get => _pressLetter; }

        public bool LetterFound { get; set; }
        public HangmanVM()
        {
            Game = new StateOfTheGame();
            Game.CurrentUser = new User();
            Game.SecretWord = new SecretWord();
            _pressLetter = new GuessLetter(this);
            LetterFound = false;
        }
        ~HangmanVM()
        {
            MessageBox.Show(Game.Level.ToString());
        }
    }
}
