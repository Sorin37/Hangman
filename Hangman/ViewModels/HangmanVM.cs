using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hangman
{
    internal class HangmanVM : INotifyPropertyChanged
    {
        public StateOfTheGame Game { get; set; }

        //public ICommand pressLetter { get => _pressLetter; }

        //public bool LetterFound { get; set; }

        private string _Image;
        public string Image
        {
            get => _Image; set
            {
                _Image = value;
                NotifyPropertyChanged("Image");
            }
        }
        private ObservableCollection<string> _Boxes;
        public ObservableCollection<string> Boxes {
            get => _Boxes; set
            {
                _Boxes = value;
                NotifyPropertyChanged("Boxes");
            }
        }

        public HangmanVM()
        {
            Game = new StateOfTheGame();
            Game.CurrentUser = new User();
            Game.SecretWord = new SecretWord();
            //_pressLetter = new GuessLetter(this);
            //LetterFound = false;
            Image = "../Gallow/0.png";
            Boxes = new ObservableCollection<string>() { "", "", "", "", "", ""};
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
