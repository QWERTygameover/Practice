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
    /// Логика взаимодействия для HeaderWindow.xaml
    /// </summary>
    public partial class HeaderWindow : Window
    {
        Staff staff;
        Staff selectedStaff;

        Departament selectedDepartament;

        public HeaderWindow(Staff staff)
        {
            InitializeComponent();

            this.staff = staff;

            UpdateDG();
            UpdateWorkerDG();
        }

        private void UpdateDG()
        {
            Entity entity = new Entity();

            dgHeader.ItemsSource = entity.Departaments.Where(_=>_.Staff == staff.Id).ToArray();
        }

        private void UpdateWorkerDG()
        {
            Entity entity = new Entity();

            List<int> workersId = new List<int>();

            string[] workers = tbWorkers.Text.Split();

            foreach (var worker in workers)
            {
                if (worker == string.Empty)
                {
                    continue;
                }

                workersId.Add(Convert.ToInt32(worker));
            }

            if(selectedDepartament == null)
            {
                dgStaff.ItemsSource = entity.Staff.Where(_ => !workersId.Contains(_.Id)).ToArray();
                return;
            }

            dgStaff.ItemsSource = entity.Staff.Where(_ => !selectedDepartament.Workers.Contains(_.Id) && !workersId.Contains(_.Id)).ToArray();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(tbWorkers.Text == string.Empty)
            {
                return;
            }

            string[] workers = tbWorkers.Text.Split();
            List<int> workersId = new List<int>();

            foreach (var worker in workers)
            {
                if(worker == string.Empty)
                {
                    continue;
                }

                workersId.Add(Convert.ToInt32(worker));
            }

            Entity entity = new Entity();

            Departament dep = new Departament();

            dep.Staff = staff.Id;
            dep.Workers = workersId;

            entity.Departaments.Add(dep);
            entity.SaveChanges();
            UpdateDG();
            UpdateWorkerDG();
        }

        private void dgHeader_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgHeader.SelectedItem == null)
            {
                return;
            }

            selectedDepartament = dgHeader.SelectedItem as Departament;

            tbWorkers.Text = string.Empty;

            foreach (int i in selectedDepartament.Workers)
            {
                tbWorkers.Text += i.ToString() + " ";
            }

            UpdateWorkerDG();
        }

        private void btnWorkerClear_Click(object sender, RoutedEventArgs e)
        {
            tbWorkers.Text = null;

            if(selectedDepartament == null)
            {
                return;
            }

            UpdateWorkerDG();
        }

        private void dgStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgStaff.SelectedItem == null) 
            {
                return;
            }

            selectedStaff = dgStaff.SelectedItem as Staff;
        }

        private void btnWorkerAdd_Click(object sender, RoutedEventArgs e)
        {
            tbWorkers.Text += selectedStaff.Id.ToString() + " ";

            UpdateWorkerDG();

            selectedStaff = null;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(selectedDepartament == null)
            {
                return;
            }

            Entity entity = new Entity();

            entity.Remove(selectedDepartament);

            entity.SaveChanges();

            UpdateDG();
            UpdateWorkerDG();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDepartament == null)
            {
                return;
            }

            if (tbWorkers.Text == string.Empty)
            {
                return;
            }

            string[] workers = tbWorkers.Text.Split();
            List<int> workersId = new List<int>();

            foreach (var worker in workers)
            {
                if (worker == string.Empty)
                {
                    continue;
                }

                workersId.Add(Convert.ToInt32(worker));
            }

            Entity entity = new Entity();

            selectedDepartament.Workers = workersId;
            selectedDepartament.Staff = staff.Id;

            entity.Departaments.Update(selectedDepartament);

            entity.SaveChanges();

            UpdateDG();
            UpdateWorkerDG();
        }
    }
}
