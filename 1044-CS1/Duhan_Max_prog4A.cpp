// Chris Duhan
// CS 1044 - Wuthrich
// Assignment 4A
// 11/9/2015
// The purpose of this program is to learn to use functions to perform tasks.
// It wiil open an input file, open an input and output file and read the
// input file. It will then process the values read in to determine whether
// they are integers or words. It checks the integers to determine if they are
// prime or not, and converts the words into the sum of their ASCII code
// values. It writes all this information to an output file. 

#include <iostream>
#include <fstream>
#include <string>
#include <cstdlib> // must be included for the .c_str() later

using namespace std;

void printBorder(char c, string name, ofstream &outfile);

bool isPrime(int);

int conversion(string);

int main()
{
	ifstream infile("prog4A.dat");
	ofstream outfile("Duhan_Max_prog4A.txt");

	string input;
	int number = 0;

	printBorder('*', "Max", outfile);

	while (infile >> input)
	{
		if (atoi(input.c_str()) >= 2) // tells if it is a number
		{
			number = atoi(input.c_str());
			if (isPrime(number))
				outfile << number << " is a prime number." << endl;
			else
				outfile << number << " is not a prime number." << endl;
		}
		else // its a string
		{
			outfile << conversion(input) << endl;
		}
	}

	printBorder('#', "Duhan", outfile);

	return 0;
}

// Function printBorder
// Input: accepts a charactor a string and the ofstream object
// Process: prints a name surrounded by the choosen charactor to the 
// ofstream object
// Output: none
void printBorder(char c, string name, ofstream &outfile)
{
	outfile << string(25, c) << name << string(25, c) << endl;
}

// Function isPrime
// Input: accepts one integer value
// Process: determines if the integer is a prime number
// Output: returns a boolian value
bool isPrime(int num)
{
	bool ans = 0;
	for (int i = 2; i <= num / 2; i++)
		if (num % i == 0)
			return 0;
	return 1;
}

// Function conversion
// Input: a string value
// Process: changes a word to its accumulated ASCII code values
// Output: returns one integer
int conversion(string input)
{
	int sum = 0;
	for (int i = 0; i <= (input.length() - 1); i++)
		sum += (int)input[i];
    return sum;
}