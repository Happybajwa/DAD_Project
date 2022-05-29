using System;
using System.Collections.Generic;

#nullable disable

namespace DAD_Project.DB
{
    public partial class IndividualTruck
    {
        public IndividualTruck()
        {
            TruckFeatureAssociations = new HashSet<TruckFeatureAssociation>();
            TruckRentals = new HashSet<TruckRental>();
        }

        public int TruckId { get; set; }
        public string Colour { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime WofexpiryDate { get; set; }
        public DateTime RegistrationExpiryDate { get; set; }
        public DateTime DateImported { get; set; }
        public int ManufactureYear { get; set; }
        public string Status { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }

        public decimal _DailyRentalPrice;

        public decimal _AdvanceDepositRequired;
        public int TruckModelId { get; set; }

        public virtual TruckModel TruckModel { get; set; }
        public virtual ICollection<TruckFeatureAssociation> TruckFeatureAssociations { get; set; }
        public virtual ICollection<TruckRental> TruckRentals { get; set; }

        //Input Validation for daily rent 
        //As daily rent for a truck cannot be less than $ 50
        //Or greater then $1500
        public decimal DailyRentalPrice
        {
            get
            {
                return _DailyRentalPrice;
            }
            set
            {
                if (value < 50)
                {
                    throw new Exception("Daily Rent cannot be less than $50");
                }
                else if (value > 1500)
                {
                    throw new Exception("Daily Rent cannot be greater than $1500");
                }
                else
                {
                    _DailyRentalPrice = value;
                }
            }
        }

        //Input Validation for daily rent 
        //As Advance deposit for a truck cannot be less than $ 50
        //Or greater then $5000
        public decimal AdvanceDepositRequired
        {
            get
            {
                return _AdvanceDepositRequired;
            }
            set
            {
                if (value < 50)
                {
                    throw new Exception("Advance Deposit cannot be less than $50");
                }
                else if (value > 5000)
                {
                    throw new Exception("Advance Deposit cannot be greater than $5000");
                }
                else
                {
                    _AdvanceDepositRequired = value;
                }
            }
        }

    }
}
