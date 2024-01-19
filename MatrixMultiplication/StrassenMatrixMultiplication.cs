namespace MatrixMultiplication;

public  class StrassenMatrixMultiplication
{
    public static int[,] StrassenMultiply(int[,] matrixA, int[,] matrixB)
    {
        var n = matrixA.GetLength(0);

        // Base case: If the matrices are 1x1, perform standard multiplication
        if (n == 1)
        {
            var resultMatrix = new int[1, 1];
            resultMatrix[0, 0] = matrixA[0, 0] * matrixB[0, 0];
            return resultMatrix;
        }

        // Split matrices into quadrants

        SplitMatrix(matrixA, out var a11, out var a12, out var a21, out var a22);
        SplitMatrix(matrixB, out var b11, out var b12, out var b21, out var b22);

        // Strassen's recursive formulas
        var s1 = SubtractMatrices(b12, b22);
        var s2 = AddMatrices(a11, a12);
        var s3 = AddMatrices(a21, a22);
        var s4 = SubtractMatrices(b21, b11);
        var s5 = AddMatrices(a11, a22);
        var s6 = AddMatrices(b11, b22);
        var s7 = SubtractMatrices(a12, a22);
        var s8 = AddMatrices(b21, b22);
        var s9 = SubtractMatrices(a11, a21);
        var s10 = AddMatrices(b11, b12);

        // Recursive multiplications
        var p1 = StrassenMultiply(a11, s1);
        var p2 = StrassenMultiply(s2, b22);
        var p3 = StrassenMultiply(s3, b11);
        var p4 = StrassenMultiply(a22, s4);
        var p5 = StrassenMultiply(s5, s6);
        var p6 = StrassenMultiply(s7, s8);
        var p7 = StrassenMultiply(s9, s10);

        // Compute quadrants of the result matrix
        var c11 = AddMatrices(SubtractMatrices(AddMatrices(p5, p4), p2), p6);
        var c12 = AddMatrices(p1, p2);
        var c21 = AddMatrices(p3, p4);
        var c22 = SubtractMatrices(SubtractMatrices(AddMatrices(p5, p1), p3), p7);

        // Combine quadrants to form the result matrix
        return CombineMatrices(c11, c12, c21, c22);
    }

    private static void SplitMatrix(int[,] sourceMatrix, out int[,] topLeft, out int[,] topRight, out int[,] bottomLeft, out int[,] bottomRight)
    {
        var n = sourceMatrix.GetLength(0) / 2;
        topLeft = new int[n, n];
        topRight = new int[n, n];
        bottomLeft = new int[n, n];
        bottomRight = new int[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                topLeft[i, j] = sourceMatrix[i, j];
                topRight[i, j] = sourceMatrix[i, j + n];
                bottomLeft[i, j] = sourceMatrix[i + n, j];
                bottomRight[i, j] = sourceMatrix[i + n, j + n];
            }
        }
    }

    private static int[,] AddMatrices(int[,] matrixA, int[,] matrixB)
    {
        var n = matrixA.GetLength(0);
        var resultMatrix = new int[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                resultMatrix[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }

        return resultMatrix;
    }

    private static int[,] SubtractMatrices(int[,] matrixA, int[,] matrixB)
    {
        var n = matrixA.GetLength(0);
        var resultMatrix = new int[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                resultMatrix[i, j] = matrixA[i, j] - matrixB[i, j];
            }
        }

        return resultMatrix;
    }

    private static int[,] CombineMatrices(int[,] matrix11, int[,] matrix12, int[,] matrix21, int[,] matrix22)
    {
        var n = matrix11.GetLength(0);
        var resultMatrix = new int[2 * n, 2 * n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                resultMatrix[i, j] = matrix11[i, j];
                resultMatrix[i, j + n] = matrix12[i, j];
                resultMatrix[i + n, j] = matrix21[i, j];
                resultMatrix[i + n, j + n] = matrix22[i, j];
            }
        }

        return resultMatrix;
    }
}
