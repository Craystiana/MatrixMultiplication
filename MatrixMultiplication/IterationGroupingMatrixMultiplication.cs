namespace MatrixMultiplication;

public class IterationGroupingMatrixMultiplication
{
    public static int[,] ParallelMultiplyMatricesWithIterationGrouping(int[,] matrixA, int[,] matrixB)
    {
        var rowsA = matrixA.GetLength(0);
        var colsB = matrixB.GetLength(1);

        var result = new int[rowsA, colsB];

        // Define the chunk size for iteration grouping
        var chunkSize = Environment.ProcessorCount;

        // Parallelize matrix multiplication using Parallel.For with iteration grouping
        Parallel.For(0, rowsA, new ParallelOptions { MaxDegreeOfParallelism = chunkSize }, i =>
        {
            for (var j = 0; j < colsB; j++)
            {
                // Compute the result for element (i, j) using iteration grouping
                ComputeElementWithIterationGrouping(matrixA, matrixB, result, i, j, chunkSize);
            }
        });

        return result;
    }

    private static void ComputeElementWithIterationGrouping(int[,] matrixA, int[,] matrixB, int[,] result, int i, int j, int chunkSize)
    {
        var colsA = matrixA.GetLength(1);

        // Split the iteration range into chunks
        for (var k = 0; k < colsA; k += chunkSize)
        {
            // Compute the result for element (i, j) using iteration grouping
            for (var kk = k; kk < Math.Min(k + chunkSize, colsA); kk++)
            {
                result[i, j] += matrixA[i, kk] * matrixB[kk, j];
            }
        }
    }

}
