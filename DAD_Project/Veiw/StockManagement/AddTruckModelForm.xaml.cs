using Microsoft.EntityFrameworkCore;
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
using System.Text.RegularExpressions;

namespace TruckRental_Project.Veiw.StockManagement
{
    /// <summary>
    /// Interaction logic for AddTruckModelForm.xaml
    /// </summary>
    public partial class AddTruckModelForm : Window
    {
        public AddTruckModelForm()
        {
            InitializeComponent();
            using (DAD_HarpreetContext ctx = new())
            {
                truckModelsListBox.ItemsSource = ctx.TruckModels.ToList();
            }
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
                warningLabel.Content = "No Special Characters Are Allowed";
            }
            else
            {
                warningLabel.Content = "";
                warningLabel.Visibility = Visibility.Hidden;
            }
        }
        //previewing the text input in text box
        //making sure that we don't allow user to enter special characters or alphabets
        //displaying error message in a hidden label
        //this will make sure only to allow integers in text box
        private void PreviewTextInput2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled == true)
            {
                warningLabel.Visibility = Visibility.Visible;
                warningLabel.Content = "Only Numbers Are Allowed";
            }
            else
            {
                warningLabel.Content = "";
                warningLabel.Visibility = Visibility.Hidden;
            }
        }

        private void AddModelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textboxes = {modelNameTextBox, manufacturerTextBox,
                                    seatsTextBox, doorstextBox};
            if(inputValidation.IsTextBoxEmpty(textboxes) == true)
            {
                MessageBox.Show("Please fill in all the required information");
            }
            else
            {
                using (DAD_HarpreetContext ctx = new())
                {
                    TruckModel NewModel = new();
                    NewModel.Model = modelNameTextBox.Text;
                    NewModel.Manufacturer = manufacturerTextBox.Text;
                    NewModel.Size = sizeComboBox.Text.ToLower();
                    NewModel.Seats = int.Parse(seatsTextBox.Text);
                    NewModel.Doors = int.Parse(doorstextBox.Text);

                    try
                    {
                        DAO.AddNewTruckModel(NewModel);
                        MessageBox.Show("New truck model has been added to system Successfully");
                        truckModelsListBox.ItemsSource = ctx.TruckModels.ToList();

                        inputValidation.ClearAllTextBoxes(inputStackPanel);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }                     
                }
            }
        }
    }
}
