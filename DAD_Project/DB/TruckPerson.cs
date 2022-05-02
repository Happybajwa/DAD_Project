﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DAD_Project.DB
{
    public partial class TruckPerson
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }

        public virtual TruckCustomer TruckCustomer { get; set; }
        public virtual TruckEmployee TruckEmployee { get; set; }
    }
}
