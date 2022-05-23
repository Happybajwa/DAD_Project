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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TruckRental_Project;

namespace DAD_Project.Veiw
{
    /// <summary>
    /// Interaction logic for mainWindowDashBoard.xaml
    /// </summary>
    public partial class mainWindowDashBoard : UserControl
    {
        public mainWindowDashBoard()
        {
            InitializeComponent();
            var list = DAO.getAllTruck();
            List<IndividualTruck> availabletrucks = new();
            List<IndividualTruck> rentedtrucks = new();
            List<IndividualTruck> maintenanceTrucks = new();
            foreach (var item in list)
            {
                if(item.Status == "Available for rent")
                {   
                    availabletrucks.Add(item);   
                }
                else if(item.Status =="Rented")
                {     
                    rentedtrucks.Add(item);                   
                }else if(item.Status =="Under Repair")
                {
                    maintenanceTrucks.Add(item);  
                }
            }
            totalTruckLabel.Content = availabletrucks.Count.ToString();
            totalRentedTruckLabel.Content = rentedtrucks.Count.ToString();
            totalMiantenanceLabel.Content = maintenanceTrucks.Count.ToString();

        }
        
    }
}
