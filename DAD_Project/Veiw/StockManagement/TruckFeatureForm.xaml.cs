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
    /// Interaction logic for TruckFeatureForm.xaml
    /// </summary>
    public partial class TruckFeatureForm : Window
    {
        DAD_HarpreetContext ctx = new();
        public TruckFeatureForm()
        {
            InitializeComponent();
            featureDataGrid.ItemsSource = ctx.TruckFeatures.ToList();

        }

        private void AddFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(featureTextBox.Text))
            {
                MessageBox.Show("Please Enter Feature Name First. Feature Name cant be null");
            }
            else
            {
                if(DAO.IsTruckFeatureExists(featureTextBox.Text) == true)
                {
                    MessageBox.Show("Feature already exists in the system.");
                }
                else
                {
                    TruckFeature NewFeature = new TruckFeature();
                    NewFeature.Description = featureTextBox.Text;
                    ctx.TruckFeatures.Add(NewFeature);
                    ctx.SaveChanges();
                    MessageBox.Show("New Trcuk Feature has been added to system successfully");
                    featureTextBox.Clear();
                    featureDataGrid.ItemsSource = ctx.TruckFeatures.ToList();
                }        
            }
        }
    }
}
