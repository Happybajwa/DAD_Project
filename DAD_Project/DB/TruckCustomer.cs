using System;
using System.Collections.Generic;

#nullable disable

namespace DAD_Project.DB
{
    public partial class TruckCustomer
    {
        public TruckCustomer()
        {
            TruckRentals = new HashSet<TruckRental>();
        }

        public int CustomerId { get; set; }
        public string LicenseNumber { get; set; }
        public int Age { get; set; }
        public DateTime LicenseExpiryDate { get; set; }

        public virtual TruckPerson Customer { get; set; }
        public virtual ICollection<TruckRental> TruckRentals { get; set; }
    }
}
