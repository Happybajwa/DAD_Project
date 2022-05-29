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
        public int _Seats;
        public int _Doors;

        //INPUT VALIDATION
        //FOR TRUCK DOORS AS TRUCK CANNOT HAVE MORE THAN 4 DOORS
        //OR A NEGATIVE VALUE
        public int Doors
        {
            get
            {
                return _Doors;
            }
            set
            {
                if(value > 4)
                {
                    throw new Exception("Truck cannot have more than 4 Doors");
                }
                else if(value < 0)
                {
                    throw new Exception("Invalid Input for truck doors");
                }
                else
                {
                    _Doors = value;
                }
            }
        }
        //INPUT VALIDATION
        //FOR TRUCK SEATS AS TRUCK CANNOT HAVE MORE THAN 5 SEATS
        //OR A NEGATIVE VALUE
        public int Seats
        {
            get
            {
                return _Seats;
            }
            set
            {
                if (value > 5)
                {
                    throw new Exception("Truck cannot have more than 5 Seats");
                }
                else if (value < 0)
                {
                    throw new Exception("Invalid Input for Truck Seats");
                }
                else
                {
                    _Seats = value;
                }
            }
        }
        public virtual ICollection<IndividualTruck> IndividualTrucks { get; set; }
    }
}
