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
                truckFeatureListBox.ItemsSource = ctx.TruckFeatures.ToList();
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

        private void addNewTruckButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Creating a textbox array to check none of the textbox is empty
            TextBox[] checkAllTextBoxes = {registraionTextBox, colourTextBox, ManufacturingTextBox,
                DailyRentTextBox, advanceDepositeTextBox};

            //Creating a datePicker array to check none of the datepicker i s empty
            DatePicker[] checkAllDatePickers = {WOFDatePicker,
                RegistrationExpiryDatePicker, ImportDatePicker};
            if(inputValidation.IsTextBoxEmpty(checkAllTextBoxes) || inputValidation.IsDatePickerEmpty(checkAllDatePickers)
               || truckModelListBox.SelectedItem == null || truckFeatureListBox.SelectedIndex == -1)
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

                    //inputValidation.ClearAllTextBoxes(addNewTruckPanel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               


            }
        }

        private void AddFeaturesToTruckMenu(object sender, RoutedEventArgs e)
        {
            AddfeaturesToTruck form = new();
            form.ShowDialog();
        }
    }
}
