using DAD_Project.DB;
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
using TruckRental_Project;
using TruckRental_Project.Veiw.StockManagement;

namespace DAD_Project.Veiw
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void addNewTruckButton_Click(object sender, RoutedEventArgs e)
        {
           string username = usernameTextBox.Text;
           string password = passwordTextBox.Password;
          
            if(username != null && password != null)
            {
                try
                {
                    var role = DAO.isLoginDetailsAreCorrect(username, password);
                    if (role != null && role.Equals("Admin"))
                    {
                        MainWindow mainWindow = new MainWindow();
                        addNewTruck form = new();
                        mainWindow.Show();
                        this.Close();
                    }
                    else if (role != null && role.Equals("Staff"))
                    {
                        MainWindow mainWindow = new();
                        mainWindow.StockManagementButton.IsEnabled = false;
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        errorLabel.Content = "INCORRECT USERNAME OR PASSWORD";
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }   
            }
            else
            {
                MessageBox.Show("Please Enter the Login Details");
            }
        }
    }
}
