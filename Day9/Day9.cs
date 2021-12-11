namespace Aoc2021;

class Day9
{
    public static readonly List<string> Input = File.ReadAllLines("Day9/input").ToList();

    public static long Part1()
    {
        var grid = CreateGrid();
        int height = Input.Count();
        int width = Input[0].Length;
        int sum = 0;
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (IsLowPoint(grid, row, col, height, width))
                    sum += (grid[row, col] + 1);
            }
        }
        return sum;
    }

    public static int[,] CreateGrid()
    {
        int width = Input[0].Length;
        int height = Input.Count();

        int[,] grid = new int[height, width];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                grid[row, col] = int.Parse(Input[row][col].ToString());
            }
        }
        return grid;
    }

    public static bool IsLowPoint(int[,] grid, int row, int col, int height, int width)
    {
        bool lowest = true;
        int position = grid[row, col];
        if (row - 1 >= 0)
            lowest &= position < grid[row - 1, col];
        if (row + 1 < height)
            lowest &= position < grid[row + 1, col];
        if (col - 1 >= 0)
            lowest &= position < grid[row, col - 1];
        if (col + 1 < width)
            lowest &= position < grid[row, col + 1];

        return lowest;
    }

    public static long Part2()
    {
        throw new NotImplementedException();
    }

}
