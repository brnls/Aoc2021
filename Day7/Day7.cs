namespace Aoc2021;

class Day7
{
    public static readonly List<int> Input = File.ReadAllText("Day7/input").Split(",").Select(int.Parse).ToList();

    public static long Part1()
    {
        var average = (int)Math.Round(Input.Average(), MidpointRounding.AwayFromZero);


        var min = int.MaxValue;
        var max = Input.Max();
        foreach(var pos in Enumerable.Range(0, max))
        {
            var value = Math.Min(min, Input.Select(x => Math.Abs(x - pos)).Sum());
            if(value < min)
            {
                min = value;
            }
        }
        return min;
    }

    public static long Part2()
    {
        var average = (int)Math.Round(Input.Average(), MidpointRounding.AwayFromZero);


        var min = int.MaxValue;
        var max = Input.Max();
        foreach(var pos in Enumerable.Range(0, max))
        {
            var value = Math.Min(min, Input.Select(x => {
                var distance = Math.Abs(x - pos);
                return distance * (distance + 1) / 2;
            }).Sum());
            if(value < min)
            {
                min = value;
            }
        }
        return min;
    }
}
