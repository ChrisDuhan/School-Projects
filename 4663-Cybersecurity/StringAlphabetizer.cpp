#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

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
		
	while (getline(infile, in))
		readIn.push_back(in);

	for (auto i : readIn)
		if (i.length() >= 5)
			printOut.push_back(i);

	sort(printOut.begin(), printOut.end());
	
	for (auto i : printOut)
		outfile << i << endl;

	return 0;
}
