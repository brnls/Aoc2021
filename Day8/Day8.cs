namespace Aoc2021;

class Day8
{
    public static readonly List<string> Input = File.ReadAllLines("Day8/input").ToList();

    public static long Part1()
    {
        var acceptedSegments = new List<int> { 2, 3, 4, 7 };
        var outputValues = Input.Select(x => x.Split('|')[1]).ToList();
        return outputValues.Select(row => row.Split().Where(word => acceptedSegments.Contains(word.Length)).Count()).Sum();
    }

    public static long Part2()
    {
        throw new NotImplementedException();
    }
}
