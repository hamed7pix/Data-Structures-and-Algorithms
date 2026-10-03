using System;

class RecursiveAlgorithms
{
    static int Factorial(int n)
    {
        // 1. Base Case: The smallest instance we know the answer to immediately
        if (n == 0 || n == 1)
        {
            return 1;
        }

        // 2. Recursive Case: Call the function again with a smaller input
        return n * Factorial(n - 1);
    }

    static void CountdownAndUp(int n)
    {
        if (n <= 0)
        {
            Console.WriteLine("\nBase case reached! Unwinding the stack...\n");
            return;
        }

        Console.WriteLine($"Winding down: {n}");
        CountdownAndUp(n - 1);  // The recursive call
        Console.WriteLine($"Unwinding up: {n}");
    }

    static int Fibonacci(int n)
    {
        // Base Cases
        if (n == 0)
        {
            return 0;
        }
        else if (n == 1)
        {
            return 1;
        }

        // Recursive Case (Branching into two paths)
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public static void Run()
    {
        Console.WriteLine("\n=== Recursive Algorithms ===");
        Console.WriteLine("5! = " + Factorial(5));

        Console.WriteLine();
        CountdownAndUp(3);

        Console.WriteLine();
        Console.WriteLine("The 6th Fibonacci number is: " + Fibonacci(6));
    }
}
