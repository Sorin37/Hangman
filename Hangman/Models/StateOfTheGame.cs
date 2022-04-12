using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    public class StateOfTheGame : INotifyPropertyChanged
    {
        private User _CurrentUser;
        public User CurrentUser
        {
            get
            {
                return _CurrentUser;
            }
            set
            {
                _CurrentUser = value;
                NotifyPropertyChanged("CurrentUser");
            }
        }
        private int _Mistakes = 0;
        public int Mistakes
        {
            get
            {
                return _Mistakes;
            }
            set
            {
                _Mistakes = value;
                NotifyPropertyChanged("Mistakes");
            }
        }
        private int _Level = 0;
        public int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                _Level = value;
                NotifyPropertyChanged("Level");
            }
        }
        public string LevelText { get { return $"{_Category} level: {_Level}"; } }

        private SecretWord _SecretWord;

        public SecretWord SecretWord
        {
            get
            {
                return _SecretWord;
            }
            set
            {
                _SecretWord = value;
                NotifyPropertyChanged("SecretWord");
            }
        }

        public int _RemainingTime = 30;
        public int RemainingTime
        {
            get
            {
                return _RemainingTime;
            }
            set
            {
                _RemainingTime = value;
                NotifyPropertyChanged("RemainingTime");
            }
        }

        private string _Category = "";

        public string Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
                NotifyPropertyChanged("Category");
                NotifyPropertyChanged("LevelText ");
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
