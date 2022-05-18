using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAD_Project.DB;
using System.Windows.Controls;
using System.Windows;

namespace TruckRental_Project
{
    internal class DAO
    {
        public static bool CheckTruckRegoExists(string registration)
        {
            //Checking if a truck is already in system with the same registration number
            //if truck exists function will return true and truck wont be added to the system
            //User will be notified
            using (DAD_HarpreetContext ctx = new())
            {
                var regoExists = ctx.IndividualTrucks.Where(t => t.RegistrationNumber == registration).FirstOrDefault();
                if(regoExists != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }       
            }
        }
        public static void AddNewTruck(IndividualTruck truck)
        {
            //Adding Individual truck in database
            using (DAD_HarpreetContext ctx = new())
            {
                //Chexking if truck alredy exists in the system
                if(CheckTruckRegoExists(truck.RegistrationNumber) == false)
                {
                    ctx.IndividualTrucks.Add(truck);
                    ctx.SaveChanges();
                }
                else
                {
                    //throwing an exception if truck registration already exists in system
                    throw new Exception("Truck with Same Registration Number Already Exists in the System");
                }
                
            }
        }
        
        /* Adding a new truck model in the database. before adding we are checking that truck model
           doesnt exists in the system already. if it does we will simply show user an error that this
           truck model already exists in the system.      
         */
        public static void AddNewTruckModel(TruckModel model)
        {
            using (DAD_HarpreetContext ctx = new())
            {
               if(IsTruckModelExists(model.Model) == true)
                {
                    throw new Exception("This Truck model already exists in this system.");
                }
               else
                {
                    ctx.TruckModels.Add(model);
                    ctx.SaveChanges();
                }
            }
        }

        /* Checking that if a truck model already in the system. we can not have two truck model
            with same name in the system.
         */
        public static bool IsTruckModelExists(string name)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                var truckModel = ctx.TruckModels.Where(m => m.Model.Equals(name)).FirstOrDefault();
                if(truckModel != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
        }

        //Searching the truck model number with model name
        public static TruckModel searchTruckModelName(string name)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                var TruckModel = ctx.TruckModels.Include(truck => truck.IndividualTrucks).Where(model => model.Model == name).FirstOrDefault();
                if(TruckModel != null)
                {
                    return TruckModel;
                }
                else
                {
                    throw new Exception("Truck Model doesnt exits in the system with the given model name.");
                }
                    
            }
        }

        public static List<IndividualTruck> SearchTruckByStatus(string status)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                var trucks = ctx.IndividualTrucks.Include(tm => tm.TruckModel).Where(m => m.Status == status).ToList();
                return trucks;
            }
        }  


        //adding a feature to truck before adding we are checking that
        //truck and feature are not null in parameters
        public static void AddFeatureToTruck(IndividualTruck truck, TruckFeature feature)
        {
            if (truck != null || feature != null)
                using (DAD_HarpreetContext ctx = new())
                {
                    TruckFeatureAssociation association = new TruckFeatureAssociation();
                    association.TruckId = truck.TruckId;
                    association.FeatureId = feature.FeatureId;
                    ctx.TruckFeatureAssociations.Add(association);
                    ctx.SaveChanges();
                }
            else
            {
                throw new Exception("Please make sure that you have searched a existing  truck and selected a feature to add");
            }
        }


        //performing search to know if truck already have some features or not
        //for that we are using truck id as we know truck feature association only have truck id and feature id
        public static List<TruckFeatureAssociation> DoesTruckHasFeatureExistedAlready(int truckId)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                return ctx.TruckFeatureAssociations.Include(feature => feature.Feature).Where(truck => truck.TruckId == truckId).ToList();
            }
        }

        //Here we are checking that is user assigning the same feature to truck that truck already has
        public static bool DoesTruckHasThisFeatureAlready(int truckId, int featureId)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                var truck = ctx.TruckFeatureAssociations.Where(p => p.TruckId == truckId && p.FeatureId == featureId).FirstOrDefault();
                if(truck == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public static bool IsTruckFeatureExists(string feature)
        {
            //We are checking if the feature we are trying to add in system -
            //already exists in the system
            using (DAD_HarpreetContext ctx = new())
            {
                var ExistingFeature = ctx.TruckFeatures.Where(f => f.Description.ToLower().Equals(feature.ToLower())).FirstOrDefault();
                if(ExistingFeature == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        //here we are searching that existing truck using its registration number
        public static IndividualTruck getTruckByRegistrationNumber(string rego)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                return ctx.IndividualTrucks.Include(t => t.TruckModel).Where(t => t.RegistrationNumber == rego).FirstOrDefault();
            }
        }

        //We are returing a list of all available truck features in the system
        public static List<TruckFeature> getAllTruckFeatures()
        {
            using (DAD_HarpreetContext ctx = new())
            {
                return ctx.TruckFeatures.ToList();
            }
        }

        //searching truck and feature association using truck id
        //it wil return us all features linked to the given truck id
        public static List<TruckFeatureAssociation> searchTruckFeatureAssociationByTruckID(int id)
        {
            using(DAD_HarpreetContext ctx = new())
            {
                return ctx.TruckFeatureAssociations.Include(feature => feature.Feature).Where(t => t.TruckId == id).ToList();
            }
        }

        public static void RemoveTruckFeature(TruckFeatureAssociation feature)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                ctx.TruckFeatureAssociations.Remove(feature);
                ctx.SaveChanges();
            }
        }
    }
}
