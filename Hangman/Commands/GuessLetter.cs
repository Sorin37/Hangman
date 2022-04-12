using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hangman
{
    internal class GuessLetter : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public HangmanVM _vm;
        public GuessLetter(HangmanVM vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (parameter as Button).IsEnabled = false;
            SecretWord secretWord = _vm.Game.SecretWord;
            char[] pressedLetter = (parameter as Button).Content.ToString().ToCharArray();
            var aux = secretWord.GuessedWord.ToCharArray();

            //_vm.LetterFound = false;

            for (int i = 0; i < secretWord.Word.Length; i++)
            {
                if (Char.ToUpper(secretWord.Word[i]) == pressedLetter[0])
                {
                    //_vm.LetterFound = true;
                    aux[i * 2] = secretWord.Word[i];
                }
            }

            secretWord.GuessedWord = new string(aux);
        }
    }
}
