using DAD_Project.DB;
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

namespace TruckRental_Project.Veiw.StockManagement
{
    /// <summary>
    /// Interaction logic for UpdateTrucksForm.xaml
    /// </summary>
    public partial class UpdateTrucksForm : Window
    {
        public UpdateTrucksForm()
        {
            InitializeComponent();
            this.updateButton.IsEnabled = false;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(truckModeltextBox.Text))
            {
                MessageBox.Show("Please Enter Truck Model Number");
            }
            else
            {

                var truckModel = DAO.searchTruckModelName(truckModeltextBox.Text);
                if(truckModel == null)
                {
                    MessageBox.Show("Sorry, No Truck Model found by given model ID");
                }
                else
                {
                    truckListDataGrid.ItemsSource = truckModel.IndividualTrucks.ToList();
                    this.updateButton.IsEnabled = true;
                }
            }
        }

        private void truckListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(truckListDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please Select a row");
            }
            else
            {
                var truck = truckListDataGrid.SelectedItem as IndividualTruck;
                var features = DAO.searchTruckFeatureAssociationByTruckID(truck.TruckId);
                if(features.Count == 0)
                {
                    ErrorLable.Content = "This Truck Does'nt have any features";
                    truckFeatureDataGrid.ItemsSource = features;
                }
                else
                {
                    ErrorLable.Content = "";
                    truckFeatureDataGrid.ItemsSource = features;
                }
            }
        }
    }
}
