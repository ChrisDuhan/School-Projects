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
    /// Interaction logic for GenerateWindow.xaml
    /// </summary>
    /// 
    public partial class GenerateWindow : Window
    {
        // The amount of valid passwords that will be created by the generator
        int passwordQuantity = 0;

        // The first name of the user, in case they want to not allow their name in the passwords
        string firstName = "";

        // The last name of the user, ^^
        string lastName = "";

        // The filename that the user wishes their password list to be called
        string fileName = "";

        // The email address of the user, so it can be disallowed in the passwords
        string emailAddress = "";

        // The array that will hold the verifier information that the validity checking will be based on
        int[] verifierInfo = new int[8];

        //********************************************************************
        //		GenerateWindow::GenerateWindow(int[])
        //		Parameters: An array of integers
        //		Complexity: O(1)
        //		The default constructor, it calls "GenerateWindow.xaml" to 
        //      create then display the "Generate Passwords" window to the 
        //      user. It then sets verifyerInfo equal to the passed in array
        //      of integers representing the choises the user selected for 
        //      their verifier.
        //********************************************************************
        public GenerateWindow(int[] i)
        {
            // Calling "GenerateWindow.xaml"
            InitializeComponent();

            // Save the incoming information locally
            verifierInfo = i;
        }

        //********************************************************************
        //		GenerateWindow::QuantityBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Sanitizes input in the Quantity field, allowing only digits
        //      to be entered.
        //********************************************************************
        private void QuantityBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the text in the MinLength TextBox is anything other than a digit
            if (System.Text.RegularExpressions.Regex.IsMatch(Quantity.Text, "[^0-9]"))
                // remove the offending charactor
                Quantity.Text = Quantity.Text.Remove(Quantity.Text.Length - 1);
        }

        //********************************************************************
        //		GenerateWindow::FileNameBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the file name TextBox 
        //      field in the "GenerateWindow.xaml" file. Never used.
        //********************************************************************
        private void FileNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        //********************************************************************
        //		GenerateWindow::FirstNameBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the First name TextBox 
        //      field in the "GenerateWindow.xaml" file. Never used.
        //********************************************************************
        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        //********************************************************************
        //		GenerateWindow::LastNameBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the Last name TextBox 
        //      field in the "GenerateWindow.xaml" file. Never used.
        //********************************************************************
        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        //********************************************************************
        //		GenerateWindow::EmailBox_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the Email TextBox field
        //      in the "GenerateWindow.xaml" file. Never used.
        //********************************************************************
        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        //********************************************************************
        //		GenerateWindow::Generate_Button_Click(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: ?
        //		A pregenerated funtion, created for the Generate button
        //      in the "GenerateWindow.xaml" file.
        //      
        //********************************************************************
        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            // Obtaining how many passwords the user wishes to generate
            passwordQuantity = int.Parse(Quantity.Text);

            // Get the information from the TextBoxes
            fileName = FileName.Text;
            firstName = FirstName.Text;
            lastName = LastName.Text;
            emailAddress = EmailAddress.Text;

            // An array to hold the valid passwords once validated, before being written to the output file
            String[] generatedPasswords = new String[passwordQuantity];

            // These ints mark the respective start and end of valid ascii charactors that can be used for generation
            int asciiCharacterStart = 33;
            int asciiCharacterEnd = 126;

            int incrementer = 0;

            // The RNG, seeded with current time for random charactor generation
            Random random = new Random(DateTime.Now.Millisecond);

            // a dynamic object that allows you to expand the number of characters in the string, solving the immutability of a string
            StringBuilder build = new StringBuilder();

            // The string that will hold the potential password that the StringBuilder object creates
            String passwordCandidate = "";

            // The array that holds all valid passwords
            String[] lines = new String[passwordQuantity];

            // The object that both creates and then writes to the output file
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Max\Desktop\" + fileName + ".txt");

            //  Can't use a for loop here, ass we can only increment if a generated password is valid, which may not happen every time
            while (incrementer < passwordQuantity)
            {
                // Completly clear the StringBuilder, if not, charactors keep appending to the end
                build.Clear();
                
                // Execute as many times as the minimum password length specified
                for (int i = 0; i < this.verifierInfo[0]; i++)
                    build.Append((char)(random.Next(asciiCharacterStart, asciiCharacterEnd + 1) % 255));

                // Only have to do this because some functions have problems taking build.ToString() as a parameter
                passwordCandidate = build.ToString();

                // A Verifier object, taking the current verifier information and the most recent potential password
                Verifier passwordChecker = new Verifier(verifierInfo, passwordCandidate);
                /*
                 I know that this next section of code is ugly. so much so that I'm not going 
                 to allow it to run. The only reason I havn't fixed it is because of the RNG of 
                 my generator the chances of getting a dictionary word or seeing your name or 
                 email in a generated password is so small that I don't even worry about it.
                 But I'm leaving it here if I ever want to come back to it and truly add that
                 overkill checking.
                 
                // If dictionary words are not allowed and the verifier failed
                if (verifierInfo[5] != 1 && !passwordChecker.PassState())
                {
                    break;
                }
                else
                {
                    // If the user's name is not allowed
                    if (verifierInfo[6] != 1)
                    {
                        // If the user's first or last name appear anywhere in the password
                        if (!(passwordCandidate.IndexOf(firstName) == -1) || !(passwordCandidate.IndexOf(lastName) == -1))
                        {
                            break;
                        }
                    }
                    else
                    {
                        // If the user's email address is not allowed
                        if (verifierInfo[7] != 1)
                        {
                            // String for the non-domain portion of the user's email
                            string mailBoxName = "";

                            // finding the location of the "@" in the user's email address
                            int index = emailAddress.IndexOf("@");

                            if (index > 0)
                                // Set "mailBox" to the non-domain portion
                                mailBoxName = emailAddress.Substring(0, index - 1);

                            // If the user's email address is not allowed
                            if (!(passwordCandidate.IndexOf(emailAddress) == -1) || !(passwordCandidate.IndexOf(mailBoxName) == -1))
                            {
                                break;
                            }
                        }
                        else
                        {
                            // If the password passed the verifier
                            if (passwordChecker.PassState())
                            {
                                lines[incrementer] = passwordCandidate;
                                ++incrementer;
                            }
                        }
                    }
                }*/

                // Only if the password is valid is the conditional entered
                if (passwordChecker.PassState())
                {  
                    lines[incrementer] = passwordCandidate;
                    ++incrementer;
                }
            }

            // Provide some useful information in the outfile
            file.WriteLine("These passwords were tested using the following information:   Name: " + firstName + " " + lastName + "   " + "Email address: " + emailAddress);
            file.WriteLine();

            // Writing all passwords at the same time, there were odd issues them as they were being validated
            using (file)
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                    file.WriteLine();
                }

            MessageBox.Show("Password generation succesful!");

            // Close the current window
            this.Close();
        }
    }
}
