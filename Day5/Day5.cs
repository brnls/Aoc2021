using System.Text;

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
        var coordinates = ParseCoordinates().Where(c => c.IsVerticalLine() || c.IsHorizontalLine() || c.IsDiagonalLine()).ToList();
        return FindOverlaps(coordinates);
    }

    public static long FindOverlaps(List<Coordinates> coordinates)
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
        return positions.Values.Count(x => x > 1);

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

    public delegate int Del(int x, int y);

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

    public static int Add(int val1, int val2)
    {
        return val1 + val2;
    }
    public static int Minus(int val, int val2)
    {
        return val - val2;
    }
        
}



public class Coordinates
{
    public Coordinate Start { get; set; }
    public Coordinate End { get; set; }


    public bool IsVerticalLine()
    {
        return Start.X == End.X && Start.Y != End.Y;
    }

    public bool IsHorizontalLine()
    {
        return Start.X != End.X && Start.Y == End.Y;
    }
    
    public bool IsDiagonalLine()
    {
        return
            (Start.X == Start.Y && End.X == End.Y) ||  //45diagonal
            (Start.X == End.Y && Start.Y == End.X);    //45diagonal
    }

    public override bool Equals(object? obj)
    {
        return obj is Coordinates coordinates &&
               EqualityComparer<Coordinate>.Default.Equals(Start, coordinates.Start) &&
               EqualityComparer<Coordinate>.Default.Equals(End, coordinates.End);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
}

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }


    public override bool Equals(object? obj)
    {
        return obj is Coordinate coordinate &&
               X == coordinate.X &&
               Y == coordinate.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

