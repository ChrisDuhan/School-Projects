//********************************************************************
//      Password Project
//      Name: Chris Duhan
//      Class: Cybersecurity: Principles & Management
//      Instructor: Dr. Halverson
//      Due Date: 04-03-2018
//********************************************************************
//      This program takes a file containing a line of numbers
//		and checks it for duplicated numbers. It takes 
//		non-duplicated numbers and stores them in a new array
//		while ignoring duplicates. The input file must have the
//		quantity of numbers on the line (minus one) as the first
//		number on the line. When telling the program the user 
//		must give the filename as well as the file extension.
//		It is simple enoungh to do without containing it to
//		it's own function so the worst case complexity that
//		the program could experience is O(2n), occuring if there
//		no unique numbers in the list. Otherwise the complexity
//		is O(n).
//
//********************************************************************
using System;

class MainClass {
  static int Main (string[] args) {
    Console.WriteLine ("Test");
    return 0;
  }
}

//********************************************************************
//		SLList::SLList()
//		Parameters: None
//		Complexity: O(1)
//		The default constructor, it makes the head node, a trailing
//		node, and the head and tail pointers and points them at the 
//		head node. Nodes are allocated dynamically.
//********************************************************************
