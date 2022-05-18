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
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(searchTruckTextBox.Text))
            {
                MessageBox.Show("Please Enter truck rego number to search");
            }
            else
            {
                truck = DAO.getTruckByRegistrationNumber(searchTruckTextBox.Text);
                if (truck != null)
                {
                    TruckModelLable.Content = "Truck Model:  " + truck.TruckModel.Model.ToString();
                    TruckMenfLable.Content = "Truck Manufacturer year:  " + truck.ManufactureYear.ToString();
                    var truckExistingFeatures = DAO.DoesTruckHasFeatureExistedAlready(truck.TruckId);
                    afterSearchPanel.Visibility = Visibility.Visible;
                    if (truckExistingFeatures.Count == 0)
                    {
                        TruckFeatureLable.Content = "This Truck Doesnt have any features";
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
                var truckExistingFeatures = DAO.DoesTruckHasFeatureExistedAlready(truck.TruckId);
                existingTruckFeatureListBox.ItemsSource = truckExistingFeatures.ToList();
            }
        }

        private void systemTruckFeatureListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.addFeatureToTruckButton.IsEnabled = true;
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled == true)
            {
                var textbox = sender as TextBox;
                textbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                var textbox = sender as TextBox;
                textbox.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }

        private void searchTruckTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]+");
            e.Handled = regex.IsMatch(e.Key.ToString());
            if (e.Handled == true)
            {
                var textbox = sender as TextBox;
                textbox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                var textbox = sender as TextBox;
                textbox.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }
    }
}
