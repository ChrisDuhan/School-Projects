//********************************************************************
//	StringAlphabetizer
//	Name: Chris Duhan
//      Class: Cybersecurity: Principles & Management
//      Instructor: Dr. Halverson
//	Due Date: N/A
//********************************************************************
//	This program was created as a tool to both alphbetize large
//	text files of words and to remove small words from the file
//	as it is read in. This is a support tool for a larger project,
//	where it will be more efficient to have a dictionary of 
//	alphabetized words to save time during program execution.
//********************************************************************


#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

//********************************************************************
//	main()
//	Parameters: None
//	Complexity: O(n log n)
//	Reads in words from the input file, expecting that there will
//	be only one word on each line. Once all words have been read 
//	in from the file it is closed and the length of each word is 
//	examined, if the length is less than 5 it is ignored but 
//	otherwise the word is pushed to a vector. That vector is then
//	sorted with the STL sort() and then printed to an output file.
//********************************************************************
int main(){
	vector <string> readIn;
	vector <string> printOut;
	ifstream infile;
	ofstream outfile;
	string in, out;

	infile.open("dictwords.txt", ios_base::in);
	outfile.open("sorteddict.txt", ios_base::out);

	if (!infile) {
		cout << "Error opening file to be read.\n";
		return 1;
	}

	if (!outfile) {
		cout << "Error opening file to be written.\n";
		return 1;
	}
	
	// Complexity: O(n)
	while (getline(infile, in))
		readIn.push_back(in);
	infile.close();
	
	// Complexity: O(n)
	for (auto i : readIn)
		if (i.length() >= 5)
			printOut.push_back(i);
	
	// Complexity: O(n log n)
	sort(printOut.begin(), printOut.end());
	
	// Complexity: O(n)
	for (auto i : printOut)
		outfile << i << endl;
	outfile.close();
	
	return 0;
}
