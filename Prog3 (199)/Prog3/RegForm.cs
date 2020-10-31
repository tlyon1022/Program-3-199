// Program 3
// CIS 199-02
// Due: nov 7
// By: K9195

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2020 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void FindRegTimeBtn_Click(object sender, EventArgs e)
        {
            

            const string DAY1 = "November 4"; // 1st day of registration
            const string DAY2 = "November 5"; // 2nd day of registration
            const string DAY3 = "November 6"; // 3rd day of registration
            const string DAY4 = "November 7";  // 4th day of registration
            const string DAY5 = "November 8";  // 5th day of registration
            const string DAY6 = "November 11";  // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            const float SOPHOMORE = 30; // Hours needed to be sophomore
            const float JUNIOR = 60;    // Hours needed to be junior
            const float SENIOR = 90;    // Hours needed to be senior
            
            string lastNameStr;         // Entered last name
            char lastNameLetterCh;      // First letter of last name, as char
            string dateStr = "Error";   // Holds date of registration
            float creditHours;          // Previously earned credit hours
            bool isUpperClass;          // Upperclass or not?
            
            bool found = false;//bool variable to transition out of while loop when proper place in array
            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case
                
                if (float.TryParse(creditHoursTxt.Text, out creditHours) && creditHours >= 0)
                {
                    if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                    {
                        isUpperClass = (creditHours >= JUNIOR);

                        // Juniors and Seniors share same schedule but different days
                        if (isUpperClass)
                          
                        {
                            if (creditHours >= SENIOR)
                                dateStr = DAY1;
                            else // Must be juniors
                                dateStr = DAY2;
                            char[] lowerLetter = { 'A', 'E', 'J', 'P', 'T' };//array for upper classmen letter positions
                            string[] times = { TIME3, TIME4, TIME5, TIME1, TIME2 };//array for upper classmen time positions
                            int index = lowerLetter.Length - 1;
                            while (index >= 0 && !found)//finds a match for lower limits and will decrement until a match is found and bool will change to true
                            {
                                if (lastNameLetterCh >= lowerLetter[index])
                                    found = true;
                                else
                                    --index;
                            }
                            // Output results
                            dateTimeLbl.Text = $"{dateStr} at {times[index]}";
                        }
                        // Sophomores and Freshmen
                        else // Must be soph/fresh
                        
                        {
                            if (creditHours >= SOPHOMORE)
                            {
                                // A-B, P-Z on day one
                                if ((lastNameLetterCh <= 'B') ||  // <= B
                                    (lastNameLetterCh >= 'P'))    // >= P
                                    dateStr = DAY3;
                                else // All other letters on next day
                                    dateStr = DAY4;
                            }
                            else // must be freshman
                            {
                                // A-B, P-Z on day one
                                if ((lastNameLetterCh <= 'B') ||  // <= B
                                    (lastNameLetterCh >= 'P'))    // >= P
                                    dateStr = DAY5;
                                else // All other letters on next day
                                    dateStr = DAY6;
                            }
                           char[] lowerLetter = { 'A', 'C', 'E', 'G', 'J', 'M', 'P', 'R', 'T', 'W' };//array for lower classmen letter positions
                            string[] times = { TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4 };//array for lower classmen time positions
                            int index = lowerLetter.Length - 1;
                            while (index >= 0 && !found)//finds a match for lower limits and will decrement until a match is found and bool will change to true
                            {
                                if (lastNameLetterCh >= lowerLetter[index])
                                    found = true;
                                else
                                    --index;
                            }
                            // Output results
                            dateTimeLbl.Text = $"{dateStr} at {times[index]}";
                        }


                        
                        
                    }
                    else // Not A-Z
                        MessageBox.Show("Make sure last name starts with a letter!");
                }
                else
                    MessageBox.Show("Enter a valid number of credit hours!");
            }
            else // Empty textbox
                MessageBox.Show("Please enter last name!");
        }
    }
}
