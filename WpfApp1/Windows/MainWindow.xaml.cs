using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Db;
using WpfApp1.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;

            Entity entity = new Entity();

            User user = entity.Users.Where(_=>_.Login == login && _.Password == password).FirstOrDefault();

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            Staff staff = entity.Staff.Where(_=>_.Users == user.Id).FirstOrDefault();

            if(staff == null)
            {
                UserWindow UsWindow = new UserWindow();
                UsWindow.Show();
                Close();
                return;
            }

            switch (staff.Roles)
            {
                case 1:
                    AdminWindow AdWindow = new AdminWindow();
                    AdWindow.Show();
                    Close();
                    break;
                case 2:
                    LogistWindow LoWindow = new LogistWindow(staff);
                    LoWindow.Show();
                    Close();
                    break;
                case 3:
                    SecurityWindow SeWindow = new SecurityWindow(staff);
                    SeWindow.Show();
                    Close();
                    break;
                case 4:
                    BuhgalterWindow BuWindow = new BuhgalterWindow();
                    BuWindow.Show();
                    Close();
                    break;
                case 5:
                    HeaderWindow HeWindow = new HeaderWindow(staff);
                    HeWindow.Show();
                    Close();
                    break;
            }
        }
    }
}