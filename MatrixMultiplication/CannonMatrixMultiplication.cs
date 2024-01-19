namespace MatrixMultiplication;

public class CannonMatrixMultiplication
{
    public static int[,] CannonMultiply(int[,] matrixA, int[,] matrixB)
    {
        var matrixSize = matrixA.GetLength(0);

        // Initialize matrices C and tempMatrix
        var matrixC = new int[matrixSize, matrixSize];
        var tempMatrix = new int[matrixSize, matrixSize];

        // Calculate block size
        var blockSize = matrixSize > Environment.ProcessorCount ? matrixSize / Environment.ProcessorCount : matrixSize;

        // Loop over each block row and block column
        for (var i = 0; i < matrixSize; i += blockSize)
        {
            for (var j = 0; j < matrixSize; j += blockSize)
            {
                // Extract block matrices
                var matrixAProc = ExtractBlock(matrixA, i, j, blockSize);
                var matrixBProc = ExtractBlock(matrixB, i, j, blockSize);

                // Perform local matrix multiplication
                MultiplyBlocks(matrixAProc, matrixBProc, tempMatrix);

                // Accumulate the result in the global matrix C
                AccumulateResult(tempMatrix, matrixC, i, j, blockSize);
            }
        }

        return matrixC;
    }

    private static int[,] ExtractBlock(int[,] sourceMatrix, int row, int col, int blockSize)
    {
        var blockMatrix = new int[blockSize, blockSize];

        for (var i = 0; i < blockSize; i++)
        {
            for (var j = 0; j < blockSize; j++)
            {
                blockMatrix[i, j] = sourceMatrix[(row + i) % sourceMatrix.GetLength(0),
                                                 (col + j) % sourceMatrix.GetLength(1)];
            }
        }

        return blockMatrix;
    }

    private static void MultiplyBlocks(int[,] matrixA, int[,] matrixB, int[,] resultMatrix)
    {
        var blockSize = matrixA.GetLength(0);

        for (var i = 0; i < blockSize; i++)
        {
            for (var j = 0; j < blockSize; j++)
            {
                for (var k = 0; k < blockSize; k++)
                {
                    resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }
    }

    private static void AccumulateResult(int[,] tempMatrix, int[,] resultMatrix, int row, int col, int blockSize)
    {
        for (var i = 0; i < blockSize; i++)
        {
            for (var j = 0; j < blockSize; j++)
            {
                resultMatrix[(row + i) % resultMatrix.GetLength(0),
                             (col + j) % resultMatrix.GetLength(1)] += tempMatrix[i, j];
            }
        }
    }

}
