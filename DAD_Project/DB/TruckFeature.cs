using System;
using System.Collections.Generic;

#nullable disable

namespace DAD_Project.DB
{
    public partial class TruckFeature
    {
        public TruckFeature()
        {
            TruckFeatureAssociations = new HashSet<TruckFeatureAssociation>();
        }

        public int FeatureId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TruckFeatureAssociation> TruckFeatureAssociations { get; set; }
    }
}
