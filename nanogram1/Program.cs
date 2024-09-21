using System;

namespace NanogramSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the nanogram grid:");
            int gridSize = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the numbers in the set (separated by spaces):");
            string[] numberSetStr = Console.ReadLine().Split(' ');
            int[] numberSet = new int[numberSetStr.Length];
            for (int i = 0; i < numberSetStr.Length; i++)
            {
                numberSet[i] = Convert.ToInt32(numberSetStr[i]);
            }

            // Initialize the grid with zeros
            int[,] grid = new int[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = 0;
                }
            }

            Console.WriteLine("Enter the initial values of the grid (row by row, separated by spaces):");
            for (int i = 0; i < gridSize; i++)
            {
                string[] rowStr = Console.ReadLine().Split(' ');
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = Convert.ToInt32(rowStr[j]);
                }
            }

            // Solve the nanogram
            if (SolveNanogram(grid, numberSet))
            {
                // Print the solution
                PrintGrid(grid);
            }
            else
            {
                Console.WriteLine("No solution found.");
            }
        }

        static bool SolveNanogram(int[,] grid, int[] numberSet)
        {
            // Iterate through each cell in the grid
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    // If the cell is already filled, skip it
                    if (grid[i, j] != 0)
                    {
                        continue;
                    }

                    // Try each number in the set
                    foreach (int number in numberSet)
                    {
                        // Check if the number can be placed in the cell
                        if (CanPlaceNumber(grid, i, j, number))
                        {
                            // Place the number in the cell
                            grid[i, j] = number;

                            // Recursively solve the rest of the grid
                            if (SolveNanogram(grid, numberSet))
                            {
                                return true;
                            }

                            // If the recursive call returns false, backtrack and try another number
                            grid[i, j] = 0;
                        }
                    }

                    // If no number can be placed in the cell, return false
                    return false;
                }
            }

            // If all cells have been filled, return true
            return true;
        }

        static bool CanPlaceNumber(int[,] grid, int row, int col, int number)
        {
            // Check if the number is already in the row or column
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (grid[row, i] == number || grid[i, col] == number)
                {
                    return false;
                }
            }

            // Return true if the number is not already in the row or column
            return true;
        }

        static void PrintGrid(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}