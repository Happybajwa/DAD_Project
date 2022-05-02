using System;
using System.Collections.Generic;

#nullable disable

namespace DAD_Project.DB
{
    public partial class TruckModel
    {
        public TruckModel()
        {
            IndividualTrucks = new HashSet<IndividualTruck>();
        }

        public int ModelId { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Size { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }

        public virtual ICollection<IndividualTruck> IndividualTrucks { get; set; }
    }
}
