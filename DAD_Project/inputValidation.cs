using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TruckRental_Project
{
    internal class inputValidation
    {
        /* Checking if all the text boxes those are required to save a new indidual truck in the system
           at same time using textbox array and checking if any text is empty.*/ 
        public static bool IsTextBoxEmpty(TextBox[] textboxes)
        {
            bool isEmpty = false;
            foreach(var textbox in textboxes)
            {
                if(string.IsNullOrEmpty(textbox.Text))
                {
                    isEmpty = true;
                }
                else
                {
                    isEmpty = false;
                }
            }
            return isEmpty;   
        }


        /* Checking that all the required dates to save a new truck
            in the system are not null.  */
        public static bool IsDatePickerEmpty(DatePicker[] date)
        {
            bool isEmpty = false;
            foreach(var datePicker in date)
            {
                if(datePicker.SelectedDate == null)
                {
                    isEmpty = true;
                }
                else
                {
                    isEmpty = false;
                }
            }
            return isEmpty;
        }


        //Using Method to clear all the textboxes once opertaion has been successfull
        public static void ClearAllTextBoxes(StackPanel stack)
        {
            foreach (Control ctr in stack.Children)
            {
                if (ctr.GetType().Name == "TextBox")
                {
                    TextBox t = (TextBox)ctr;
                    {
                        t.Text = "";

                    }

                }
            }
        }

        //Using Method to clear all the date pickers once opertaion has been successfull
        public static void clearDatePickers(StackPanel stack)
        {
            foreach (Control ctr in stack.Children)
            {

                if (ctr.GetType().Name == "DatePicker")
                {
                    DatePicker d = (DatePicker)ctr;
                    {
                        d.SelectedDate = null;
                    }

                }
            }
        }


    }
}
