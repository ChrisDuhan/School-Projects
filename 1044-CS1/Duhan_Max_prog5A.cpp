// Chris Duhan
// CS 1044 - Wuthrich
// Assignment 5A
// 12/1/2015
// The purpose of this program is to take information about the 32 professional
// football teams in America, store it in an array of structs and then 
// perform various calculations and references to the information.

#include <iostream>
#include <iomanip>
#include <fstream>
#include <string>

using namespace std;

// Creates the structure to hold the information of each team.
struct Team
{
	string name, conf, divs;
	int wins, loss, pntsScored, pntsAllowed, touchdowns;
	double winPCNT;
};
int main()
{
	ifstream infile("nfl.dat");
	ofstream outfile("Duhan_Max_nfl.txt");

	outfile << "Max Duhan\n" << endl;

	Team teams[32]; // Creating an array to hold a struct of each teams info.
	double largestWinPCNT = 0;
	int  num = 0, leastTD = 0, mostPNTS = 0, totalWins = 0, totalLoss = 0;

	// Information is read to each variable in the array of structs except
	// for each teams win percentage. The in file is then closed.
	for (int i = 0; i < 32; i++)
	{
		getline(infile, teams[i].name);
		infile >> teams[i].conf >> teams[i].divs >> teams[i].wins;
		infile >> teams[i].loss >> teams[i].pntsScored;
		infile >> teams[i].pntsAllowed >> teams[i].touchdowns;
		infile.ignore();
	}
	infile.close();

	// Now that the infile has been closed the information will be used.
	// The final variable value will also be calculated.
	mostPNTS = teams[0].pntsScored;
	leastTD = teams[0].touchdowns;
	for (int i = 0; i < 32; i++)
	{
		// One of the int values will be cast as a double to 
		// get a percentage value.
		teams[i].winPCNT = (double(teams[i].wins) /
			(teams[i].wins + teams[i].loss)) * 100;
		if (teams[i].winPCNT > largestWinPCNT)
			largestWinPCNT = teams[i].winPCNT;
		if (teams[i].touchdowns < leastTD)
			leastTD = teams[i].touchdowns;
		if (teams[i].pntsScored > mostPNTS)
			mostPNTS = teams[i].pntsScored;
		totalWins += teams[i].wins;
		totalLoss += teams[i].loss;
	}

	// Multiple for loops are the easiest way to search the information
	// that has been stored to find what is wanted.
	outfile << "The team(s) with the highest win";
	outfile << " percentage are as follows: " << endl;
	for (int i = 0; i < 32; i++)
		if (teams[i].winPCNT == largestWinPCNT)
		{
		outfile << "The " << teams[i].name << " with a win percentage of ";
		outfile << fixed << setprecision(3) << teams[i].winPCNT;
		outfile << "%." << endl;
		}

	outfile << endl << "The team(s) with the fewest";
	outfile << " scored touchdowns are as follows: " << endl;
	for (int i = 0; i < 32; i++)
		if (teams[i].touchdowns == leastTD)
		{
		outfile << "The " << teams[i].name << " with ";
		outfile << teams[i].touchdowns << " touchdowns." << endl;
		}

	outfile << endl << "The total wins to losses ratio for";
	outfile << " all teams combined is: " << totalWins;
	outfile << " / " << totalLoss << endl;

	outfile << endl << "The team(s) with the most points";
	outfile << " scored are as follows: " << endl;
	for (int i = 0; i < 32; i++)
		if (teams[i].pntsScored == mostPNTS)
		{
		outfile << "The " << teams[i].name << " who scored ";
		outfile << teams[i].pntsScored << " points" << endl;
		}

	outfile << endl << "The teams in the NFC conference";
	outfile << "and East division are: " << endl;
	for (int i = 0; i < 32; i++)
	{
		if (teams[i].conf == "NFC" && teams[i].divs == "East")
		{
			outfile << "The " << teams[i].name << " with " << teams[i].wins;
			outfile << " total wins and " << teams[i].loss << " total losses,";
			outfile << endl << "making their win percentage ";
			outfile << teams[i].winPCNT << "%" << endl << endl;
		}
	}
	return 0;
}