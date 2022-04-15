using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.Models
{
    internal class Statistic : INotifyPropertyChanged
    {
        public User User { get; set; }

        private int _NrOfGames = 0;
        public int NrOfGames
        {
            get => _NrOfGames;
            set
            {
                _NrOfGames = value;
                NotifyPropertyChanged("NrOfGames");
            }
        }


        private int _NrOfWins = 0;

        public int NrOfWins
        {
            get => _NrOfWins;
            set
            {
                _NrOfWins = value;
                NotifyPropertyChanged("NrOfWins");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
