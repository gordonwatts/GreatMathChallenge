# GreatMathChallenge
Attempt to systematically discover all possible equations for a Elementary School Math Challenge

# The Challenge

Using the numbers 1, 8, 9, and 6 once and only once, and in that order,
find an equation that totals 0, 1, 2, etc., to 100. You may use the following operations:

   - +, -, *, and /
   - Exponent
   - Factorial
   - Join (e.g. "1" and "8" become "18"). But this may only be done on the original numbers.
   - Absolute Value
   - Square Root
   
Here is a sample of the output:

	Found 91 solutions.
	Tried 2026228 combinations.
	0 = (1 - ((8 - 9)^6))
	1 = (1^(896))
	2 = ((1^8)/(sq(9)/6))
	3 = ((18)/(9 - 6)!)
	4 = (-((18)/9) + 6)
	5 = -((1^(89)) - 6)
	6 = ((1^(89))*6)

# Code

I thought this was going to be easy!

    - The number of combinations explodes when you consider nested unary
	- Some work has been done to prune the combinations - hopefully there are no bugs
	- This uses LINQ and AsParallel to go multithreaded
	- Takes awhile to run on a Surface Pro 2 (at least 30 minutes, didn't time it beyond that)

The whiteboard jpeg is mostly my kid's as we developed the algorithm. Sadly, I did all the coding
(which you may not be able to tell!!).

