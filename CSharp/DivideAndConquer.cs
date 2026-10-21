using System;
using System.Collections.Generic;
using System.Linq;

class DivideAndConquer
{
    static (List<int>, int) InsertionSortWithCounter(List<int> arr)
    {
        List<int> A = new List<int>(arr);
        int n = A.Count;
        int comparisons = 0;

        for (int i = 1; i < n; i++)
        {
            int key = A[i];
            int j = i - 1;

            while (j >= 0)
            {
                comparisons++;
                if (A[j] > key)
                {
                    A[j + 1] = A[j];
                    j--;
                }
                else
                {
                    break;
                }
            }
            A[j + 1] = key;
        }

        return (A, comparisons);
    }

    static (List<int>, int) MergeSortWithCounter(List<int> arr)
    {
        if (arr.Count <= 1)
        {
            return (arr, 0);
        }

        int mid = arr.Count / 2;

        var (leftSorted, leftCount) = MergeSortWithCounter(arr.Take(mid).ToList());
        var (rightSorted, rightCount) = MergeSortWithCounter(arr.Skip(mid).ToList());

        var (merged, mergeCount) = Merge(leftSorted, rightSorted);

        int totalComparisons = leftCount + rightCount + mergeCount;
        return (merged, totalComparisons);
    }

    static (List<int>, int) Merge(List<int> left, List<int> right)
    {
        List<int> result = new List<int>();
        int i = 0;
        int j = 0;
        int comparisons = 0;

        while (i < left.Count && j < right.Count)
        {
            comparisons++;
            if (left[i] <= right[j])
            {
                result.Add(left[i]);
                i++;
            }
            else
            {
                result.Add(right[j]);
                j++;
            }
        }

        while (i < left.Count)
        {
            result.Add(left[i]);
            i++;
        }

        while (j < right.Count)
        {
            result.Add(right[j]);
            j++;
        }

        return (result, comparisons);
    }

    static List<int> GenerateWorstCase(List<int> arr)
    {
        if (arr.Count <= 1)
        {
            return arr;
        }

        List<int> evens = new List<int>();
        List<int> odds = new List<int>();

        for (int i = 0; i < arr.Count; i++)
        {
            if (i % 2 == 0)
            {
                evens.Add(arr[i]);
            }
            else
            {
                odds.Add(arr[i]);
            }
        }

        List<int> left = GenerateWorstCase(evens);
        List<int> right = GenerateWorstCase(odds);

        List<int> result = new List<int>(left);
        result.AddRange(right);
        return result;
    }

    public static void Run()
    {
        Console.WriteLine("\n=== Divide and Conquer ===");
        int N = 20;

        // Insertion Sort
        Console.WriteLine("--- Insertion Sort ---");
        List<int> bestCaseIns = Enumerable.Range(1, N).ToList();
        Console.WriteLine("Best Case: [" + string.Join(", ", bestCaseIns) + "]");

        List<int> worstCaseIns = Enumerable.Range(1, N).Reverse().ToList();
        Console.WriteLine("Worst Case: [" + string.Join(", ", worstCaseIns) + "]");

        Random rnd = new Random();
        List<int> averageCaseIns = Enumerable.Range(1, N).OrderBy(x => rnd.Next()).ToList();
        Console.WriteLine("Average Case: [" + string.Join(", ", averageCaseIns) + "]");

        var (_, bestCountIns) = InsertionSortWithCounter(bestCaseIns);
        var (_, worstCountIns) = InsertionSortWithCounter(worstCaseIns);
        var (_, avgCountIns) = InsertionSortWithCounter(averageCaseIns);

        Console.WriteLine($"\n{"Case Scenario",-15} | {"Initial Array Status",-22} | {"Executions (N=20)",-18} | {"Theoretical Limit"}");
        Console.WriteLine(new string('-', 82));
        Console.WriteLine($"{"Best Case",-15} | {"Already Sorted",-22} | {bestCountIns,-18} | O(N)");
        Console.WriteLine($"{"Average Case",-15} | {"Randomly Shuffled",-22} | {avgCountIns,-18} | O(N^2)");
        Console.WriteLine($"{"Worst Case",-15} | {"Reverse Sorted",-22} | {worstCountIns,-18} | O(N^2)");

        Console.WriteLine("\nMathematical Verification:");
        Console.WriteLine($"- Best Case expects N-1 comparisons: {N} - 1 = {N - 1}");
        Console.WriteLine($"- Worst Case expects N(N-1)/2 comparisons: {N} * {N - 1} / 2 = {N * (N - 1) / 2}");


        // Merge Sort
        Console.WriteLine("\n\n--- Merge Sort ---");
        N = 16;
        List<int> bestCaseMerge = Enumerable.Range(1, N).ToList();
        Console.WriteLine("Best Case: [" + string.Join(", ", bestCaseMerge) + "]");

        List<int> worstCaseMerge = GenerateWorstCase(Enumerable.Range(1, N).ToList());
        Console.WriteLine("Worst Case: [" + string.Join(", ", worstCaseMerge) + "]");

        List<int> averageCaseMerge = Enumerable.Range(1, N).OrderBy(x => rnd.Next()).ToList();
        Console.WriteLine("Average Case: [" + string.Join(", ", averageCaseMerge) + "]");

        var (_, bestCountMerge) = MergeSortWithCounter(bestCaseMerge);
        var (_, worstCountMerge) = MergeSortWithCounter(worstCaseMerge);
        var (_, avgCountMerge) = MergeSortWithCounter(averageCaseMerge);

        Console.WriteLine($"\n{"Case Scenario",-15} | {"Initial Array Status",-22} | {"Executions (N=16)",-18} | {"Time Complexity"}");
        Console.WriteLine(new string('-', 75));
        Console.WriteLine($"{"Best Case",-15} | {"Already Sorted",-22} | {bestCountMerge,-18} | O(N log N)");
        Console.WriteLine($"{"Average Case",-15} | {"Randomly Shuffled",-22} | {avgCountMerge,-18} | O(N log N)");
        Console.WriteLine($"{"Worst Case",-15} | {"Maximum Interleaved",-22} | {worstCountMerge,-18} | O(N log N)");

        Console.WriteLine("\nMathematical Verification (For N=2^k):");
        int log2N = (int)Math.Log2(N);
        Console.WriteLine($"- Best Case expects (N/2) * log2(N) comparisons: ({N}/2) * {log2N} = {(N / 2) * log2N}");
        Console.WriteLine($"- Worst Case expects N * log2(N) - N + 1 comparisons: {N} * {log2N} - {N} + 1 = {N * log2N - N + 1}");
    }
}
