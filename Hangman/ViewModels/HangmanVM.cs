using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    internal class HangmanVM
    {
        public User CurrentUser { get; set; }
        public SecretWord Word { get; set; }
        public HangmanVM()
        {
            CurrentUser = new User();
            Word = new SecretWord();
        }
    }
}
