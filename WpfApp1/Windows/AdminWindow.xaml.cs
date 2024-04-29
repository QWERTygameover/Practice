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
using WpfApp1.Db;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        User selectedUser;

        public AdminWindow()
        {
            InitializeComponent();

            UpdateDG();
        }

        private void UpdateDG()
        {
            Entity entity = new Entity();

            dgUsers.ItemsSource = entity.Users.ToList();
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = dgUsers.SelectedItem as User;

            if(user == null)
            {
                return;
            }

            selectedUser = user;

            tbLogin.Text = user.Login;
            tbPassword.Text = user.Password;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Entity entity = new Entity();

            if (!CheckUser())
            {
                MessageBox.Show("Логин уже занят");
                return;
            }

            if (tbLogin.Text.Length > 20)
            {
                return;
            }

            if (tbPassword.Text.Length > 300)
            {
                return;
            }

            if (tbLogin.Text == string.Empty)
            {
                return;
            }

            if (tbPassword.Text == string.Empty)
            {
                return;
            }

            User user = new User();

            user.Login = tbLogin.Text;
            user.Password = tbPassword.Text;

            entity.Users.Add(user);
            entity.SaveChanges();

            UpdateDG();
        }

        private bool CheckUser()
        {
            Entity entity = new Entity();

            User validUser = entity.Users.Where(_ => _.Login == tbLogin.Text).FirstOrDefault();

            if (validUser != null)
            {
                return false;
            }

            return true;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(selectedUser == null)
            {
                return;
            }

            Entity entity = new Entity();

            entity.Users.Remove(selectedUser);
            entity.SaveChanges();
            UpdateDG();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckUser())
            {
                MessageBox.Show("Логин уже занят");
                return;
            }

            if(selectedUser == null)
            {
                return;
            }

            if(tbLogin.Text == string.Empty)
            {
                return;
            }

            if(tbPassword.Text == string.Empty)
            {
                return;
            }

            if(tbLogin.Text.Length > 20)
            {
                return;
            }

            if(tbPassword.Text.Length > 300) 
            {
                return;
            }

            Entity entity = new Entity();

            selectedUser.Login = tbLogin.Text;
            selectedUser.Password = tbPassword.Text;
            entity.Users.Update(selectedUser);

            entity.SaveChanges();

            UpdateDG();
        }
    }
}
