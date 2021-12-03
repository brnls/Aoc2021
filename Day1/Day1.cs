namespace Aoc2021.Day1;

class Day1
{
    public static readonly int[] Input = File.ReadAllLines("Day1/input").Select(int.Parse).ToArray();

    public static long Part1()
    {
        return Input.Zip(Input.Skip(1)).Where(x => x.First < x.Second).Count();
    }

    public static long Part2()
    {
        var sums = Input.Zip(Input.Skip(1)).Zip(Input.Skip(2)).Select(x => x.First.First + x.First.Second + x.Second).ToArray();
        return sums.Zip(sums.Skip(1)).Where(x => x.First < x.Second).Count();
    }
}