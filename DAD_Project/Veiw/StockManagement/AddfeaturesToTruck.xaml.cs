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
using TruckRental_Project;

namespace DAD_Project.Veiw.StockManagement
{
    
    /// <summary>
    /// Interaction logic for AddfeaturesToTruck.xaml
    /// </summary>
    /// 
    public partial class AddfeaturesToTruck : Window
    {
        
        public AddfeaturesToTruck()
        {
            InitializeComponent();
            featureDataGrid.ItemsSource = DAO.getAllTruckFeatures();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(searchTruckTextBox.Text))
            {
                MessageBox.Show("Please Enter truck rego number to search");
            }
            else
            {
                var truck = DAO.getTruckByRegistrationNumber(searchTruckTextBox.Text);
                TruckModelLable.Content = "Truck Model Name\n\n" +truck.TruckModel.Model.ToString();
                TruckRegoLable.Content = "Truck Manufacturer year\n\n" + truck.ManufactureYear.ToString();
                var truckExistingFeatures = DAO.DoesTruckHasFeatureExistedAlready(truck.TruckId);
                if(truckExistingFeatures.Count == 0)
                {
                    TruckFeatureLable.Content = "This Truck Doesnt have any features";
                }
                else
                {
                    existingTruckFeatureListBox.ItemsSource = truckExistingFeatures.ToList();
                }
            }
        }

        private void addfeatureToTruck_Click(object sender, RoutedEventArgs e)
        {
            var truck = DAO.getTruckByRegistrationNumber(searchTruckTextBox.Text);
            if(truck != null && featureDataGrid.SelectedItem != null)
            {
                TruckFeature feature = featureDataGrid.SelectedItem as TruckFeature;
                if(DAO.DoesTruckHasThisFeature(truck.TruckId, feature.FeatureId) == true)
                {
                    MessageBox.Show("Trcuk has this feature already. Please Select another one");
                }
                else
                {
                    DAO.AddFeatureToTruck(truck, feature);
                    MessageBox.Show("Feature now has been added to truck");
                    var truckExistingFeatures = DAO.DoesTruckHasFeatureExistedAlready(truck.TruckId);
                    existingTruckFeatureListBox.ItemsSource = truckExistingFeatures.ToList();
                }
            }         
        }    
    }
}
