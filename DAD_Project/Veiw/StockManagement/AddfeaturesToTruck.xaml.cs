using DAD_Project.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        IndividualTruck truck;
        
        public AddfeaturesToTruck()
        {
            InitializeComponent();
            systemTruckFeatureListBox.ItemsSource = DAO.getAllTruckFeatures();
            afterSearchPanel.Visibility = Visibility.Hidden;
            this.addFeatureToTruckButton.IsEnabled = false;
            warningLabel.Visibility = Visibility.Hidden;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(searchTruckTextBox.Text))
            {
                MessageBox.Show("Please Enter truck registration number to search");
            }
            else
            {
                truck = DAO.getTruckByRegistrationNumber(searchTruckTextBox.Text);
                if (truck != null)
                {
                    TruckModelLable.Content = "Truck Model:  " + truck.TruckModel.Model.ToString();
                    TruckMenfLable.Content = "Truck Manufacturer year:  " + truck.ManufactureYear.ToString();
                    var truckExistingFeatures = DAO.GetAllTruckFeaturesByTruckID(truck.TruckId);
                    afterSearchPanel.Visibility = Visibility.Visible;
                    if (truckExistingFeatures.Count == 0)
                    {
                        TruckFeatureLable.Content = "This Truck Doesn't have any features";
                    }
                    else
                    {
                        existingTruckFeatureListBox.ItemsSource = truckExistingFeatures.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Truck not found with the given registration number");
                }
            }
            
        }

        private void addfeatureToTruck_Click(object sender, RoutedEventArgs e)
        {
            TruckFeature feature = systemTruckFeatureListBox.SelectedItem as TruckFeature;
            if (DAO.DoesTruckHasThisFeatureAlready(truck.TruckId, feature.FeatureId) == true)
            {
                MessageBox.Show("Trcuk has this feature already.");
            }
            else
            {
                DAO.AddFeatureToTruck(truck, feature);
                MessageBox.Show("Feature now has been added to truck");
                addFeatureToTruckButton.IsEnabled = false;
                TruckFeatureLable.Visibility = Visibility.Hidden;
                var truckExistingFeatures = DAO.GetAllTruckFeaturesByTruckID(truck.TruckId);
                existingTruckFeatureListBox.ItemsSource = truckExistingFeatures.ToList();
            }
        }

        private void systemTruckFeatureListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.addFeatureToTruckButton.IsEnabled = true;
        }

        //previewing the text input in text box
        //making sure that we don't allow user to enter special characters
        //displaying error message in a hidden label
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled == true)
            {
                warningLabel.Visibility = Visibility.Visible;
                warningLabel.Content = "No Special Characters Allowed";
            }
            else
            {
                warningLabel.Content = "";
                warningLabel.Visibility = Visibility.Hidden;
            }
        }   
    }
}
