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
    /// Логика взаимодействия для BuhgalterWindow.xaml
    /// </summary>
    public partial class BuhgalterWindow : Window
    {
        public BuhgalterWindow()
        {
            InitializeComponent();

            LoadDG();
        }

        private void LoadDG()
        {
            Entity entity = new Entity();

            dgTransactions.ItemsSource = entity.Transactions.ToArray();
        }
    }
}
