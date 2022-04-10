using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    internal class SecretWord : INotifyPropertyChanged
    {
        private string _word = "";
        public string Word
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
                NotifyPropertyChanged("Word");
            }
        }
        private string _guessedWord = "";
        public string GuessedWord
        {
            get
            {
                return _guessedWord;
            }
            set
            {
                _guessedWord = value;
                NotifyPropertyChanged("GuessedWord");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
