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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAD_Project.DB;
using DAD_Project.Veiw.StockManagement;

namespace TruckRental_Project.Veiw.StockManagement
{
    /// <summary>
    /// Interaction logic for addNewTruck.xaml
    /// </summary>
    public partial class addNewTruck : UserControl
    {
        public addNewTruck()
        {
            InitializeComponent();
            using (DAD_HarpreetContext ctx = new())
            {
                truckModelListBox.ItemsSource = ctx.TruckModels.ToList();
            }
        }
     
        private void AvailableTruckMenu(object sender, RoutedEventArgs e)
        {
            AvailableTrucksForRentForm availableTrucksForRentForm = new AvailableTrucksForRentForm();
            availableTrucksForRentForm.ShowDialog();

        }

        private void addTruckModelMenu(object sender, RoutedEventArgs e)
        {
            AddTruckModelForm addTruckModelForm = new AddTruckModelForm();
            addTruckModelForm.ShowDialog();
        }

        private void addNewFeatureMenu(object sender, RoutedEventArgs e)
        {
           TruckFeatureForm featureForm = new TruckFeatureForm();
           featureForm.ShowDialog();
        }

        private void UpdateTruckMenu(object sender, RoutedEventArgs e)
        {
            UpdateTrucksForm updateTrucksForm = new UpdateTrucksForm();
            updateTrucksForm.ShowDialog();
        }

        private void AddFeaturesToTruckMenu(object sender, RoutedEventArgs e)
        {
            AddfeaturesToTruck form = new();
            form.ShowDialog();
        }
        //previewing the text input in text box
        //making sure that we don't allow user to enter special characters
        //displaying error message in a hidden label
        //this will make sure only to allow integers and alphabets in text box
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
        private void PreviewTextInputIntegerOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
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
        //previewing the text input in text box
        //making sure that we don't allow user to enter special characters or integers
        //displaying error message in a hidden label
        //this will make sure only to allow alphabets in text box
        private void PreviewTextInputAlphabetsOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Handled == true)
            {
                warningLabel.Visibility = Visibility.Visible;
                warningLabel.Content = "Only Alphabets Are Allowed";
            }
            else
            {
                warningLabel.Content = "";
                warningLabel.Visibility = Visibility.Hidden;
            }
        }
        private void addNewTruckButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Creating a textbox array to check none of the textbox is empty
            TextBox[] checkAllTextBoxes = {registraionTextBox, colourTextBox, ManufacturingTextBox,
                DailyRentTextBox, advanceDepositeTextBox};

            //Creating a datePicker array to check none of the datepicker is empty
            DatePicker[] checkAllDatePickers = {WOFDatePicker,
                RegistrationExpiryDatePicker, ImportDatePicker};

            if(inputValidation.IsTextBoxEmpty(checkAllTextBoxes) || inputValidation.IsDatePickerEmpty(checkAllDatePickers)
               || truckModelListBox.SelectedItem == null)
            {
                MessageBox.Show("Please make sure that all the required information is provided\n" +
                    "Note: Input boxes and Dates cant be left empty.");
            }
            else
            {

                IndividualTruck NewTruck = new();
                NewTruck.RegistrationNumber = registraionTextBox.Text;
                NewTruck.Colour = colourTextBox.Text;
                NewTruck.WofexpiryDate = WOFDatePicker.SelectedDate.Value;
                NewTruck.RegistrationExpiryDate = RegistrationExpiryDatePicker.SelectedDate.Value;
                NewTruck.DateImported = ImportDatePicker.SelectedDate.Value;
                NewTruck.ManufactureYear = int.Parse(ManufacturingTextBox.Text);
                NewTruck.Status = statusComboBox.Text;
                NewTruck.FuelType = FuelTypeComboBox.Text.ToLower();
                NewTruck.Transmission = TransmissionComboBox.Text.ToLower();
                NewTruck.DailyRentalPrice = int.Parse(DailyRentTextBox.Text);
                NewTruck.AdvanceDepositRequired = int.Parse(advanceDepositeTextBox.Text);
                var truckModel = truckModelListBox.SelectedItem as TruckModel;
                NewTruck.TruckModelId = truckModel.ModelId;
                try
                {
                    DAO.AddNewTruck(NewTruck);
                    MessageBox.Show("Truck has been added to system successfully");
                    var truck = DAO.getTruckByRegistrationNumber(NewTruck.RegistrationNumber);

                    inputValidation.ClearAllTextBoxes(addNewTruckPanel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }              
            }
        }
    }
}
