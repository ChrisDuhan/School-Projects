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
    /// Interaction logic for VerifierWindow.xaml
    /// </summary>
    public partial class VerifierWindow : Window
    {
        // What the user wants to use the program for this session
        int verifierType = 0;

        // The array that will hold the verifier information that the validity checking will be based on
        int[] verifier = new int[8];

        //********************************************************************
        //		VerifierWindow::VerifierWindow(int)
        //		Parameters: An integer - the code for what the user want to do
        //                  this session
        //		Complexity: O(1)
        //		The default constructor, it calls "VerifierWindow.xaml" to 
        //      create then display the "Password Checking Assistant" window  
        //      to the user. It then sets verifyerType equal to the passed in 
        //      integer representing the choise the user made for this session
        //********************************************************************
        public VerifierWindow(int i)
        {
            // Calling "VerifierWindow.xaml"
            InitializeComponent();

            // Save the incoming information locally
            verifierType = i;
        }

        //********************************************************************
        //		VerifierWindow::MinLength_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Sanitizes input in the MinLength field, allowing only digits
        //      to be entered.
        //********************************************************************
        private void MinLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the text in the MinLength TextBox is anything other than a digit
            if (System.Text.RegularExpressions.Regex.IsMatch(MinLength.Text, "[^0-9]"))
                // remove the offending charactor
                MinLength.Text = MinLength.Text.Remove(MinLength.Text.Length - 1);
        }

        //********************************************************************
        //		VerifierWindow::MinUpChar_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Sanitizes input in the MinUpper field, allowing only digits
        //      to be entered.
        //********************************************************************
        private void MinUpChar_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the text in the MinLength TextBox is anything other than a digit
            if (System.Text.RegularExpressions.Regex.IsMatch(MinUpper.Text, "[^0-9]"))
                // remove the offending charactor
                MinUpper.Text = MinUpper.Text.Remove(MinUpper.Text.Length - 1);
        }

        //********************************************************************
        //		VerifierWindow::MinLowChar_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Sanitizes input in the MinLower field, allowing only digits
        //      to be entered.
        //********************************************************************
        private void MinLowChar_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the text in the MinLength TextBox is anything other than a digit
            if (System.Text.RegularExpressions.Regex.IsMatch(MinLower.Text, "[^0-9]"))
                // remove the offending charactor
                MinLower.Text = MinLower.Text.Remove(MinLower.Text.Length - 1);
        }

        //********************************************************************
        //		VerifierWindow::MinNumeric_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Sanitizes input in the MinNumeric field, allowing only digits
        //      to be entered.
        //********************************************************************
        private void MinNumeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the text in the MinLength TextBox is anything other than a digit
            if (System.Text.RegularExpressions.Regex.IsMatch(MinNumeric.Text, "[^0-9]"))
                // remove the offending charactor
                MinNumeric.Text = MinNumeric.Text.Remove(MinNumeric.Text.Length - 1);
        }

        //********************************************************************
        //		VerifierWindow::MinSpecial_TextChanged(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Sanitizes input in the MinSpecial field, allowing only digits
        //      to be entered.
        //********************************************************************
        private void MinSpecial_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If the text in the MinLength TextBox is anything other than a digit
            if (System.Text.RegularExpressions.Regex.IsMatch(MinSpecial.Text, "[^0-9]"))
                // remove the offending charactor
                MinSpecial.Text = MinSpecial.Text.Remove(MinSpecial.Text.Length - 1);
        }

        //********************************************************************
        //		VerifierWindow::DictWords_Checked(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Changes the state of the verifier information when the 
        //      dictionary words allowed box is checked.
        //********************************************************************
        private void DictWords_Checked(object sender, RoutedEventArgs e)
        {
            verifier[5] = 1;
        }

        //********************************************************************
        //		VerifierWindow::Username_Checked(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Changes the state of the verifier information when the user's
        //      name allowed box is checked.
        //********************************************************************
        private void Username_Checked(object sender, RoutedEventArgs e)
        {
            verifier[6] = 1;
        }

        //********************************************************************
        //		VerifierWindow::Email_Checked(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Changes the state of the verifier information when the email
        //      address allowed box is checked.
        //********************************************************************
        private void Email_Checked(object sender, RoutedEventArgs e)
        {
            verifier[7] = 1;
        }

        //********************************************************************
        //		VerifierWindow::Continue_Button_Click(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Aquires all inputs into the verifier array and depending on
        //      what the user wanted to do, calles the appropriate window.
        //********************************************************************
        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the information from the fields
            verifier[0] = int.Parse(MinLength.Text);
            verifier[1] = int.Parse(MinUpper.Text);
            verifier[2] = int.Parse(MinLower.Text);
            verifier[3] = int.Parse(MinNumeric.Text);
            verifier[4] = int.Parse(MinSpecial.Text);

            // If user chose "Basic Verify"
            if (verifierType == 1)
            {
                UserInfoWindow userInfoWindow = new UserInfoWindow(verifier);
                userInfoWindow.Show();
                this.Close();
            }

            // If user chose "Generate"
            else if (verifierType == 2)
            {
                GenerateWindow generateWindow = new GenerateWindow(verifier);
                generateWindow.Show();
                this.Close();
            }

            // Should never see this...
            else 
            {
                MessageBox.Show("There has been an error");
                // Close the current window
                this.Close();
            }      
        }   
    }
}
