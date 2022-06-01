using DAD_Project.Veiw;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TruckRental_Project.Veiw.StockManagement;

namespace DAD_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainWindowDashBoard form = new();
            DisplayPanel.Children.Add(form);
        }

        private void StockManagementButton_Click(object sender, RoutedEventArgs e)
        {
            addNewTruck addTruckForm = new();
            DisplayPanel.Children.Clear();
            DisplayPanel.Children.Add(addTruckForm);    
        }

        private void CustomerManagementButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sorry Not Available\nI have only completed Stock Management Part");
        }

        private void logoutButtonButton_Click(object sender, RoutedEventArgs e)
        {          
            LoginPage page = new();
            page.Show();
            this.Close();
        }

        private void EmployeeManagementButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sorry Not Available\nI have only completed Stock Management Part");
        }

        private void RentalManagementButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sorry Not Available\nI have only completed Stock Management Part");
        }
    }
 

}