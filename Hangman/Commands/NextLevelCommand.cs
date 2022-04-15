using Hangman.Models;
using Hangman.ViewModels;
using Hangman.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hangman.Commands
{
    internal class NextLevelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private NextLevelVM vm;
        public NextLevelCommand(NextLevelVM passedVM)
        {
            vm = passedVM;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (vm.Game.Level == 6)
            {
                ObservableCollection<Statistic> AllStats;
                string stats;
                stats = File.ReadAllText(".\\userInfo.json");
                AllStats = new ObservableCollection<Statistic>();
                AllStats = JsonSerializer.Deserialize<ObservableCollection<Statistic>>(stats);
                AllStats.First(x => x.User.Id == vm.Game.CurrentUser.Id).NrOfWins++;
                AllStats.First(x => x.User.Id == vm.Game.CurrentUser.Id).NrOfGames++;

                stats = JsonSerializer.Serialize(AllStats);
                File.WriteAllText(".\\userInfo.json", stats);

                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else
            {
                Hangman_Game newhg = new Hangman_Game(vm.Game);
                newhg.Show();
            }
        }
    }
}
