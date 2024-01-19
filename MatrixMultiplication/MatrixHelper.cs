namespace MatrixMultiplication;

public class MatrixHelper
{
    public static int[,] GetMatrixFromUser(string prompt)
    {
        Console.WriteLine(prompt);
        Console.Write("Enter number of rows: ");
        var rows = int.Parse(Console.ReadLine());

        Console.Write("Enter number of columns: ");
        var cols = int.Parse(Console.ReadLine());

        var matrix = new int[rows, cols];

        Console.WriteLine($"Enter {rows}x{cols} matrix elements:");

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                Console.Write($"Element at position ({i + 1}, {j + 1}): ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }

        return matrix;
    }

    public static void PrintMatrix(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
