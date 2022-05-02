using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAD_Project.DB;

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
                if (ctx.IndividualTrucks.Any(truck => truck.RegistrationNumber == registration))
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
                var TruckModel = ctx.TruckModels.Where(model => model.Model == name).FirstOrDefault();
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
    }
}
