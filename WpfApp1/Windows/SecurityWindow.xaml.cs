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
    /// Логика взаимодействия для SecurityWindow.xaml
    /// </summary>
    public partial class SecurityWindow : Window
    {
        Staff staff;
        SecurityReport report;

        public SecurityWindow(Staff staff)
        {
            InitializeComponent();

            this.staff = staff;

            DGUpdate();
        }

        private void DGUpdate()
        {
            Entity entity = new Entity();

            dgReports.ItemsSource = entity.SecurityReports.ToList();
        }

        private void dgReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SecurityReport currentReport = dgReports.SelectedItem as SecurityReport;

            if(currentReport == null)
            {
                return;
            }

            report = currentReport;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbReport.Text.Length > 300)
            {
                return;
            }

            if(tbReport.Text == string.Empty)
            {
                return;
            }

            Entity entity = new Entity();

            SecurityReport currentReport = new SecurityReport();

            currentReport.Staff = staff.Id;
            currentReport.Description = tbReport.Text;
            if (cbDegree.SelectedIndex != -1)
            {
                ComboBoxItem item = (ComboBoxItem)cbDegree.SelectedItem;

                currentReport.TheDegreeOfThreat = Convert.ToInt32(item.Content.ToString());
            }

            entity.SecurityReports.Add(currentReport);
            entity.SaveChanges();

            DGUpdate();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (report == null)
            {
                return;
            }

            Entity entity = new Entity();
            entity.SecurityReports.Remove(report);
            entity.SaveChanges();

            DGUpdate();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(report == null)
            {
                return;
            }

            if (cbDegree.SelectedIndex != -1)
            {
                ComboBoxItem item = (ComboBoxItem)cbDegree.SelectedItem;

                report.TheDegreeOfThreat = Convert.ToInt32(item.Content.ToString());
            }

            if (tbReport.Text == string.Empty)
            {
                return;
            }

            if (tbReport.Text.Length > 300)
            {
                return;
            }

            report.Description = tbReport.Text;

            Entity entity = new Entity();
            entity.SecurityReports.Update(report);
            entity.SaveChanges();

            DGUpdate();
        }
    }
}
