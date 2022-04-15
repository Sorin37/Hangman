using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hangman.ViewModels
{
    internal class StatisticsVM
    {
        public ObservableCollection<Statistic> Statistics { get; set; }
        public StatisticsVM()
        {
            Statistics = new ObservableCollection<Statistic>();
            string stats = File.ReadAllText(".\\userInfo.json");
            Statistics = JsonSerializer.Deserialize<ObservableCollection<Statistic>>(stats);
        }
    }
}
