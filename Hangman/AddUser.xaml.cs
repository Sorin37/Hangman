using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User() { Name = firstTxt.Text, Avatar = avatar.Source.ToString()};
            (DataContext as UsersList).Users.Add(user);
            MessageBox.Show("User added!");
        }

        private void imageLeft_Click(object sender, RoutedEventArgs e)
        {
            var images = (DataContext as UsersList).Images;
            int indexOfLastImage = images.IndexOf((DataContext as UsersList).SelectedImage.Path);
            if (indexOfLastImage == 0)
                indexOfLastImage = images.Count();
            (DataContext as UsersList).SelectedImage.Path = images[indexOfLastImage - 1];
        }

        private void imageRight_Click(object sender, RoutedEventArgs e)
        {
            var images = (DataContext as UsersList).Images;
            int indexOfLastImage = images.IndexOf((DataContext as UsersList).SelectedImage.Path);
            if (indexOfLastImage == images.Count() - 1)
                indexOfLastImage = -1;
            (DataContext as UsersList).SelectedImage.Path = images[indexOfLastImage + 1];
        }
    }
}
