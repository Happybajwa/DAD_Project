﻿using DAD_Project.DB;
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

namespace TruckRental_Project.Veiw.StockManagement
{
    /// <summary>
    /// Interaction logic for UpdateTrucksForm.xaml
    /// </summary>
    public partial class UpdateTrucksForm : Window
    {
        IndividualTruck truck;
        public UpdateTrucksForm()
        {
            InitializeComponent();
            this.updateButton.IsEnabled = false;
            this.removeFeatureButton.IsEnabled = false;
            updatePanel.Visibility = Visibility.Hidden;
        }
        //PREVIEWING THE TEXT INPUT IN TEXT BOX
        //MAKING SURE THAT WE DON'T ALLOW USER TO ENTER SPECIAL CHARACTERS
        //DISPLAYING ERROR MESSAGE IN A HIDDEN LABEL
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

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var truck = truckListDataGrid.SelectedItem as IndividualTruck;
                if (truck == null)
                {
                    MessageBox.Show("Please Select a row to update truck information."); 
                }               
                else
                {
                    DAO.updateSelectedTruck(truck);
                    MessageBox.Show("Truck Info has been updated successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OOPS SOMETHING WENT WRONG\n" + ex.Message);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(truckModeltextBox.Text))
            {
                MessageBox.Show("Please Enter Truck Model Number");
            }
            else
            {
                try
                {
                    var truckModel = DAO.searchTruckModelName(truckModeltextBox.Text);
                    if (truckModel == null)
                    {
                        MessageBox.Show("Sorry, No Truck Model found by given model ID");
                    }
                    else
                    {
                        var trucks = truckModel.IndividualTrucks.ToList();
                        if(trucks.Count == 0)
                        {
                            MessageBox.Show("No truck has registered under this truck model name");
                        }
                        truckListDataGrid.ItemsSource = trucks;
                        this.updateButton.IsEnabled = true;
                        updatePanel.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void truckListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (truckListDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please Select a row");
            }
            else
            {
                truck = truckListDataGrid.SelectedItem as IndividualTruck;
                var features = DAO.searchTruckFeatureAssociationByTruckID(truck.TruckId);
                if (features.Count == 0)
                {
                    ErrorLable.Content = "This Truck Does'nt have any features";
                    truckFeatureDataGrid.ItemsSource = features;
                    this.removeFeatureButton.IsEnabled = false;
                }
                else
                {
                    ErrorLable.Content = "";
                    truckFeatureDataGrid.ItemsSource = features;
                    this.removeFeatureButton.IsEnabled = true;
                }
            }
        }

        private void removeFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            if (truckFeatureDataGrid.SelectedItem != null)
            {
                var truckFeatureAssociation = truckFeatureDataGrid.SelectedItem as TruckFeatureAssociation;
                DAO.RemoveTruckFeature(truckFeatureAssociation);
                MessageBox.Show("Truck feature has been removed Successfully");
                var features = DAO.searchTruckFeatureAssociationByTruckID(truck.TruckId);
                truckFeatureDataGrid.ItemsSource = features;
            }
            else
            {
                MessageBox.Show("Please select feature to remove from table.");
            }
        }

        //MAKING SURE THAT WE DONT ALLOW USER
        //TO ADD ALPHBATIC VALUE IN DAILY RENT AND ADVANCE RENT DATAGRID CELL
        private void DataGridCell_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);               
            }
            catch
            {
                e.Handled = true;
            }
        }
    }
}