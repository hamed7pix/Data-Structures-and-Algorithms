using System;
using System.Collections.Generic;
using System.Linq;

class TimeComplexity
{
    static int? CheckFirstElement(List<int> elements)
    {
        if (elements == null || elements.Count == 0)
        {
            return null;
        }
        return elements[0];
    }

    static string LinearSearch(List<int> elements, int target)
    {
        int iterations = 0;
        for (int i = 0; i < elements.Count; i++)
        {
            iterations++;
            if (elements[i] == target)
            {
                return $"Found at index {i} in {iterations} iterations";
            }
        }
        return $"Not found after {iterations} iterations";
    }

    static string BinarySearch(List<int> sortedElements, int target)
    {
        int left = 0;
        int right = sortedElements.Count - 1;
        int iterations = 0;

        while (left <= right)
        {
            iterations++;
            int mid = left + (right - left) / 2;
            if (sortedElements[mid] == target)
            {
                return $"Found in {iterations} iterations";
            }
            else if (sortedElements[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return $"Not found after {iterations} iterations";
    }

    static List<int> QuickSort(List<int> arr)
    {
        if (arr.Count <= 1)
        {
            return arr;
        }

        int pivot = arr[arr.Count - 1];
        List<int> left = new List<int>();
        List<int> right = new List<int>();

        for (int i = 0; i < arr.Count - 1; i++)
        {
            if (arr[i] <= pivot)
            {
                left.Add(arr[i]);
            }
            else
            {
                right.Add(arr[i]);
            }
        }

        List<int> result = new List<int>();
        result.AddRange(QuickSort(left));
        result.Add(pivot);
        result.AddRange(QuickSort(right));
        return result;
    }

    static string OptimizedBubbleSort(List<int> arr)
    {
        List<int> elements = new List<int>(arr);
        int n = elements.Count;
        int iterations = 0;

        for (int i = 0; i < n; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                iterations++;
                if (elements[j] > elements[j + 1])
                {
                    int temp = elements[j];
                    elements[j] = elements[j + 1];
                    elements[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped)
            {
                break;
            }
        }
        return $"Sorted in {iterations} comparisons";
    }

    public static void Run()
    {
        Console.WriteLine("\n=== Time Complexity ===");
        // 1. O(1) Check first element
        List<int> bestCaseInput1 = new List<int> { 42 };
        List<int> averageCaseInput1 = Enumerable.Range(0, 1000).ToList();
        List<int> worstCaseInput1 = Enumerable.Range(0, 1000000).ToList();

        Console.WriteLine("Best case: " + CheckFirstElement(bestCaseInput1));
        Console.WriteLine("Average case: " + CheckFirstElement(averageCaseInput1));
        Console.WriteLine("Worst case: " + CheckFirstElement(worstCaseInput1));

        // 2. Linear Search
        List<int> dataset2 = new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        Console.WriteLine("Best case (Target 10): " + LinearSearch(dataset2, 10));
        Console.WriteLine("Average case (Target 50): " + LinearSearch(dataset2, 50));
        Console.WriteLine("Worst case (Target 999): " + LinearSearch(dataset2, 999));

        // 3. Binary Search
        List<int> sortedDataset3 = Enumerable.Range(1, 100).ToList();
        Console.WriteLine("Best case (Target 50): " + BinarySearch(sortedDataset3, 50));
        Console.WriteLine("Average case (Target 25): " + BinarySearch(sortedDataset3, 25));
        Console.WriteLine("Worst case (Target 999): " + BinarySearch(sortedDataset3, 999));

        // 4. Quick Sort
        List<int> bestInput4 = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6, 5 };

        Random rnd = new Random();
        List<int> averageInput4 = Enumerable.Range(1, 100).OrderBy(x => rnd.Next()).Take(10).ToList();
        List<int> worstInput4 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Console.WriteLine("Best case sorted: [" + string.Join(", ", QuickSort(bestInput4)) + "]");
        Console.WriteLine("Average case sorted: [" + string.Join(", ", QuickSort(averageInput4)) + "]");
        Console.WriteLine("Worst case sorted: [" + string.Join(", ", QuickSort(worstInput4)) + "]");

        // 5. Bubble Sort
        List<int> bestCase5 = new List<int> { 1, 2, 3, 4, 5 };
        Console.WriteLine("Best case: " + OptimizedBubbleSort(bestCase5));

        List<int> averageCase5 = new List<int> { 4, 1, 3, 5, 2 };
        Console.WriteLine("Average case: " + OptimizedBubbleSort(averageCase5));

        List<int> worstCase5 = new List<int> { 5, 4, 3, 2, 1 };
        Console.WriteLine("Worst case: " + OptimizedBubbleSort(worstCase5));
    }
}
