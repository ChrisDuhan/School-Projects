using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cyber
{
    /// <summary>
    /// Interaction logic for UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        // The first name of the user, in case they want to not allow their name in the passwords
        string firstName = "";

        // The last name of the user, ^^
        public string lastName = "";

        // The email address of the user, so it can be disallowed in the passwords
        string emailAddress = "";

        // The password that the user will be checking
        string password = "";

        // The array that will hold the verifier information that the validity checking will be based on
        int[] verifierInfo = new int[8];

        // A list to hold the passwords the user has attempted, before being written to the output file
        List<string> attemptedPasswords = new List<string>();

        //********************************************************************
        //		UserInfoWindow::UserInfoWindow(int[])
        //		Parameters: An array of integers
        //		Complexity: O(1)
        //		The default constructor, it calls "UserInfoWindow.xaml" to 
        //      create then display the "User Information" window to the 
        //      user. It then sets verifyerInfo equal to the passed in array
        //      of integers representing the choises the user selected for 
        //      their verifier.
        //********************************************************************
        public UserInfoWindow(int[] i)
        {
            // Calling "UserInfoWindow.xaml"
            InitializeComponent();

            // Save the incoming information locally
            verifierInfo = i;
        }

        //********************************************************************
        //		UserInfoWindow::FirstNameBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the first name TextBox 
        //      field in the "UserInfoWindow.xaml" file. Never used.
        //********************************************************************
        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string firstName = FirstName.Text;
        }

        //********************************************************************
        //		UserInfoWindow::LastNameBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the last name TextBox 
        //      field in the "UserInfoWindow.xaml" file. Never used.
        //********************************************************************
        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string lastName = LastName.Text;
        }

        //********************************************************************
        //		UserInfoWindow::EmailBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the email TextBox field
        //      in the "UserInfoWindow.xaml" file. Never used.
        //********************************************************************
        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string emailAddress = EmailAddress.Text;
        }

        //********************************************************************
        //		UserInfoWindow::PasswordBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the password TextBox field
        //      in the "UserInfoWindow.xaml" file. Never used.
        //********************************************************************
        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string password = Password.Text;
        }

        //********************************************************************
        //		UserInfoWindow::Submit_Button_Click(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: 0(n^m) - where n is the number of passwords the 
        //                  user will attempt, and m is the number of words
        //                  in the dictionary file.
        //		Performs the check of each password the user enters.
        //********************************************************************
        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the information from the TextBoxes
            firstName = FirstName.Text;
            lastName = LastName.Text;
            emailAddress = EmailAddress.Text;
            password = Password.Text;

            // A Verifier object, taking the current verifier information and the password to be verified
            Verifier passwordChecker = new Verifier(verifierInfo, password);

            // If dictionary words are not allowed and the verifier failed
            if (verifierInfo[5] != 1 && !passwordChecker.DictPassState())
            {
                MessageBox.Show("The password you attempted\nfails to meet the \npreviously specified requirements.");
                attemptedPasswords.Add(password + " : Invalid - attempted password contains a dictionary word");
            }
            else
            {
                // If the user's name is not allowed and the user's first or last name appear anywhere in the password
                if (verifierInfo[6] != 1 && (!(password.IndexOf(firstName) == -1) || !(password.IndexOf(lastName) == -1)))
                {
                    MessageBox.Show("The password you attempted\nfails to meet the \npreviously specified requirements.");
                    attemptedPasswords.Add(password + " : Invalid - attempted password contains your name");
                }
                else
                {
                    // String for the non-domain portion of the user's email
                    string mailBoxName = "";

                    // finding the location of the "@" in the user's email address
                    int index = emailAddress.IndexOf("@");

                    if (index > 0)
                        // Set "mailBox" to the non-domain portion
                        mailBoxName = emailAddress.Substring(0, index - 1);

                    // If the user's email address is not allowed and appears anywhere in the password
                    if (verifierInfo[7] != 1 && (!(password.IndexOf(emailAddress) == -1) || !(password.IndexOf(mailBoxName) == -1)))
                    {
                        MessageBox.Show("The password you attempted\nfails to meet the \npreviously specified requirements.");
                        attemptedPasswords.Add(password + " : Invalid - attempted password contains your email");
                    }
                    else
                    {
                        // If the password passed the verifier
                        if (passwordChecker.PassState())
                        {
                            MessageBox.Show("You have entered a valid Password");
                            attemptedPasswords.Add(password + " : Valid");
                        }
                        else
                        {
                            MessageBox.Show("The password you attempted\nfails to meet the \npreviously specified requirements.");
                            attemptedPasswords.Add(password + " : Invalid - attempted password failed to meet at least one minimum criteria");
                        }
                    }
                }
            }
        }

        //********************************************************************
        //		UserInfoWindow::Finished_Button_Click(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(n) - where n is the number of passwords the 
        //                  user attampted
        //		This method wraps up shop when the user has finished inputing
        //      passwords. It gets the user's last name so it can create an
        //      output file, then write all the passwords and extra 
        //      information to that file, then closes the program.
        //********************************************************************
        private void Finished_Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the information to give some information in the output file
            firstName = FirstName.Text;
            lastName = LastName.Text;
            emailAddress = EmailAddress.Text;

            // The object that both creates and then writes to the output file
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Max\Desktop\" + lastName + "_Tested_Passwords.txt");

            // Provide some useful information in the outfile
            file.WriteLine("These passwords were tested using the following information:   Name: " + firstName + " " + lastName + "   " + "Email address: " + emailAddress);
            file.WriteLine();

            // Writing all passwords at the same time
            using (file)
                foreach (string line in attemptedPasswords)
                {
                    file.WriteLine(line);
                    file.WriteLine();
                }

            // Close the current window
            this.Close();
        }
    }
}
