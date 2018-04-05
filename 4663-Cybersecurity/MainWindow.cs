//********************************************************************
//      Password Program
//      Name: Chris Duhan
//      Class: 4663 Cyber Security
//      Instructor: Dr. Ranette Halverson
//      Due Date: 04/03/2018
//********************************************************************
//      This program provides a password validator and generator in 
//      one, and provides an easy to use GUI on top of it. The point
//      is to allow users to specify what minimum requirements they 
//      want the passwords to have and they can either have any 
//      number of passwords randomly generated for them or they can
//      enter passwords themselves and when they are done, they'll be 
//      able to see the passwords they entered and if they passed 
//      validation.
//      
//      This file is technically "main()", as it's the first thing
//      that begins executing, I could have made an actual main() file
//      but all it would do would be call this file at that point, 
//      wasting time and resources for the process.
//
//      So I have to admit, this is my first program written in C#...
//      I like it, it's user friendly and understandable, and very, 
//      very powerful. Seems like Java with C++'s tighter style and
//      behavior.
//********************************************************************

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cyber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // What the user wants to use the program for this session
        private int verifierType = 0;

        //********************************************************************
        //		MainWindow::MainWindow()
        //		Parameters: None
        //		Complexity: O(1)
        //		The default constructor, it calls "MainWindow.xaml" to 
        //      create then display the "Welcome" window to the user. 
        //********************************************************************
        public MainWindow()
        {
            InitializeComponent();
        }

        //********************************************************************
        //		MainWindow::Continue_Button_Click(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Changes the state of the verifier information when the 
        //      dictionary words allowed box is checked.
        //********************************************************************
        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            // If user selected Basic Verify option
            if (BasicVerifyRadio.IsChecked == true)
                verifierType = 1;

            // If user selected Generate option
            else if (GenerateRadio.IsChecked == true)
                verifierType = 2;

            // If user pressed the "Continue" button without specifying what they wanted to do
            else
                MessageBox.Show("Please make a selection before proceeding,\nor press the \"Help\" button for more information.");


            VerifierWindow verifyWindow = new VerifierWindow(verifierType);
            verifyWindow.Show();

            // Close the current window
            this.Close();
        }

        //********************************************************************
        //		MainWindow::Help_Button_Click(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: O(1)
        //		Display some basic usage information on button click.
        //********************************************************************
        private void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("If you wish to check a single password of your own design,\nselect the \"Basic Verify\" field.\nIf you wish to generate passwords based on a set of rules,\nselect the \"Generate\" field.");
        }

        //********************************************************************
        //		MainWindow::Generate_Radio_Checked(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the Generate CheckBox
        //      in the "MainWindow.xaml" file. Never used.
        //********************************************************************
        private void Generate_Radio_Checked(object sender, RoutedEventArgs e)
        {
        }

        //********************************************************************
        //		MainWindow::BasicVerify_Radio_Checked(object, TextChangedEventArgs)
        //		Parameters: sender - the reference to the control/object that
        //                  raised the event, e - the event data
        //		Complexity: N/A
        //		A pregenerated funtion, created for the Basic Verify CheckBox
        //      in the "MainWindow.xaml" file. Never used.
        //********************************************************************
        private void BasicVerify_Radio_Checked(object sender, RoutedEventArgs e)
        {
        }
    }
}
