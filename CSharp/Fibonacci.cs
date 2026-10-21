using System;
using System.Collections.Generic;
using System.Linq;

class FibonacciDemo
{
    static int Fibonacci(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    class Frame
    {
        public int N { get; set; }
        public int Stage { get; set; }
        public int? Left { get; set; }
        public int? Right { get; set; }

        public Frame(int n)
        {
            N = n;
            Stage = 0;
            Left = null;
            Right = null;
        }
    }

    static int? FibonacciStackState(int n)
    {
        List<Frame> stack = new List<Frame> { new Frame(n) };
        int? returnValue = null;

        void PrintStack(string action)
        {
            string stackStr = stack.Count > 0
                ? "[" + string.Join(", ", stack.Select(f => $"fib({f.N})")) + "]"
                : "[]";

            Console.WriteLine($"{action,-16} | Stack: {stackStr}");
        }

        Console.WriteLine("--- Starting Execution ---");
        PrintStack("INIT");

        while (stack.Count > 0)
        {
            Frame frame = stack[stack.Count - 1]; // Peek
            int currN = frame.N;

            if (frame.Stage == 0)
            {
                if (currN <= 1)
                {
                    returnValue = currN;
                    stack.RemoveAt(stack.Count - 1); // Pop
                    PrintStack($"POP (return {currN})");
                }
                else
                {
                    frame.Stage = 1;
                    stack.Add(new Frame(currN - 1));
                    PrintStack($"PUSH fib({currN - 1})");
                }
            }
            else if (frame.Stage == 1)
            {
                frame.Left = returnValue;
                frame.Stage = 2;
                stack.Add(new Frame(currN - 2));
                PrintStack($"PUSH fib({currN - 2})");
            }
            else if (frame.Stage == 2)
            {
                frame.Right = returnValue;
                int result = frame.Left.Value + frame.Right.Value;
                returnValue = result;
                stack.RemoveAt(stack.Count - 1); // Pop
                PrintStack($"POP (return {result})");
            }
        }

        Console.WriteLine("--- Execution Complete ---");
        return returnValue;
    }

    static void PrintFibonacciTree(int n, string prefix = "", bool isLeft = true, bool isRoot = true)
    {
        if (isRoot)
        {
            Console.WriteLine($"fib({n})");
        }
        else
        {
            string branch = isLeft ? "├── " : "└── ";
            Console.WriteLine($"{prefix}{branch}fib({n})");
        }

        if (n <= 1)
        {
            return;
        }

        string newPrefix;
        if (isRoot)
        {
            newPrefix = "";
        }
        else
        {
            newPrefix = prefix + (isLeft ? "│   " : "    ");
        }

        PrintFibonacciTree(n - 1, newPrefix, true, false);
        PrintFibonacciTree(n - 2, newPrefix, false, false);
    }

    public static void Run()
    {
        Console.WriteLine("\n=== Fibonacci Demo ===");
        Console.WriteLine(Fibonacci(4));
        Console.WriteLine();

        FibonacciStackState(3);
        Console.WriteLine();

        PrintFibonacciTree(4);
    }
}
