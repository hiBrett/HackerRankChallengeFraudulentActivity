## My solution to the HackerRank challenge "Fraudulent Activity"

Challenge Source: https://www.hackerrank.com/challenges/fraudulent-activity-notifications

#### Description
A notice is sent each time a client spends 2x more than their median spending over a number of trailing days. Find the total number of notices sent to this client. A notice is only sent when the minimum number of trailing days requirement has been met.

#### Solution
My strategy is to keep the recent expenditures list sorted in order using a binary search approach. This allows for a fast median calculation. Otherwise, the list would need to be sorted each time a median is calculated.

### Steps
1. Make a new list that will hold the recent expenditures.
2. For each expediture:
3. Insert it into the list while keeping the list sorted.
4. Remove the oldest expenditure once if we reached the trailing days total.
5. If we have enough trailing days, check if the expenditure is greater than the recent expediture list median.

### Time and Space Complexity
The space complexity is O(n) depending on the number of trailing days.
<br/>The time complexity of this solution is O(n*logn).
<br/> This is because this solution uses binary search which has an O(logn) time complexity for each item in the array.

### Improvement Ideas
The main perfomance cost comes from keeping the list sorted. This should be the first place to look for improving the performance.
<br/>A possible improvement would be to have a queue to remember the index of the earliest day rather than finding it every time. This would use more space, but the space complexity would stay the same.
<br/>The inputs could be validated, though this may not be necessary as the inputs are constrained in the problem statement.
