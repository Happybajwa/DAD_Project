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
using DAD_Project.DB;

namespace TruckRental_Project.Veiw.StockManagement
{
    /// <summary>
    /// Interaction logic for AvailableTrucksForRentForm.xaml
    /// </summary>
    public partial class AvailableTrucksForRentForm : Window
    {
        public AvailableTrucksForRentForm()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var truckList = DAO.SearchTruckByStatus(statusComboBox.Text);
            if (truckList != null)
            {
                truckDataGrid.ItemsSource = truckList.ToList(); 
            }
            else
            {
                MessageBox.Show("no trucks in the sytsem yet");
            }

        }
    }
}
