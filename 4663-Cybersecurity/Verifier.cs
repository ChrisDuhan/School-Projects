using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyber
{
    public class Verifier
    {
        // The top level flags that tells if a password has passed verification or not
        bool pass, dictPass = false;

        // Flags for determining if the password has met certain portions of requirements
        bool passUpper, passLower, passNumeric, passSpecial = false;

        // Accumulators for determining if the password has met certain portions of requirements
        int upperCount, lowerCount, numericCount, specialCount = 0;

        // The array that will hold the verifier information that the validity checking will be based on
        int[] verifierInfo = new int[8];

        // The sting that will be read in from the dictionary file
        String line = "";

        // A list to hold the passwords the user has attempted, before being written to the output file
        List<string> dictionary = new List<string>();

        // The object that reads from the dictionary file
        System.IO.StreamReader file = new System.IO.StreamReader(@"c:\Users\Max\source\repos\Cyber\Cyber\Dictionary1.txt");

        //********************************************************************
        //		Verifier::Verifier(int[], string)
        //		Parameters: An array of integers - the information the new
        //      verifier will use, and a string which is the password to be
        //      tested
        //		Complexity: O(n) - where n is the number of words in the 
        //      choosen dictionary.
        //		The default constructor, and it goes on to perform the 
        //      validation immediately. It first sets verifyerInfo equal to 
        //      the passed in array of integers representing the choises the  
        //      user selected for their verifier. Then checks the password for
        //      words in the dictionary, if one is found it stops there. If 
        //      not it continues to ensure that the minimum charactor
        //      requirements have been met.
        //********************************************************************
        public Verifier(int[] i, string password)
        {
            // Save the incoming information locally
            verifierInfo = i;

            // If the length of the password is too short
            if (password.Length < verifierInfo[0])
                return;
            
            // If dictionary words are not allowed
            if (verifierInfo[5] != 1)
                // While there still words in the dictionary file
                while ((line = file.ReadLine()) != null)
                    // If a word in the dictionary appears anywhere in the password
                    if (!(password.IndexOf(line) == -1))
                        return;
            dictPass = true;
            // Inspect each charactor in the password to see which kind it is
            foreach (char c in password)
            {
                // If the charactor is an uppercase letter
                if (char.IsUpper(c))
                    ++upperCount;

                // If the charactor is a lowercase letter
                if (char.IsLower(c))
                    ++lowerCount;

                // If the charactor is a digit
                if (char.IsDigit(c))
                    ++numericCount;

                // If the charactor is a special charactor
                if (!char.IsLetterOrDigit(c))
                    ++specialCount;
            }

            // If the amount of uppercase letters in the password meets the minimum requirement
            if (upperCount >= verifierInfo[1])
                passUpper = true;
            
            // If the amount of lowercase letters in the password meets the minimum requirement
            if (lowerCount >= verifierInfo[2])
                passLower = true;
            
            // If the amount of digits in the password meets the minimum requirement
            if (numericCount >= verifierInfo[3])
                passNumeric = true;
            
            // If the amount of special charactors in the password meets the minimum requirement
            if (specialCount >= verifierInfo[4])
                passSpecial = true;
            
            // If all the previous checks have been succesful
            if (passUpper && passLower && passNumeric && passSpecial)
                pass = true;
        }

        //********************************************************************
        //		Verifier::PassState()
        //		Parameters: None
        //		Complexity: O(1)
        //		Returns the top level "pass", the one saying if a particular
        //      password was valid or not for any reason.
        //********************************************************************
        public bool PassState()
        {
            return pass;
        }

        //********************************************************************
        //		Verifier::DictPassState()
        //		Parameters: None
        //		Complexity: O(1)
        //		Returns the top level "dictPass", the one saying if a 
        //      particular password passed dictionary checking.
        //********************************************************************
        public bool DictPassState()
        {
            return dictPass;
        }
    }
}
