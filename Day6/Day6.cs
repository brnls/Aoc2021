namespace Aoc2021;

class Day6
{
    public static readonly List<int> Input = File.ReadAllText("Day6/input").Split(",").Select(int.Parse).ToList();

    public static long Part1()
    {
        var fish = Input.ToList();

        int newFish = 0;
        for (int day = 0; day < 80; day++)
        {
            fish.AddRange(Enumerable.Range(0, newFish).Select(x => 9));
            newFish = 0;
            for (int fishIndex = 0; fishIndex < fish.Count; fishIndex++)
            {
                var newFishValue = UpdateFishTimer(fish[fishIndex]);
                fish[fishIndex] = newFishValue;
                if (newFishValue == 0) newFish++;
            }
        }
        return fish.Count;
    }

    public static long Part2()
    {
        var fish = Input.GroupBy(x => x)
            .ToDictionary(x => x.First(), x => x.LongCount());

        var newFish = 0L;
        for (int day = 0; day < 256; day++)
        {
            var nextDay = new Dictionary<int, long>();
            nextDay[8] = newFish;
            newFish = 0L;
            foreach (var fishGroup in fish)
            {
                var newFishValue = UpdateFishTimer(fishGroup.Key);
                if (nextDay.ContainsKey(newFishValue))
                {
                    nextDay[newFishValue] += fishGroup.Value;
                }
                else
                {
                    nextDay[newFishValue] = fishGroup.Value;
                }
                if (newFishValue == 0) newFish = fishGroup.Value;
            }
            fish = nextDay;
        }

        return fish.Select(x => x.Value).Sum();
    }

    static int UpdateFishTimer(int num)
    {
        if (num == 0) return 6;
        return num - 1;
    }
}






