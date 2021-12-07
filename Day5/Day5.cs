
namespace Aoc2021;

class Day5
{
    public static readonly List<string> Input = File.ReadAllLines("Day5/input").ToList();

    public static long Part1()
    {
        var coordinates = ParseCoordinates().Where(c => (c.IsVerticalLine() || c.IsHorizontalLine())).ToList();
        return FindOverlaps(coordinates);
    }

    public static long Part2()
    {
        var coordinates = ParseCoordinates().Where(c => c.IsVerticalLine() || c.IsHorizontalLine() || c.IsDiagonalLineEqual() || c.IsDiagonalLineOpposite()).ToList();
        return FindOverlaps(coordinates);
    }

    public static long FindOverlaps(List<Line> coordinates)
    {
        var positions = new Dictionary<Coordinate, int>();
        foreach (var coordinatePair in coordinates)
        {
            var coordList = GenerateCoordinates(coordinatePair);
            foreach (var generatedCoordinate in coordList)
            {
                if (positions.ContainsKey(generatedCoordinate))
                    positions[generatedCoordinate] += 1;
                else
                    positions.Add(generatedCoordinate, 1);
            }
        }
        var overlaps = positions.Where(x => x.Value > 1).ToList();
        return overlaps.Count();
    }

    public static List<Line> ParseCoordinates()
    {
        var coordinates = new List<Line>();
        var coordRow = Input.Select(i => i.Split(" -> ")).ToList();
        foreach (var coord in coordRow)
        {
            var startCoord = coord[0].Split(',');
            var endCoord = coord[1].Split(',');
            Coordinate coordinate1 = new Coordinate { X = int.Parse(startCoord[0]), Y = int.Parse(startCoord[1]) };
            Coordinate coordinate2 = new Coordinate { X = int.Parse(endCoord[0]), Y = int.Parse(endCoord[1]) };
            var start = coordinate1.X <= coordinate2.X ? coordinate1 : coordinate2;
            coordinates.Add(new Line
            {
                Start = start,
                End = start == coordinate1 ? coordinate2 : coordinate1
            });
        }
        return coordinates;
    }

    public static List<Coordinate> GenerateCoordinates(Line coordinates)
    {
        var lineType = Line.DetermineLineType(coordinates);

        var currentPosition = coordinates.Start;
        var generatedCoords = new List<Coordinate>() { currentPosition };

        bool addY = NeedToAdd(coordinates.Start.Y, coordinates.End.Y);

        while (!currentPosition.Equals(coordinates.End))
        {
            if (lineType == LineOrientation.Horizontal)
            {
                currentPosition = new Coordinate { X = currentPosition.X + 1, Y = currentPosition.Y };
            }
            else if (lineType == LineOrientation.Vertical)
            {
                if (addY)
                    currentPosition = new Coordinate { X = currentPosition.X, Y = currentPosition.Y + 1 };
                else
                    currentPosition = new Coordinate { X = currentPosition.X, Y = currentPosition.Y - 1 };
            }
            else if (lineType == LineOrientation.Diagonal_Positive)
            {
                currentPosition = new Coordinate { X = currentPosition.X + 1, Y = currentPosition.Y + 1 };
            }
            else 
            {
                currentPosition = new Coordinate { X = currentPosition.X + 1, Y = currentPosition.Y - 1 };
            }
            generatedCoords.Add(currentPosition);
        }
        return generatedCoords;
    }

    public static bool NeedToAdd(int start, int end)
    {
        return start < end;
    }
}






