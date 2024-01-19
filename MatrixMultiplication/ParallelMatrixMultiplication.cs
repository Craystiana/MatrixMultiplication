namespace MatrixMultiplication;

public class ParallelMatrixMultiplication
{
    public static int[,] ParallelMultiplyMatrices(int[,] matrixA, int[,] matrixB)
    {
        var rowsA = matrixA.GetLength(0);
        var colsA = matrixA.GetLength(1);
        var colsB = matrixB.GetLength(1);

        var result = new int[rowsA, colsB];

        // Parallelize matrix multiplication using Parallel.For
        Parallel.For(0, rowsA, i =>
        {
            for (var j = 0; j < colsB; j++)
            {
                // Compute the result for element (i, j)
                for (var k = 0; k < colsA; k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        });

        return result;
    }
}
