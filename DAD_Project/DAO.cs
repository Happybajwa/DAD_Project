using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAD_Project.DB;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;

namespace TruckRental_Project
{
    internal class DAO
    {
    //----------------------------------------------------------------------------------------------------//
        public static bool CheckTruckRegoExists(string registration)
        {
            //CHECKING IF A TRUCK IS ALREADY IN SYSTEM WITH THE SAME REGISTRATION NUMBER
            //IF TRUCK EXISTS FUNCTION WILL RETURN TRUE AND TRUCK WONT BE ADDED TO THE SYSTEM
            //USER WILL BE NOTIFIED
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

    //----------------------------------------------------------------------------------------------------//
        public static void AddNewTruck(IndividualTruck truck)
        {
            //ADDING INDIVIDUAL TRUCK IN DATABASE
            using (DAD_HarpreetContext ctx = new())
            {
                //CHECKING IF TRUCK ALREDY EXISTS IN THE SYSTEM
                if(CheckTruckRegoExists(truck.RegistrationNumber) == false)
                {
                    ctx.IndividualTrucks.Add(truck);
                    ctx.SaveChanges();
                }
                else
                {
                    //THROWING AN EXCEPTION IF TRUCK REGISTRATION ALREADY EXISTS IN SYSTEM
                    throw new Exception("Truck with Same Registration Number Already Exists in the System");
                }
                
            }
        }

    //----------------------------------------------------------------------------------------------------//   
        /* ADDING A NEW TRUCK MODEL IN THE DATABASE. BEFORE ADDING WE ARE CHECKING THAT TRUCK MODEL
           DOESNT EXISTS IN THE SYSTEM ALREADY. IF IT DOES WE WILL SIMPLY SHOW USER AN ERROR THAT THIS
           TRUCK MODEL ALREADY EXISTS IN THE SYSTEM.      
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

     //----------------------------------------------------------------------------------------------------//
        /* CHECKING THAT IF A TRUCK MODEL ALREADY IN THE SYSTEM. WE CAN NOT HAVE TWO TRUCK MODEL
            WITH SAME NAME IN THE SYSTEM.
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

    //----------------------------------------------------------------------------------------------------//
        //SEARCHING THE TRUCK MODEL NUMBER WITH MODEL NAME
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

    //----------------------------------------------------------------------------------------------------//

        //SEARCHING TRUCK BY STATUS
        public static List<IndividualTruck> SearchTruckByStatus(string status)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                var trucks = ctx.IndividualTrucks.Include(tm => tm.TruckModel).Where(m => m.Status == status).ToList();
                return trucks;
            }
        }

    //----------------------------------------------------------------------------------------------------//

        //ADDING A FEATURE TO TRUCK BEFORE ADDING WE ARE CHECKING THAT
        //TRUCK AND FEATURE ARE NOT NULL IN PARAMETERS
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

    //----------------------------------------------------------------------------------------------------//

        //PERFORMING SEARCH TO KNOW IF TRUCK ALREADY HAVE SOME FEATURES OR NOT
        //FOR THAT WE ARE USING TRUCK ID AS WE KNOW TRUCK FEATURE ASSOCIATION ONLY HAVE TRUCK ID AND FEATURE ID
        public static List<TruckFeatureAssociation> GetAllTruckFeaturesByTruckID(int truckId)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                return ctx.TruckFeatureAssociations.Include(feature => feature.Feature).Where(truck => truck.TruckId == truckId).ToList();
            }
        }

     //----------------------------------------------------------------------------------------------------//

        //HERE WE ARE CHECKING THAT IS USER ASSIGNING THE SAME FEATURE TO TRUCK THAT TRUCK ALREADY HAS
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

    //----------------------------------------------------------------------------------------------------//
        //WE ARE CHECKING IF THE FEATURE WE ARE TRYING TO ADD IN SYSTEM -
        //ALREADY EXISTS IN THE SYSTEM
        public static bool IsTruckFeatureExistsInSystem(string feature)
        {       
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

    //----------------------------------------------------------------------------------------------------//

        //HERE WE ARE SEARCHING THAT EXISTING TRUCK USING ITS REGISTRATION NUMBER
        public static IndividualTruck getTruckByRegistrationNumber(string rego)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                return ctx.IndividualTrucks.Include(t => t.TruckModel).Where(t => t.RegistrationNumber == rego).FirstOrDefault();
            }
        }

    //----------------------------------------------------------------------------------------------------//

        //WE ARE RETURING A LIST OF ALL AVAILABLE TRUCK FEATURES IN THE SYSTEM
        public static List<TruckFeature> getAllTruckFeatures()
        {
            using (DAD_HarpreetContext ctx = new())
            {
                return ctx.TruckFeatures.ToList();
            }
        }

    //----------------------------------------------------------------------------------------------------//

        //SEARCHING TRUCK AND FEATURE ASSOCIATION USING TRUCK ID
        //IT WILL RETURN US ALL FEATURES LINKED TO THE GIVEN TRUCK ID
        public static List<TruckFeatureAssociation> searchTruckFeatureAssociationByTruckID(int id)
        {
            using(DAD_HarpreetContext ctx = new())
            {
                return ctx.TruckFeatureAssociations.Include(feature => feature.Feature).Where(t => t.TruckId == id).ToList();
            }
        }

    //----------------------------------------------------------------------------------------------------//

        //THIS METHOD WILL REMOVE TRUCK'S EXISTING FEATURE
        public static void RemoveTruckFeature(TruckFeatureAssociation feature)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                ctx.TruckFeatureAssociations.Remove(feature);
                ctx.SaveChanges();
            }
        }

    //----------------------------------------------------------------------------------------------------//

        //Validating truck daily rent and advance deposit for 
        //updating purposes
        public static bool isRentalPriceOkay(decimal rent, decimal advance)
        {
            if(rent < 50 || advance < 50)
            {
                return false;
            }
            else if(rent > 1500 || advance > 5000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    //----------------------------------------------------------------------------------------------------//

        //UPDATING TRUCK  INFORMATION
        public static void updateSelectedTruck(IndividualTruck truck)
        {
            Regex isString = new Regex(@"^[a-zA-Z]+");
            try
            {
                if (isString.IsMatch(truck.Colour) && isRentalPriceOkay(truck.DailyRentalPrice, truck.AdvanceDepositRequired))
                {
                    using (DAD_HarpreetContext ctx = new())
                    {
                        ctx.Entry(truck).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("OOPS Something went wrong.\n"
                        + "Note:1. Daily rent and advance rent cannot be less than $50\n"
                        + "2. Daily Rent cannot be greater then $1500\n3. advance rent greater than $5000 ");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

    //----------------------------------------------------------------------------------------------------//

        //GETTING TRUCK EMPLOYEE WITH USERNAME AND PASSWORD
        //FOR LOGIN PAGE
        public static string isLoginDetailsAreCorrect(string username, string password)
        {
            using (DAD_HarpreetContext ctx = new())
            {
                var employee = ctx.TruckEmployees.Where(emp => emp.Username == username && emp.Password == password).FirstOrDefault();
                if (employee != null)
                {
                    return employee.Role;
                }
                else
                {
                    throw new Exception("Employee not found with given username and password");
                }
            }
        }

    //----------------------------------------------------------------------------------------------------//

        //LIST OF ALL TO FOR DASHBOARD VIEW
        public static List<IndividualTruck> getAllTruck()
        {
            using (DAD_HarpreetContext ctx = new())
            {
                return ctx.IndividualTrucks.ToList();
            }
        }

    }
}
