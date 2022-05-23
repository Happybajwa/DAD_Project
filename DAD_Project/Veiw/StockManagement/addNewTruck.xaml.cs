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
                //SETTING THE YEAR FROM 1950 TO CURRENT YEAR IN COMBOBOX TO
                //AVOID RISK FROM USER TO ENTER THE YEAR WRONG VALUE
                ManufacturingCombobox.ItemsSource = Enumerable.Range(1950, DateTime.UtcNow.Year - 1950).Reverse().ToList();
                //SETTING DEFAULT SELECTED VALUE FOR COMBO BOX
                ManufacturingCombobox.SelectedValue = 2020;
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
        //PREVIEWING THE TEXT INPUT IN TEXT BOX
        //MAKING SURE THAT WE DON'T ALLOW USER TO ENTER SPECIAL CHARACTERS
        //DISPLAYING ERROR MESSAGE IN A HIDDEN LABEL
        //THIS WILL MAKE SURE ONLY TO ALLOW INTEGERS AND ALPHABETS IN TEXT BOX
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
        //PREVIEWING THE TEXT INPUT IN TEXT BOX
        //MAKING SURE THAT WE DON'T ALLOW USER TO ENTER SPECIAL CHARACTERS OR ALPHABETS
        //DISPLAYING ERROR MESSAGE IN A HIDDEN LABEL
        //THIS WILL MAKE SURE ONLY TO ALLOW INTEGERS IN TEXT BOX
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
        //PREVIEWING THE TEXT INPUT IN TEXT BOX
        //MAKING SURE THAT WE DON'T ALLOW USER TO ENTER SPECIAL CHARACTERS OR INTEGERS
        //DISPLAYING ERROR MESSAGE IN A HIDDEN LABEL
        //THIS WILL MAKE SURE ONLY TO ALLOW ALPHABETS IN TEXT BOX
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
            
            //CREATING A TEXTBOX ARRAY TO CHECK NONE OF THE TEXT BOX IS EMPTY
            TextBox[] checkAllTextBoxes = {registraionTextBox, colourTextBox,
                DailyRentTextBox, advanceDepositeTextBox};

            //CREATING A DATEPICKER ARRAY TO CHECK NONE OF THE DATE PICKER IS EMPTY
            DatePicker[] checkAllDatePickers = {WOFDatePicker,
                RegistrationExpiryDatePicker, ImportDatePicker};

            if(inputValidation.IsTextBoxEmpty(checkAllTextBoxes) || inputValidation.IsDatePickerEmpty(checkAllDatePickers)
               || truckModelListBox.SelectedItem == null)
            {
                MessageBox.Show("Please make sure that all the required information is provided\n" +
                    "Note:\n 1. Input boxes and Dates cant be left empty.\n 2. WOF & Registration Dates Cannot be in past");
            }
            else
            {
                try
                {
                    IndividualTruck NewTruck = new();
                    NewTruck.RegistrationNumber = registraionTextBox.Text;
                    NewTruck.Colour = colourTextBox.Text;
                    NewTruck.WofexpiryDate = WOFDatePicker.SelectedDate.Value;
                    NewTruck.RegistrationExpiryDate = RegistrationExpiryDatePicker.SelectedDate.Value;
                    NewTruck.DateImported = ImportDatePicker.SelectedDate.Value;
                    NewTruck.ManufactureYear = int.Parse(ManufacturingCombobox.Text);
                    NewTruck.Status = statusComboBox.Text;
                    NewTruck.FuelType = FuelTypeComboBox.Text.ToLower();
                    NewTruck.Transmission = TransmissionComboBox.Text.ToLower();
                    NewTruck.DailyRentalPrice = int.Parse(DailyRentTextBox.Text);
                    NewTruck.AdvanceDepositRequired = int.Parse(advanceDepositeTextBox.Text);
                    var truckModel = truckModelListBox.SelectedItem as TruckModel;
                    NewTruck.TruckModelId = truckModel.ModelId;
              
                    DAO.AddNewTruck(NewTruck);
                    MessageBox.Show("Truck has been added to system successfully");
                    inputValidation.clearAllInputs(addNewTruckPanel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }              
            }
        }
    }
}
