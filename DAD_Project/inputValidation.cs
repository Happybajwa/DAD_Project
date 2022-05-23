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
        /* CHECKING IF ALL THE TEXT BOXES THOSE ARE REQUIRED TO SAVE A NEW INDIDUAL TRUCK IN THE SYSTEM
           AT SAME TIME USING TEXTBOX ARRAY AND CHECKING IF ANY TEXT IS EMPTY.*/ 
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


        /* CHECKING THAT ALL THE REQUIRED DATES TO SAVE A NEW TRUCK
            IN THE SYSTEM ARE NOT NULL.  */
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

        //USING METHOD TO CLEAR ALL THE INPUTS ONCE OPERATION HAS BEEN SUCCESSFUL
        public static void clearAllInputs(StackPanel stack)
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
                else if (ctr.GetType().Name == "TextBox")
                {
                    TextBox t = (TextBox)ctr;
                    {
                        t.Text = "";
                    }
                }
            }
        }
    }
}
