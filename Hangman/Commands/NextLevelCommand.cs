using Hangman.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Hangman_Game newhg = new Hangman_Game(vm.Game);
            newhg.Show();
        }
    }
}
