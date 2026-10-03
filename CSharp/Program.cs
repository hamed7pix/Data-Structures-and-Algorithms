using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Running all algorithms...");

        TimeComplexity.Run();
        RecursiveAlgorithms.Run();
        FibonacciDemo.Run();
        DivideAndConquer.Run();

        Console.WriteLine("\nAll algorithms executed successfully.");
    }
}
