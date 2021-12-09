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
        Dostuff(Input[0]);
        return 0;
    }

    public static void DoStuff(string line)
    {
        var legend = new char[7];
        var lineSplit = line.Split("|");
        var legendNums = lineSplit[0].Split();

        var one = legendNums.Single(x => x.Length == 2);
        var four = legendNums.Single(x => x.Length == 4);
        var seven = legendNums.Single(x => x.Length == 3);
        var eight = legendNums.Single(x => x.Length == 7);
        var sixOrZeroOrNine = legendNums.Where(x => x.Length == 6);
        var six = sixOrZeroOrNine.Single(x => x.Except(one).Count() == 5);
        var zeroOrNine = sixOrZeroOrNine.Where(x => x != six);


        legend[0] = seven.Except(one).Single();
        legend[1] = eight.Except(six).Single();
        legend[2] = one.Where(x => x != legend[1]).Single();

        var nine = zeroOrNine.Single(x => x.Except(four.Concat(new char[] { legend[0] })).Count() == 1);
        var zero = zeroOrNine.Single(x => x != nine);

        legend[3] = nine.Except(four.Concat(new char[] { legend[0] })).SingleOrDefault();
        legend[4] = eight.Except(nine).Single();
        legend[6] = eight.Except(zero).Single();
        legend[5] = eight.Except(legend).Single();
    }

}
