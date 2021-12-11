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
        var scores = Input.Where(x => FindCorruptedPosition(x) == null)
            .Select(line => FindCompletionString(line))
            .Select(str => CalculateScore(str))
            .OrderBy(x => x)
            .ToArray();
        return scores[scores.Count() / 2];
    }

    public static long CalculateScore(string completionString)
    {
        long totalScore = 0L;
        foreach (var c in completionString)
        {
            totalScore *= 5;
            totalScore += GetCompletionScore(c);
        }
        return totalScore;
    }

    public static int GetCompletionScore(char c)
    {
        return c switch
        {
            ')' => 1,
            ']' => 2,
            '}' => 3,
            '>' => 4,
            _ => 0
        };
    }

    public static string FindCompletionString(string input)
    {
        var completionString = "";
        var stack = new Stack<char>();
        foreach (var symbol in input)
        {
            if (OpeningSymbols.Contains(symbol))
                stack.Push(symbol);
            else
            {
                if (!stack.Any())
                    throw new Exception("not sure yet...");
                else
                {
                    if (!IsMatch(symbol, stack.Pop()))
                        throw new Exception("Expecting valid input");

                }
            }
        }

        if (!stack.Any())
            throw new Exception("not expecting empty stack");

        while (stack.Any())
        {
            completionString += GetComplement(stack.Pop());
        }

        return completionString;
    }

    public static char GetComplement(char openingSymbol)
    {
        return openingSymbol switch
        {
            '(' => ')',
            '[' => ']',
            '{' => '}',
            '<' => '>',
            _ => throw new Exception("Unexpected symbol")
        };
    }

}
