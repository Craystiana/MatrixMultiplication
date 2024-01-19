using System.Diagnostics;

namespace MatrixMultiplication;

internal class Program
{
    private static void Main()
    {
        // Get matrices from the user
        var matrixA = MatrixHelper.GetMatrixFromUser("Enter the first matrix:");
        var matrixB = MatrixHelper.GetMatrixFromUser("Enter the second matrix:");


        // Check if matrices are compatible for multiplication
        if (matrixA.GetLength(1) != matrixB.GetLength(0))
        {
            Console.WriteLine("Error: Matrices are not compatible for multiplication.");
            return;
        }

        var sw = Stopwatch.StartNew();
        // Perform parallel matrix multiplication
        var resultParallel = ParallelMatrixMultiplication.ParallelMultiplyMatrices(matrixA, matrixB);
        sw.Stop();

        // Display the result
        Console.WriteLine("Result of Parallel Matrix Multiplication:");
        MatrixHelper.PrintMatrix(resultParallel);
        Console.WriteLine($"Parallel Matrix Multiplication duration {sw.ElapsedMilliseconds}");

        sw.Start();
        // Perform iteration grouping matrix multiplication
        var resultIterationGrouping = IterationGroupingMatrixMultiplication.ParallelMultiplyMatricesWithIterationGrouping(matrixA, matrixB);
        sw.Stop();

        // Display the result
        Console.WriteLine("Result of Iteration Grouping Matrix Multiplication:");
        MatrixHelper.PrintMatrix(resultIterationGrouping);
        Console.WriteLine($"Parallel Iteration Grouping Multiplication duration {sw.ElapsedMilliseconds}");

        sw.Start();
        // Perform Cannon matrix multiplication
        var resultCannon = CannonMatrixMultiplication.CannonMultiply(matrixA, matrixB);
        sw.Stop();

        // Display the result
        Console.WriteLine("Result of Cannon Matrix Multiplication:");
        MatrixHelper.PrintMatrix(resultCannon);
        Console.WriteLine($"Parallel Cannon Multiplication duration {sw.ElapsedMilliseconds}");

        sw.Start();
        // Perform Strassen matrix multiplication
        var resultStrassen = StrassenMatrixMultiplication.StrassenMultiply(matrixA, matrixB);
        sw.Stop();

        // Display the result
        Console.WriteLine("Result of Strassen Matrix Multiplication:");
        MatrixHelper.PrintMatrix(resultStrassen);
        Console.WriteLine($"Parallel Strassen Multiplication duration {sw.ElapsedMilliseconds}");
    }
}