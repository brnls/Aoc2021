using System.Text;

namespace Aoc2021;

class Day4
{
    public static readonly List<string> Input = File.ReadAllText("Day4/input").Split("\r\n\r\n").ToList();

    public static long Part1()
    {
        var bingoNums = Input.First().Split(',').Select(int.Parse).ToList();
        var boards = ParseBingoBoards();

        List<List<Position>> winningBoard = null;
        int winningNumber = 0;

        foreach (var num in bingoNums)
        {
            if (winningBoard != null) break;
            foreach (var board in boards)
            {
                if (MarkBoard(board, num))
                {
                    winningBoard = board;
                    winningNumber = num;
                    break;
                }
            }
        }
        if (winningBoard == null)
            throw new Exception("Did not find a winning board");

        return CalculateScore(winningBoard, winningNumber);
    }

    public static long Part2()
    {
        var bingoNums = Input.First().Split(',').Select(int.Parse).ToList();
        var boards = ParseBingoBoards();

        List<List<Position>> winningBoard = null;
        int winningNumber = 0;
        var completeBoards = new bool[boards.Count];
        foreach (var num in bingoNums)
        {
            for (int i = 0; i < boards.Count; i++)
            {
                if (!completeBoards[i] && MarkBoard(boards[i], num))
                {
                    winningBoard = boards[i];
                    winningNumber = num;
                    completeBoards[i] = true;
                }
            }
        }

        return CalculateScore(winningBoard, winningNumber);
    }

    public static List<List<List<Position>>> ParseBingoBoards()
    {
        var boardStrs = Input.Skip(1);
        return boardStrs.Select(x => x.Split("\r\n").Select(y => y.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(z => new Position { Val = int.Parse(z) }).ToList()).ToList()).ToList();
    }

    /// <summary>
    /// Returns true if there's a win condition
    /// Returns false if marked OR not found
    /// </summary>
    public static bool MarkBoard(List<List<Position>> board, int number)
    {
        (int x, int y) = (-1,-1);
        bool numFound = false;
        for(var i = 0; i < board.Count; i++)
        {
            for(var j = 0; j < board.Count; j++)
            {
                if (board[i][j].Val == number)
                {
                    x = i;
                    y = j;
                    board[i][j].IsMarked = true;
                    numFound = true;
                    break;
                }
            }
            if (numFound)
                break;
        }

        if (!numFound)
            return false;


        bool allMarked = true;
        for(var i = 0; i < board.Count; i++)
        {
            allMarked &= board[x][i].IsMarked;
        }
        if (allMarked) return true;

        allMarked = true;

        for(var i = 0; i < board.Count; i++)
        {
            allMarked &= board[i][y].IsMarked;
        }

        if (allMarked) return true;

        return false;
    }

    public static long CalculateScore (List<List<Position>> winningBoard, int winningNum)
    {
        var total = winningBoard.SelectMany(x => x).Where(y => !y.IsMarked).Select(z => z.Val).Sum();
        return winningNum * total;
    }
}

public class Position
{
    public int Val { get; set; }
    public bool IsMarked { get; set; }
}
