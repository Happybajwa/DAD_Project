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
     
        private void AddModelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox[] textboxes = {modelNameTextBox, manufacturerTextBox};
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
                    NewModel.Seats = int.Parse(seatsComboBox.Text);
                    NewModel.Doors = int.Parse(doorsComboBox.Text);

                    try
                    {
                        DAO.AddNewTruckModel(NewModel);
                        MessageBox.Show("New truck model has been added to system Successfully");
                        truckModelsListBox.ItemsSource = ctx.TruckModels.ToList();

                        inputValidation.clearAllInputs(inputStackPanel);
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
