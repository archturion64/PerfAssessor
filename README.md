# PerfAssessor
A C# benchmarking tool for testing containers and routines.

This code is by no means readable since that was not my aim whilst writing it.
I wanted to see how far I could be able to push the functional programming envelope in C#.
In the process I wanted to explore some of C#'s more used containers and their performance.


Example usage:	
	Enter a number for your choice of action: 
	0: Back
	1: Hashset<Tuple>
	2: Hashset<ValueTuple>
	3: Run all async
	
	2
	[ .  .  .  .  .  .  .  .  .  . ]
	Bench Hashset<ValueTuple> complete in: 210.4 ms
