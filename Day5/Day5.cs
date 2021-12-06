
namespace Aoc2021.Day5;

class Day5
{
    public static readonly List<string> Input = File.ReadAllLines("Day5/input").ToList();
    public delegate int Del(int x, int y);
    public delegate Coordinate CoordinateOp(Coordinate coordinate, bool add);

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

    public static long FindOverlaps(List<Coordinates> coordinates)
    {
        var positions = new Dictionary<Coordinate, int>();
        foreach (var coordinatePair in coordinates)
        {
            var coordList = GenerateCoordinatesLongWay(coordinatePair);
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

    public static List<Coordinates> ParseCoordinates()
    {
        var coordinates = new List<Coordinates>();
        var coordRow = Input.Select(i => i.Split(" -> ")).ToList();
        foreach (var coord in coordRow)
        {
            var startCoord = coord[0].Split(',');
            var endCoord = coord[1].Split(',');
            coordinates.Add(new Coordinates
            {
                Start = new Coordinate { X = int.Parse(startCoord[0]), Y = int.Parse(startCoord[1]) },
                End = new Coordinate { X = int.Parse(endCoord[0]), Y = int.Parse(endCoord[1]) }
            });
        }
        return coordinates;
    }


    public static List<Coordinate> GenerateCoordinates(Coordinates coordinates)
    {
        Del _operator = (coordinates.Start.X > coordinates.End.X) || (coordinates.Start.Y > coordinates.End.Y) ? Minus : Add;
        var generatedCoords = new List<Coordinate>() { coordinates.Start };

        var currentPosition = coordinates.Start;
        var endPosition = coordinates.End;
        while (!currentPosition.Equals(endPosition))
        {
            //vertical line
            if (currentPosition.X == endPosition.X && currentPosition.Y != endPosition.Y)
                currentPosition = new Coordinate { X = currentPosition.X, Y = _operator(currentPosition.Y, 1) };
            //horizontal line
            else if (currentPosition.X != endPosition.X && currentPosition.Y == endPosition.Y)
                currentPosition = new Coordinate { X = _operator(currentPosition.X, 1), Y = currentPosition.Y };
            else
                throw new Exception("Expecting horizontal/vertical line only");

            generatedCoords.Add(currentPosition);
        }
        return generatedCoords;
    }

    public static List<Coordinate> GenerateCoordinatesLongWay(Coordinates coordinates)
    {
        var generatedCoords = new List<Coordinate>() { coordinates.Start };
        var lineType = Coordinates.DetermineLineType(coordinates);

        var startPosition = coordinates.Start;
        var currentPosition = startPosition;
        var endPosition = coordinates.End;
        bool addX = NeedToAdd(startPosition.X, endPosition.X);
        bool addY = NeedToAdd(startPosition.Y, endPosition.Y);

        while (!currentPosition.Equals(endPosition))
        {
            if (lineType == Line.Horizontal)
            {
                if (addX)
                    currentPosition = new Coordinate { X = currentPosition.X + 1, Y = currentPosition.Y };
                else
                    currentPosition = new Coordinate { X = currentPosition.X - 1, Y = currentPosition.Y };
            }
            else if (lineType == Line.Vertical)
            {
                if (addY)
                    currentPosition = new Coordinate { X = currentPosition.X, Y = currentPosition.Y + 1 };
                else
                    currentPosition = new Coordinate { X = currentPosition.X, Y = currentPosition.Y - 1 };
            }
            else if (lineType == Line.Diagonal_Equal)
            {
                if (addX)
                    currentPosition = new Coordinate { X = currentPosition.X + 1, Y = currentPosition.Y + 1 };
                else
                    currentPosition = new Coordinate { X = currentPosition.X - 1, Y = currentPosition.Y - 1 };
            }
            else 
            {
                if (addX)
                    currentPosition = new Coordinate { X = currentPosition.X + 1, Y = currentPosition.Y - 1 };
                else
                    currentPosition = new Coordinate { X = currentPosition.X - 1, Y = currentPosition.Y + 1 };
            }
            generatedCoords.Add(currentPosition);
        }
        return generatedCoords;
    }

    public static bool NeedToAdd(int start, int end)
    {
        return start < end;
    }

    public static int Add(int val1, int val2)
    {
        return val1 + val2;
    }

    public static int Minus(int val, int val2)
    {
        return val - val2;
    }
}






