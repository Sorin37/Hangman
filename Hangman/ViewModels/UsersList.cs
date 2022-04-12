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
        public User SelectedUser { get; set; }
        public User HighlightedUser { get; set; }
        public ObservableCollection<string> Images { get; set; }

        public ImagePath SelectedImage { get; set; }

        public UsersList()
        {
            Users = new ObservableCollection<User>();
            string users;
            users = File.ReadAllText(".\\userInfo.json");
            Users = JsonSerializer.Deserialize<ObservableCollection<User>>(users);

            Images = new ObservableCollection<string>();
            string[] images = Directory.GetFiles("../../Avatars");
            foreach(string image in images)
            {
                Images.Add(Path.GetFullPath(image).ToString());
            }
            SelectedImage = new ImagePath();
            SelectedImage.Path = Images[0];
            HighlightedUser = null;
        }

        ~UsersList()
        {
            string jsonString = JsonSerializer.Serialize(Users);
            File.WriteAllText(".\\userInfo.json", jsonString);
        }
    }
}
