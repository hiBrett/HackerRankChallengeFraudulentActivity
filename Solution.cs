using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{
    public static int activityNotifications(List<int> expenditures, int d)
    {
        int total = 0;
        List<int> recentExpenditures = new List<int>();

        for (int i = 0; i < expenditures.Count; i++)
        {
            if (i >= d)
            {
                if (expenditures[i] >= GetMedian(recentExpenditures) * 2)
                {
                    total += 1;
                }
            }

            InsertInOrder(recentExpenditures, expenditures[i]);
            if (recentExpenditures.Count > d)
            {
                recentExpenditures.RemoveAt(BinarySearch(recentExpenditures, expenditures[i - d]));
            }
        }

        return total;
    }

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
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int d = Convert.ToInt32(firstMultipleInput[1]);

        List<int> expenditure = Console.ReadLine().TrimEnd().Split(' ').ToList()
            .Select(expenditureTemp => Convert.ToInt32(expenditureTemp)).ToList();

        int result = Result.activityNotifications(expenditure, d);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
