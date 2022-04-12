using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class User : INotifyPropertyChanged
    {
        private int _Id;
        public int Id { get { return _Id; } set { _Id = value; } }
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }
        private string _Avatar;
        public string Avatar
        {
            get
            {
                return _Avatar;
            }
            set
            {
                _Avatar = value;
                NotifyPropertyChanged("Avatar");
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
