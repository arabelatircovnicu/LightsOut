using System;

namespace LightsOut.Classes
{
    public class ArrayHelper
    {
        public static void GetSolutionWithGaussianReduction(Boolean[,] matrix)
        {
            int lineCount = matrix.GetUpperBound(0) + 1;
            int columnCount = matrix.GetUpperBound(1) + 1;

            var toggleMatrix = GetToggleMatrix(lineCount, columnCount);

            var vector = BuildVectorFromMatrix(matrix);
            var augmentedMatrix = AddColumnToMatrix(toggleMatrix, vector);
            
            Boolean[] solution = new Boolean[lineCount * columnCount];

            var reducedMatrix = GetReducedMatrix(augmentedMatrix);

            if (reducedMatrix != null && solution!=null)
            {
                Console.WriteLine("To solve the problem the following cell should be pressed: ");
                for (int i = 0; i < solution.Length; i++)
                {
                    solution[i] = reducedMatrix[i, reducedMatrix.GetUpperBound(1)];
                    if (solution[i])
                        Console.WriteLine(i);                    
                }
            }
            else
            {
                Console.WriteLine("There is no solution available!");
            }                    
        }

        private static int[,] GetMatrix(Boolean[,] matrix)
        {
            var result = new int[matrix.GetUpperBound(0) + 1, matrix.GetUpperBound(1) + 1];

            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                    result[i, j] = Convert.ToInt32(matrix[i, j]);
            }

            return result;
        }

        private static void Print(Boolean[,] matrix)
        {
            Console.WriteLine();

            var b = GetMatrix(matrix);

            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                    Console.Write(b[i, j] + "    ");
            }
        }

        private static void Print(Boolean[] vector)
        {
            Console.WriteLine();

            for (int i = 0; i < vector.Length; i++)
            {
                Console.Write(vector[i] + "    ");
            }
        }
        
        private static Boolean[,] GetReducedMatrix(bool[,] matrix)
        {
           // Print(matrix);
            var lineCount = matrix.GetUpperBound(0) + 1;
            var columnCount = matrix.GetUpperBound(1) + 1;

            int pivotRow = 0;
            int pivotColumn = 0;

            while (pivotRow < lineCount && pivotColumn < columnCount)
            {
                if (matrix[pivotRow, pivotColumn] == false)
                {
                    var firstPivotFound = GetFirstPivot(matrix, pivotRow, pivotColumn);
                    if (firstPivotFound == -1)
                    {
                       break;
                    }
                       
                    if (pivotRow != firstPivotFound)
                    {
                        SwapLinesInMatrix(matrix, pivotRow, firstPivotFound);                                                                     
                    }
                }

                for (int i = pivotRow + 1; i < lineCount; i++)
                {
                    if (matrix[i, pivotColumn] == true)
                    {
                        for (int j = pivotColumn; j < columnCount; j++)
                        {
                            var aux = (Convert.ToInt32(matrix[i, j]) + Convert.ToInt32(matrix[pivotRow, j])) % 2;
                            matrix[i, j] = Convert.ToBoolean(aux);
                        }
                    }
                }

                for (int i = 0; i < pivotRow; i++)
                {
                    if (matrix[i, pivotColumn] == true)
                    {
                        for (int j = pivotColumn; j < columnCount; j++)
                        {
                            var aux = (Convert.ToInt32(matrix[i, j]) + Convert.ToInt32(matrix[pivotRow, j])) % 2;
                            matrix[i, j] = Convert.ToBoolean(aux);
                        }
                    }
                }

                //Print(matrix);
                pivotRow++;
                pivotColumn++;
            }
            
            return matrix;
        }

        private static void SwapLinesInMatrix(Boolean[,] matrix, int pivotRow, int firstPivotFound)
        {
            for(int j=0; j<matrix.GetUpperBound(1) + 1;j++)
            {
                var aux = matrix[pivotRow, j];
                matrix[pivotRow, j] = matrix[firstPivotFound, j];
                matrix[firstPivotFound, j] = aux;
            }            
        }

        private static int GetFirstPivot(Boolean[,] matrix, int lineStartPosition, int columnPosition)
        {
            var position = -1;
            var columnCount = matrix.GetUpperBound(0) + 1;

            for (int i = lineStartPosition; i < columnCount; i++)
            {
                if (matrix[i, columnPosition] == true)
                    return i;

                
            }

            return position;
        }

        private static Boolean[,] GetToggleMatrix(int n, int m)
        {
            Boolean[,] result = new Boolean[n * m, n * m];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    int col = RowMajorIndex(i, j, m);
                    result[col, col] = true;

                    for (int di = -1; di <= +1; ++di)
                    {
                        for (int dj = -1; dj <= +1; ++dj)
                        {
                            if ((di == 0) == (dj == 0)) continue;

                            if (i + di < n && i + di >= 0 && j + dj >= 0 && j + dj < m)
                                result[col, RowMajorIndex(i + di, j + dj, m)] = true;
                        }
                    }
                }
            }

            return result;
        }

        private static int RowMajorIndex(int i, int j, int ColumnCount)
        {
            return i * ColumnCount + j;
        }

        private static Boolean[] BuildVectorFromMatrix(Boolean[,] matrix)
        {
            int columnCount = matrix.GetUpperBound(1) + 1;
            int lineCount = matrix.GetUpperBound(0) + 1;

            var result = new Boolean[lineCount * columnCount];

            for (int i = 0; i < lineCount; i++)
                for (int j = 0; j < columnCount; j++)
                {
                    int column = RowMajorIndex(i, j, columnCount);
                    result[column] = matrix[i, j];
                }

            return result;
        }

        private static Boolean[,] AddColumnToMatrix(Boolean[,] matrix, Boolean[] vector)
        {
            var columnCount = matrix.GetUpperBound(1) + 1;
            var lineCount = matrix.GetUpperBound(0) + 1;

            var result = new Boolean[lineCount, columnCount + 1];

            for (int i = 0; i < lineCount; i++)
                for (int j = 0; j < columnCount; j++)
                    result[i, j] = matrix[i, j];

            for (int i = 0; i < vector.Length; i++)
                result[i, columnCount] = vector[i];

            return result;
        }
    }
}
