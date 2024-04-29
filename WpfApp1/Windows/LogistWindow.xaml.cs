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
    /// Логика взаимодействия для LogistWindow.xaml
    /// </summary>
    public partial class LogistWindow : Window
    {
        Staff staff;

        Transportation selectedTransportation;

        public LogistWindow(Staff staff)
        {
            InitializeComponent();

            this.staff = staff;

            LoadDG();
        }

        private void LoadDG()
        {
            Entity entity = new Entity();

            dgTransportation.ItemsSource = entity.Transportations.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(tbProducts.Text.Length == 0) 
            {
                return;
            }

            if(tbProducts.Text.Length > 300)
            {
                return;
            }

            if(dpDate.SelectedDate == null)
            {
                return;
            }

            if (tbProducts.Text == string.Empty)
            {
                return;
            }

            Entity entity = new Entity();

            Transportation transportation = new Transportation();

            transportation.Products = tbProducts.Text;
            transportation.DateOfTransportation = DateOnly.FromDateTime(dpDate.SelectedDate.Value.Date);
            transportation.Staff = staff.Id;

            entity.Transportations.Add(transportation);

            entity.SaveChanges();
            LoadDG();
        }

        private void dgTransportation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgTransportation.SelectedItems == null)
            {
                return;
            }

            selectedTransportation = dgTransportation.SelectedItem as Transportation;

            if(selectedTransportation == null)
            {
                return;
            }
            
            tbProducts.Text = selectedTransportation.Products;
            dpDate.SelectedDate = selectedTransportation.DateOfTransportation.ToDateTime(TimeOnly.Parse("10:00 PM")).Date;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (tbProducts.Text.Length == 0)
            {
                return;
            }

            if (tbProducts.Text.Length > 300)
            {
                return;
            }

            if (dpDate.SelectedDate == null)
            {
                return;
            }

            if(tbProducts.Text == string.Empty)
            {
                return;
            }

            Entity entity = new Entity();

            selectedTransportation.Products = tbProducts.Text;
            selectedTransportation.DateOfTransportation = DateOnly.FromDateTime(dpDate.SelectedDate.Value.Date);
            selectedTransportation.Staff = staff.Id;

            entity.Transportations.Update(selectedTransportation);

            entity.SaveChanges();

            LoadDG();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Entity entity = new Entity();

            entity.Transportations.Remove(selectedTransportation);

            entity.SaveChanges();

            LoadDG();
        }
    }
}
