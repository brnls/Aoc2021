using System.Text;

namespace Aoc2021;

class Day3
{
    public static readonly string[] Input = File.ReadAllLines("Day3/input");

    public static long Part1()
    {
       var counts = new int[Input[0].Length];
        foreach (var i in Input)
        {
            for (int j = 0; j < Input[0].Length; j++)
            {
                counts[j] += i[j] == '0' ? -1 : 1;
            }
        }

        var mcbStr = new StringBuilder();
        var lcbStr = new StringBuilder();
        foreach(var mcb in counts)
        {
            if (mcb == 0)
                throw new Exception("Expecting non-zero");
            if (mcb > 0)
            {
                mcbStr.Append('1');
                lcbStr.Append('0');
            }
            else
            {
                mcbStr.Append('0');
                lcbStr.Append('1');
            }
        }

        return Convert.ToInt32(mcbStr.ToString(), 2) * Convert.ToInt32(lcbStr.ToString(), 2);
    }

    public static long Part2()
    {
        return CalculateRating(Rating.O2Generator) * CalculateRating(Rating.CO2Scrubber);
    }

    private static int CalculateRating(Rating rating)
    {
        var filtered = Input.ToList();
        for (int i = 0; i < Input[0].Length; i++)
        {
            var commonBit = FindMCB(i, rating, filtered);
            filtered = filtered.Where(x => x[i] == commonBit).ToList();
            Console.WriteLine(filtered.Count);
            if (filtered.Count == 1)
                return Convert.ToInt32(filtered[0], 2);
        }
        throw new Exception("Expecting to have one number remaining");
    }

    private static char FindMCB(int index, Rating rating, List<string> filteredList)
    {
        var total = 0;
        foreach (var i in filteredList)
        {
            total += i[index] == '0' ? -1 : 1;
        }

        if (total == 0)
            return rating == Rating.O2Generator ? '1' : '0';
    
        var mcb = total > 0 ? '1' : '0';
        if (rating == Rating.O2Generator)
            return mcb;
        else return mcb == '1' ? '0' : '1';
    }

}

public enum Rating
{
    O2Generator,
    CO2Scrubber
}
