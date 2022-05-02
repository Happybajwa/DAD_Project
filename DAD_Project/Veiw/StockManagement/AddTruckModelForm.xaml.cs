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
