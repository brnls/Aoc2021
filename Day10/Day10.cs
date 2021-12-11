namespace Aoc2021;

class Day10
{
    public static readonly List<string> Input = File.ReadAllLines("Day10/input").ToList();
    public static readonly HashSet<char> ClosingSymbols = new HashSet<char> { ')', '}', ']', '>' };
    public static readonly HashSet<char> OpeningSymbols = new HashSet<char> { '(', '{', '[', '<' };

    public static long Part1()
    {
        return Input.Select(x => FindCorruptedPosition(x))
            .Where(x => x != null)
            .GroupBy(x => x!.Value)
            .Select(x => x.Count() * GetValue(x.Key))
            .Sum();
    }

    public static int GetValue(char symbol)
    {
        return symbol switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137,
            _ => throw new Exception("Unexpected symbol")
        };
    }


    public static char? FindCorruptedPosition(string input)
    {
        var stack = new Stack<char>();
        foreach (var symbol in input)
        {
            if (OpeningSymbols.Contains(symbol))
                stack.Push(symbol);
            else
            {
                if (!stack.Any())
                    return symbol;
                else
                {
                    if (!IsMatch(symbol, stack.Pop()))
                        return symbol;
                }
            }
        }
        return null;
    }

    public static bool IsMatch(char closingSymbol, char fromStack)
    {
        return closingSymbol switch
        {
            ')' => fromStack == '(',
            '}' => fromStack == '{',
            ']' => fromStack == '[',
            '>' => fromStack == '<',
            _ => throw new Exception("Unexpected closing symbol")
        };
    }


    public static long Part2()
    {
        throw new NotImplementedException();
    }

}
