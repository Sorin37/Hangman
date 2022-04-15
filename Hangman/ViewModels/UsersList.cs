using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Hangman
{
    internal class UsersList
    {
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Statistic> AllStats { get; set; }
        public User SelectedUser { get; set; }
        public User HighlightedUser { get; set; }
        public ObservableCollection<string> Images { get; set; }

        public ImagePath SelectedImage { get; set; }

        public UsersList()
        {
            Users = new ObservableCollection<User>();
            string stats;
            stats = File.ReadAllText(".\\userInfo.json");
            AllStats = new ObservableCollection<Statistic>();
            AllStats = JsonSerializer.Deserialize<ObservableCollection<Statistic>>(stats);

            foreach (var stat in AllStats)
            {
                Users.Add(new User() { Id = stat.User.Id, Name = stat.User.Name, Avatar = stat.User.Avatar });
            }
            Images = new ObservableCollection<string>();
            string[] images = Directory.GetFiles("../../Avatars");
            foreach (string image in images)
            {
                Images.Add(Path.GetFullPath(image).ToString());
            }
            SelectedImage = new ImagePath();
            SelectedImage.Path = Images[0];
            HighlightedUser = null;
        }

        ~UsersList()
        {
            string stats;
            stats = File.ReadAllText(".\\userInfo.json");
            AllStats = JsonSerializer.Deserialize<ObservableCollection<Statistic>>(stats);
            foreach (var user in Users)
            {
                if (!AllStats.Any(x => x.User.Id == user.Id))
                {
                    AllStats.Add(new Statistic() { User = user, NrOfGames = 0, NrOfWins = 0 });
                }
            }
            string jsonString = JsonSerializer.Serialize(AllStats);
            File.WriteAllText(".\\userInfo.json", jsonString);
        }
    }
}
