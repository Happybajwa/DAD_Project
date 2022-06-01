using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TruckRental_Project
{
    internal class Utility
    {
        //CHECKING THAT TEXTBOXES AND DATEPICKERS ARE NOT WMPTY WHILE ADDING A NEW TRUCK IN THE SYSTEM
        //IS ANY IS EMPTY ERROR WILL BE DIPLAYED ALONG WITG THE RED BORDER ON INPUT BOX
        public static string isTextBoxAndDatePickerAreEmpty(Panel panel)
        {
            string output = "";
            foreach (Control ctr in panel.Children)
            { 
                if (ctr.GetType().Name == "TextBox")
                {
                    TextBox textbox = (TextBox)ctr;
                    if (string.IsNullOrEmpty(textbox.Text))
                    {
                        output += textbox.Uid +"\n";
                        textbox.BorderBrush = Brushes.Red;
                    }    
                    else
                    {
                        textbox.BorderBrush = Brushes.Green;
                    }
                }
                else if (ctr.GetType().Name == "DatePicker")
                {
                    DatePicker datePicker = (DatePicker)ctr;
                    if (datePicker.SelectedDate == null)
                    {
                        output += datePicker.Uid + "\n";
                        datePicker.BorderBrush = Brushes.Red;
                    }
                    else
                    {
                        datePicker.BorderBrush = Brushes.Green;
                    }
                }
            }
            return output;
        }   

        //USING METHOD TO CLEAR ALL THE INPUTS ONCE OPERATION HAS BEEN SUCCESSFUL
        public static void clearAllInputs(StackPanel stack)
        {
            foreach (Control ctr in stack.Children)
            {
                if (ctr.GetType().Name == "DatePicker")
                {
                    DatePicker datePicker = (DatePicker)ctr;
                    {
                        datePicker.SelectedDate = null;
                        datePicker.BorderBrush = Brushes.CornflowerBlue;
                    }
                }
                else if (ctr.GetType().Name == "TextBox")
                {
                    TextBox textBox = (TextBox)ctr;
                    {
                        textBox.Text = "";
                        textBox.BorderBrush = Brushes.CornflowerBlue;
                    }
                }
            }
        }
    }
}
