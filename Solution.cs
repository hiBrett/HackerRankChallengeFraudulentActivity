// Find the total number of notices sent to this client.
// A notice is sent each day the client spends 2x more than their median spending over the trailing days.
public static int activityNotifications(List<int> expenditures, int trailingDays)
{
    int total = 0;
    List<int> recentExpenditures = new List<int>();

    for (int i = 0; i < expenditures.Count; i++)
    {
        if (i >= trailingDays)
        {
            if (expenditures[i] >= GetMedian(recentExpenditures) * 2)
            {
                total += 1;
            }
        }

        // Keeping the list sorted allows the GetMedian function to be fast.
        InsertInOrder(recentExpenditures, expenditures[i]);
        
        if (recentExpenditures.Count > trailingDays)
        {
            int earliestDay = i - trailingDays;
            recentExpenditures.RemoveAt(BinarySearch(recentExpenditures, expenditures[earliestDay]));
        }
    }

    return total;
}

// GetMedian assumes that the list is sorted.
private static float GetMedian(List<int> list)
{
    if (list.Count % 2 == 0)
    {
        return (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2f;
    }
    else
    {
        return list[list.Count / 2];
    }
}

// BinarySearch returns either the position of the item, or where the item should go if added.
private static int BinarySearch(List<int> list, int item)
{
    int left = 0;
    int right = list.Count - 1;
    int position = 0;

    while (left < right)
    {
        position = (left + right) / 2;
        if (item > list[position])
        {
            left = position;
            if (left == right - 1)
            {
                left = right;
                position = left;
            }
        }

        if (item < list[position])
        {
            right = position;
        }

        if (item == list[position])
        {
            left = position;
            right = position;
        }
    }

    return position;
}

private static void InsertInOrder(List<int> list, int item)
{
    int position = BinarySearch(list, item);

    if (list.Count > 0 && item > list[position])
    {
        position += 1;
    }

    if (position == list.Count)
    {
        list.Add(item);
    }
    else
    {
        list.Insert(position, item);
    }
}
